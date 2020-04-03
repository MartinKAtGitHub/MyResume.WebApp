$(document).ready(function () {
    var addExpPointBtn = $("#addExpPointBtn");
    // const addDescriptionBtn = $("#addDescriptionBtn");
    var expPointCounter = 0;
    addExpPointBtn.on("click", function () {
        // Add span for validation
        // Add Label
        var inputExpPointMaxLength = 30; // Get this from the server MaxLentgth Attribute
        var inputExpPointHTML = "<input type='text' \
        value = '' \
        data-val='true' \
        data-val-maxlength='The field PointTitle must be a string or array type with a maximum length of " + inputExpPointMaxLength + ".' \
        data-val-maxlength-max='" + inputExpPointMaxLength + "' data-val-required='The PointTitle field is required.' \
        id = 'NewExpGrp_ExpPoints_" + expPointCounter + "__PointTitle' \
        maxlength = '" + inputExpPointMaxLength + "' \
        name = 'NewExpGrp.ExpPoints[" + expPointCounter + "].PointTitle' >";
        var addDiscriptionBtnHTML = $("<button id='point" + expPointCounter + "DescBtn' type = 'button'> Add Desc </button>");
        var descCounter = 0;
        // DESC BUTTON
        addDiscriptionBtnHTML.on('click', function () {
            var inputDescMaxLength = 60; // Get this from the server MaxLentgth Attribute
            var inputDescriptionHTML = " <input type='text' \
            value = '' \
            data-val='true' \
            data-val-maxlength='The field Desc must be a string or array type with a maximum length of '" + inputDescMaxLength + "'.' \
            data-val-maxlength-max='" + inputDescMaxLength + "' \
            data-val-required='The Desc field is required.' \
            id = 'NewExpGrp_ExpPoints_" + expPointCounter + "__Descriptions_" + descCounter + "__Desc' \
            maxlength = '" + inputDescMaxLength + "' \
            name = 'NewExpGrp.ExpPoints[" + expPointCounter + "].Descriptions[" + descCounter + "].Desc' > ";
            descCounter++;
        });
        var CoreHTML = $("<div id='point" + expPointCounter + "' class='exp-point-section m-3 p-2'></div>");
        CoreHTML.append(inputExpPointHTML);
        CoreHTML.append(addDiscriptionBtnHTML);
        addExpPointBtn.before(CoreHTML);
        expPointCounter++;
    });
    //addDescriptionBtn.on('click', () => {
    //    let inputDescMaxLength = 60; // Get this from the server MaxLentgth Attribute
    //    var inputDescriptionHTML = " <input type='text' \
    //    value = '' \
    //    data-val='true' \
    //    data-val-maxlength='The field Desc must be a string or array type with a maximum length of '"+ inputDescMaxLength + "'.' \
    //    data-val-maxlength-max='"+ inputDescMaxLength + "' \
    //    data-val-required='The Desc field is required.' \
    //    id = 'NewExpGrp_ExpPoints_"+ expPointCounter + "__Descriptions_" + descCounter + "__Desc' \
    //    maxlength = '"+ inputDescMaxLength + "' \
    //    name = 'NewExpGrp.ExpPoints["+ expPointCounter + "].Descriptions[" + descCounter + "].Desc' > ";
    //    descCounter++;
    //});
});
function OnSuccsessfulCreateEXP(xhr) {
    //  var JSONOBJECT = JSON.parse(xhr.response); // Use this to manipulate the JSON https://www.w3schools.com/js/js_json_parse.asp
    //ArrayOfEvents[] . disconnect events
    alert("CLOSING MODUAL");
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