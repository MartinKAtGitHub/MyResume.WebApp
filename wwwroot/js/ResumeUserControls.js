$(document).ready(function () {
    var addExpPointBtn = $("#addExpPointBtn");
    var removeExpPointBtn = $("#removeExpPointBtn");
    var expFrom = $("#newExpFrom")[0];
    var expPointCounter = 0; // We spawn in with 1
    AddExpPointField(expPointCounter, addExpPointBtn, expFrom);
    expPointCounter++;
    addExpPointBtn.on("click", function () {
        AddExpPointField(expPointCounter, addExpPointBtn, expFrom);
        expPointCounter++;
    });
    removeExpPointBtn.on("click", function () {
        if (expPointCounter >= 2) {
            RemoveExpPointField(expFrom);
            expPointCounter--;
        }
        else {
            alert("You need at least 1 highlight with a description | this is a debug window"); // TODO change removeExpPointBtn() from using inline warning rather then an Alert window
        }
    });
});
function AddExpPointField(expPointCounter, addExpPointBtn, expFrom) {
    var parentIndex = expPointCounter;
    var descCounter = 0;
    var inputExpPointMaxLength = 30; // Get this from the server MaxLentgth Attribute
    var inputExpPointHTML = "<input type='text' \
        placeholder = 'PointTitle[" + parentIndex + "]' \
        class = 'exp-point-title' \
        data-val='true' \
        data-val-maxlength='The field PointTitle must be a string or array type with a maximum length of " + inputExpPointMaxLength + ".' \
        data-val-maxlength-max='" + inputExpPointMaxLength + "' data-val-required='The PointTitle field is required.' \
        id = 'NewExpGrp_ExpPoints_" + parentIndex + "__PointTitle' \
        maxlength = '" + inputExpPointMaxLength + "' \
        name = 'NewExpGrp.ExpPoints[" + parentIndex + "].PointTitle' value>";
    var spanValidationExpPointTitle = "<span \
        class='text-danger field-validation-valid' \
        data-valmsg-for= 'NewExpGrp.ExpPoints[" + parentIndex + "].PointTitle' \
        data-valmsg-replace='true'></span>";
    var addDiscriptionBtnHTML = $("<button id='point" + parentIndex + "DescBtn' type = 'button'> Add Desc </button>");
    var CoreHTML = $("<div id='point-" + parentIndex + "' class='exp-point-section m-3 p-2'></div>");
    CoreHTML.append(inputExpPointHTML);
    CoreHTML.append(spanValidationExpPointTitle);
    CoreHTML.append(addDiscriptionBtnHTML);
    addExpPointBtn.before(CoreHTML);
    AddDescriptionField(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom); //  we crate 1 desc field on spawn as it is an mandatory requirement for a point
    descCounter++;
    addDiscriptionBtnHTML.on('click', function () {
        AddDescriptionField(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom);
        descCounter++;
    });
    ResetFormValidationJQUnobtrusive(expFrom);
}
function AddDescriptionField(descCounter, parentIndex, spawnPosition, expFrom) {
    var inputDescMaxLength = 60; // Get this from the server MaxLentgth Attribute
    var inputDescriptionHTML = " <input type='text' \
            placeholder ='desc [" + descCounter + "]' \
            class = 'exp-point-desc' \
            data-val='true' \
            data-val-maxlength='The field Desc must be a string or array type with a maximum length of '" + inputDescMaxLength + "'.' \
            data-val-maxlength-max='" + inputDescMaxLength + "' \
            data-val-required='The Desc field is required.' \
            id = 'NewExpGrp_ExpPoints_" + parentIndex + "__Descriptions_" + descCounter + "__Desc' \
            maxlength = '" + inputDescMaxLength + "' \
            name = 'NewExpGrp.ExpPoints[" + parentIndex + "].Descriptions[" + descCounter + "].Desc' > ";
    var spanValidationExpPointDesc = "<span \
            class='text-danger field-validation-valid' \
            data-valmsg-for= 'NewExpGrp.ExpPoints[" + parentIndex + "].Descriptions[" + descCounter + "].Desc' \
            data-valmsg-replace='true'></span>";
    spawnPosition.before(inputDescriptionHTML);
    spawnPosition.before(spanValidationExpPointDesc);
    ResetFormValidationJQUnobtrusive(expFrom);
}
function RemoveExpPointField(expFrom) {
    var expPointContainers = $(".exp-point-section");
    expPointContainers[expPointContainers.length - 1].remove();
}
function ResetFormValidationJQUnobtrusive(formTag) {
    /* The client side validation messages connected bu (JQ unobtrusive) to our DataAnnotations attributes on the server side are only loaded on page load/refresh .
     * So they won't work on AJAX requests, unless we force a reset on the form.
     */
    $(formTag).removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(formTag);
}
function OnSuccsessfulCreateEXP(xhr) {
    //ArrayOfEvents[] . disconnect events
    alert("CLOSING MODUAL");
    var form = $("#newExpFrom")[0];
    form.reset();
    //// Remove dynamically crated HTML
    $("#newExperienceModal").modal('hide');
}
function OnFailureCreateEXP(xhr) {
    alert("Status : " + xhr.status + " | Text = " + xhr.statusText); // i can set these in the controller
}
function CreateExp(expId) {
    // $("#createExpForm").hide("slow");
    var form = $('#newExpFrom');
    var formData = new FormData(form.get(0));
    var actionURL = form.prop('action');
    //formData.forEach(element => {
    //    console.log(element.valueOf());
    //});
    $.ajax({
        type: "POST",
        url: actionURL,
        data: formData,
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (msg) {
            console.log("SUCCESS" + msg);
            //  alert("SUCCSESS !!!!!!!!!!!");
            // Maybe add a SUCCSES message or Icon
        },
        error: function (req, status, error) {
            alert(error);
            // Maybe add a FAIL message or Icon IN CASE AN  UNAUTHIRZED PERSON TRYS IT
        }
    });
}
function AddExpPoint(expId) {
    //$(".modal-body").
}
function AddExpPointDesc(expId) {
    // Hide or unHide 
}
function DeleteExp(expId) {
}
// Revert exp back to the point before you started editing
function RevertExp(expId) {
}
function UpdateExp(expId) {
    var form = $('#mainForm');
    if (form == null) {
        // State we cant find form
        return;
    }
    var formData = new FormData(form.get(0));
    var actionURL = form.prop('action');
    formData.forEach(function (element) {
        console.log(element.valueOf());
    });
    $.ajax({
        type: "POST",
        url: actionURL,
        data: formData,
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (msg) {
            console.log("SUCCESS" + msg);
            // Maybe add a SUCCSES message or Icon
        },
        error: function (req, status, error) {
            alert(error);
            // Maybe add a FAIL message or Icon
        }
    });
}
//# sourceMappingURL=ResumeUserControls.js.map