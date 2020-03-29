function CreateExp(expId) {
    // Hide or unHide 
    $("#createExpForm").hide("slow");
}
function CreateExpPoint(expId) {
    // Hide or unHide 
}
function CreateExpPointDesc(expId) {
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