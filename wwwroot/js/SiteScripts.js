

function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;
  //  var editBtn = 'editBtn_' + uniqueId;
    var editCol = 'editCol_' + uniqueId;
    var confirmTxt = 'confirmTxt_' + uniqueId;
    var titleTxt = 'titleTxt_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
    //    $('#' + editBtn).hide();
        $('#' + editCol).hide();
        $('#' + titleTxt).hide();

        $('#' + confirmDeleteSpan).show();
        $('#' + confirmTxt).show();
    }
    else {
        $('#' + deleteSpan).show();
      //  $('#' + editBtn).show();
        $('#' + editCol).show();
        $('#' + titleTxt).show();

        $('#' + confirmDeleteSpan).hide();
        $('#' + confirmTxt).hide();

    }
}

function test() {
    var x = XMLHttpRequest();
    x.open();
}