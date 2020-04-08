
$(document).ready(() => {

    const addExpPointBtn = $("#addExpPointBtn");
    const expFrom = $("#newExpFrom")[0] as HTMLFormElement;
    let expPointCounter = 0; // We spawn in with 1

    AddExpPointField(expPointCounter, addExpPointBtn, expFrom);
    expPointCounter++;

    addExpPointBtn.on("click", () => {

        AddExpPointField(expPointCounter, addExpPointBtn, expFrom);
        expPointCounter++;

    });
});


function AddExpPointField(expPointCounter: number, addExpPointBtn: JQuery<HTMLElement>, expFrom: HTMLFormElement) {

    let parentIndex = expPointCounter;
    let descCounter = 0;

    let inputExpPointMaxLength = 30; // Get this from the server MaxLentgth Attribute
    let inputExpPointHTML = "<input type='text' \
        placeholder = 'PointTitle["+ parentIndex +"]' \
        class = 'exp-point-title' \
        data-val='true' \
        data-val-maxlength='The field PointTitle must be a string or array type with a maximum length of "+ inputExpPointMaxLength + ".' \
        data-val-maxlength-max='"+ inputExpPointMaxLength + "' data-val-required='The PointTitle field is required.' \
        id = 'NewExpGrp_ExpPoints_" + parentIndex + "__PointTitle' \
        maxlength = '"+ inputExpPointMaxLength + "' \
        name = 'NewExpGrp.ExpPoints["+ parentIndex + "].PointTitle' value>";

    let spanValidationExpPointTitle = "<span \
        class='text-danger field-validation-valid' \
        data-valmsg-for= 'NewExpGrp.ExpPoints["+ parentIndex + "].PointTitle' \
        data-valmsg-replace='true'></span>";


    let addDiscriptionBtnHTML = $("<button id='point" + parentIndex + "DescBtn' type = 'button'> Add Desc </button>");

    let CoreHTML = $("<div id='point-" + parentIndex + "' class='exp-point-section m-3 p-2'></div>");
    CoreHTML.append(inputExpPointHTML);
    CoreHTML.append(spanValidationExpPointTitle);
    CoreHTML.append(addDiscriptionBtnHTML);

    addExpPointBtn.before(CoreHTML);

    AddDescriptionField(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom); //  we crate 1 desc field on spawn as it is an mandatory requirement for a point
    descCounter++

    addDiscriptionBtnHTML.on('click', () => {

        AddDescriptionField(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom);
        descCounter++;
    });

    // the JQ unobtrusive only loads in when the page dose, do we need to Re-parse the form so that the dynamicly added HTML can be validated
    $(expFrom).removeData("validator")    // Added by jQuery Validate
        .removeData("unobtrusiveValidation");   // Added by jQuery Unobtrusive Validation
    $.validator.unobtrusive.parse(expFrom);
}


function AddDescriptionField(descCounter: number, parentIndex: number, spawnPosition: JQuery<HTMLElement>, expFrom: HTMLFormElement) {
    let inputDescMaxLength = 60; // Get this from the server MaxLentgth Attribute

    var inputDescriptionHTML = " <input type='text' \
            placeholder ='desc ["+ descCounter + "]' \
            class = 'exp-point-desc' \
            data-val='true' \
            data-val-maxlength='The field Desc must be a string or array type with a maximum length of '"+ inputDescMaxLength + "'.' \
            data-val-maxlength-max='"+ inputDescMaxLength + "' \
            data-val-required='The Desc field is required.' \
            id = 'NewExpGrp_ExpPoints_"+ parentIndex + "__Descriptions_" + descCounter + "__Desc' \
            maxlength = '"+ inputDescMaxLength + "' \
            name = 'NewExpGrp.ExpPoints["+ parentIndex + "].Descriptions[" + descCounter + "].Desc' > ";

    let spanValidationExpPointDesc = "<span \
            class='text-danger field-validation-valid' \
            data-valmsg-for= 'NewExpGrp.ExpPoints["+ parentIndex + "].Descriptions[" + descCounter + "].Desc' \
            data-valmsg-replace='true'></span>";


    spawnPosition.before(inputDescriptionHTML);
    spawnPosition.before(spanValidationExpPointDesc);
  

    $(expFrom).removeData("validator")    // Added by jQuery Validate
        .removeData("unobtrusiveValidation");   // Added by jQuery Unobtrusive Validation
    $.validator.unobtrusive.parse(expFrom);

}



function OnSuccsessfulCreateEXP(xhr: XMLHttpRequest) { // This only fires on 200

    //ArrayOfEvents[] . disconnect events
    alert("CLOSING MODUAL");
    let form = $("#newExpFrom")[0] as HTMLFormElement;
    form.reset();

    //// Remove dynamically crated HTML

    $("#newExperienceModal").modal('hide');
}


function OnFailureCreateEXP(xhr: XMLHttpRequest) { // jQuery XMLHttpRequest type ?
    alert("Status : " + xhr.status + " | Text = " + xhr.statusText); // i can set these in the controller
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