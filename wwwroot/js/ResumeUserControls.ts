﻿
$(document).ready(() => {

    const addExpPointBtn = $("#addExpPointBtn");
    let expPointCounter = 0;

    addExpPointBtn.on("click", () => {

        // Add span for validation
        // Add Label

        let inputHTML = "<input type='text' \
        value = '' \
        data-val='true' \
        data-val-maxlength='The field PointTitle must be a string or array type with a maximum length of 30.' \
        data-val-maxlength-max='30' data-val-required='The PointTitle field is required.' \
        id = 'NewExpGrp_ExpPoints_" + expPointCounter + "__PointTitle' \
        maxlength = '30' \
        name = 'NewExpGrp.ExpPoints["+ expPointCounter + "].PointTitle' >";

        addExpPointBtn.before("<div class='exp-point-section m-3 p-2'>" + inputHTML + "</div>")

        expPointCounter++;
    })

});





function OnSuccsessfulCreateEXP(xhr: XMLHttpRequest) {

  //  var JSONOBJECT = JSON.parse(xhr.response); // Use this to manipulate the JSON https://www.w3schools.com/js/js_json_parse.asp

    alert("CLOSING MODUAL");
    $("#newExperienceModal").modal('hide');

}


function CreateExp(expId: string): void {

    // $("#createExpForm").hide("slow");
    const form = $('#newExpFrom');
    const formData = new FormData(form.get(0) as HTMLFormElement);
    const actionURL = form.prop('action');

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

function AddExpPoint(expId: string): void {
    //$(".modal-body").
}



function AddExpPointDesc(expId: string): void {
    // Hide or unHide 
}

function DeleteExp(expId): void {

}

// Revert exp back to the point before you started editing
function RevertExp(expId): void {

}

function UpdateExp(expId): void { // TODO This func cant be called if the person do not own the resume

    const form = $('#mainForm');

    if (form == null) {
        // State we cant find form
        return;
    }

    const formData = new FormData(form.get(0) as HTMLFormElement);
    const actionURL = form.prop('action');

    formData.forEach(element => {
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