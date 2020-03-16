

function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;
    var editBtn = 'editBtn_' + uniqueId;
    var editCol = 'editCol_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + editBtn).hide();
        $('#' + editCol).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + editBtn).show();
        $('#' + editCol).show();
        $('#' + confirmDeleteSpan).hide();
    }
}