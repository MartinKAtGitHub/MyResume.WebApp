$(document).ready(function () {
    var addExpPointBtn = $("#addExpPointBtn");
    var expPointCounter = 0;
    addExpPointBtn.on("click", function () {
        // Add span for validation
        // Add Label
        var inputHTML = "<input type='text' \
        value = '' \
        data-val='true' \
        data-val-maxlength='The field PointTitle must be a string or array type with a maximum length of 30.' \
        data-val-maxlength-max='30' data-val-required='The PointTitle field is required.' \
        id = 'NewExpGrp_ExpPoints_" + expPointCounter + "__PointTitle' \
        maxlength = '30' \
        name = 'NewExpGrp.ExpPoints[" + expPointCounter + "].PointTitle' >";
        addExpPointBtn.before("<div class='exp-point-section m-3 p-2'>" + inputHTML + "</div>");
        expPointCounter++;
    });
});
function OnSuccsessfulCreateEXP(xhr) {
    //  var JSONOBJECT = JSON.parse(xhr.response); // Use this to manipulate the JSON https://www.w3schools.com/js/js_json_parse.asp
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