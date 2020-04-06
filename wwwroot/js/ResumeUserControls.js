$(document).ready(function () {
    var addExpPointBtn = $("#addExpPointBtn");
    var expFrom = $("#newExpFrom")[0];
    var expPointCounter = 0;
    addExpPointBtn.on("click", function () {
        // Add span for validation
        // Add Label
        var parentIndex = expPointCounter;
        var inputExpPointMaxLength = 30; // Get this from the server MaxLentgth Attribute
        var inputExpPointHTML = "<input type='text' \
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
        var descCounter = 0;
        var CoreHTML = $("<div id='point-" + parentIndex + "' class='exp-point-section m-3 p-2'></div>");
        CoreHTML.append(inputExpPointHTML);
        CoreHTML.append(spanValidationExpPointTitle);
        CoreHTML.append(addDiscriptionBtnHTML);
        addExpPointBtn.before(CoreHTML);
        // DESC BUTTON
        addDiscriptionBtnHTML.on('click', function () {
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
            addDiscriptionBtnHTML.before(inputDescriptionHTML);
            addDiscriptionBtnHTML.before(spanValidationExpPointDesc);
            descCounter++;
            $(expFrom).removeData("validator") // Added by jQuery Validate
                .removeData("unobtrusiveValidation"); // Added by jQuery Unobtrusive Validation
            $.validator.unobtrusive.parse(expFrom);
        });
        expPointCounter++;
        // the JQ unobtrusive only loads in when the page dose, do we need to Re-parse the form so that the dynamicly added HTML can be validated
        $(expFrom).removeData("validator") // Added by jQuery Validate
            .removeData("unobtrusiveValidation"); // Added by jQuery Unobtrusive Validation
        $.validator.unobtrusive.parse(expFrom);
    });
});
function OnSuccsessfulCreateEXP(xhr) {
    //  var JSONOBJECT = JSON.parse(xhr.response); // Use this to manipulate the JSON https://www.w3schools.com/js/js_json_parse.asp
    //ArrayOfEvents[] . disconnect events
    alert("CLOSING MODUAL");
    var form = $("#newExpFrom")[0];
    form.reset();
    // Remove dynamically crated HTML
    $("#newExperienceModal").modal('hide');
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