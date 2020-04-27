$(document).ready(() => {

    $('[data-toggle="tooltip"]').tooltip(); // Enables bootstrap tool tips

    showFileNameInFileField();
});


function showFileNameInFileField() {
    $('.custom-file-input').on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).next('.custom-file-label').html(fileName);
    });
}