
$(document).ready(() => {

    const addExpPointBtn = $("#addExpPointBtn");
    const removeExpPointBtn = $("#removeExpPointBtn");
    const formCreateNewExp = $("#newExpFrom")[0] as HTMLFormElement;
    let expPointCounter = 0;

    AddExpPointFieldModal(expPointCounter, addExpPointBtn, formCreateNewExp);
    expPointCounter++;

    addExpPointBtn.on("click", () => {

        AddExpPointFieldModal(expPointCounter, addExpPointBtn, formCreateNewExp);
        expPointCounter++;

    });

    removeExpPointBtn.on("click", () => {
        if (expPointCounter >= 2) {
            RemoveExpPointField();
            expPointCounter--;
        } else {
            alert("Cant remove highlight! You need at least 1 highlight with a description to create a experience group | this is a temp window"); // TODO change removeExpPointBtn() from using inline warning rather then an Alert window
        }
    });


    $(document).ajaxSuccess(function (event, xhr, settings) { // Because of AJAX unobtrusive finding success even it uses it impossible we need to use this global event on ajax success and filter out witch one succeeded

        if (settings.url == formCreateNewExp.action) {
            $("#exp-grp-container").load("/Home/GetExperienceView", () => {
                if (status == "error") {
                    let msg = "Sorry but there was an error: ";
                    alert(msg + xhr.status + " " + xhr.statusText);
                }

                if (status == "success") {
                    alert("Crate an pop-up to indicate the creation was successful");
                }
            });

            // GenerateMainPageHTML(xhr);
            expPointCounter = 1; // we set this to 1 because 0 index is spawned at the start of the page
        }

    });

    ConnectAddExpPointBtn();



});

//function ConnectAddDescBtn() { 
//    let pointSections = $(".point-section");

//    for (var i = 0; i < pointSections.length; i++) {
//        console.log("Points = " +i)
//    }
//}

function ConnectAddExpPointBtn() {

    let expGrps = $(".experience-section");
    for (var i = 0; i < expGrps.length; i++) {

        let addPointBtn = $("#addPoint_" + i + "");
        let sectionID = $("#experienceSectionID_" + i + "").get(0) as HTMLInputElement;
        let expSection = $("#experienceSection_" + i + "");
        UpdateWithNewPointField(addPointBtn, sectionID.value);

        let points = expSection.children(".point-section");
        for (var j = 0; j < points.length; j++) {

            let desc = points.children(".desc-section");
            let addDescBtn = $("#addDesc_" + i + "_" + j + "");

            UpdateWithNewDescField(addDescBtn, sectionID.value, j);
            //for (var k = 0; k < desc.length; k++) {
            //    //let addDescBtn = $("#addDesc_" + j + "_" + k + "");
            //    //UpdateWithNewDescField(addDescBtn, sectionID.value, j);
            //}
        }
    }

}

function UpdateWithNewDescField(addDescbtn: JQuery<HTMLElement>, sectionID: string, pointIndex: number) {
    addDescbtn.on("click", () => {
        console.log(addDescbtn.get(0).id);
        $("#exp-grp-container").load("/Home/AddDescFieldToExperienceView", { expID: sectionID, pointIndex: pointIndex }, (responseText, textStatus, jqXHR) => {

            if (textStatus == "error") {
                ConnectAddExpPointBtn();
                alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
            }

            if (textStatus == "success") {
                ConnectAddExpPointBtn();
            }
        });

    });
}


function UpdateWithNewPointField(addPointbtn: JQuery<HTMLElement>, sectionID: string) {
    addPointbtn.on("click", () => {
        $("#exp-grp-container").load("/Home/AddpointFieldToExperienceView", { expID: sectionID }, (responseText, textStatus, jqXHR) => {

            if (textStatus == "error") {
                ConnectAddExpPointBtn();
                alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
            }

            if (textStatus == "success") {
                ConnectAddExpPointBtn();
            }
        });

    });
}

function GenerateMainPageHTML(xhr?: JQuery.jqXHR<any>) {
    if (xhr != undefined) {// or Null ?

        // we need to crate a class for the newExpGrpObject or else it will be an any type
        let newExpGrpObject = xhr.responseJSON.newExpGrp; // this works LEL
        //console.log(newExpGrpObject);

        // Use json object (newExpGrpObject)
        // dynamic html construction


        // ajax into partial view ?
        // generate fields using data (newExpGrpObject) pass this as model
    } else {

        // AJAX Get JSON From SEVER
        // use json for dynamic html construction
    }


}

function AddExpPointFieldModal(expPointCounter: number, addExpPointBtn: JQuery<HTMLElement>, expFrom: HTMLFormElement) {

    let parentIndex = expPointCounter;
    let descCounter = 0;

    let inputExpPointMaxLength = 30; // Get this from the server MaxLentgth Attribute
    let inputExpPointHTML = "<input type='text' \
        placeholder = 'PointTitle["+ parentIndex + "]' \
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


    let addDiscriptionBtnHTML = $("<button id='point" + parentIndex + "AddDescBtn' type = 'button'> Add Desc </button>");
    let removeDiscriptionBtnHTML = $("<button id='point" + parentIndex + "RemoveDescBtn' type = 'button'> Remove Desc </button>");
    let expPointDiv = $("<div id='point-" + parentIndex + "' class='exp-point-section m-3 p-2'></div>");

    expPointDiv.append(inputExpPointHTML);
    expPointDiv.append(spanValidationExpPointTitle);
    expPointDiv.append(addDiscriptionBtnHTML);
    expPointDiv.append(removeDiscriptionBtnHTML);

    addExpPointBtn.before(expPointDiv);

    AddDescriptionFieldModal(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom); //  we crate 1 desc field on spawn as it is an mandatory requirement for a point
    descCounter++

    addDiscriptionBtnHTML.on('click', () => {

        AddDescriptionFieldModal(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom);
        descCounter++;
    });

    removeDiscriptionBtnHTML.on('click', () => {
        if (descCounter >= 2) {
            RemoveDescField(expPointDiv);
            descCounter--;
        } else {
            alert("Cant remove description! You need at least 1 description with a highlight to create a experience group | this is a temp window"); // TODO change removeExpPointBtn() from using inline warning rather then an Alert window
        }
    });

    ResetFormValidationJQUnobtrusive(expFrom);
}


function AddDescriptionFieldModal(descCounter: number, parentIndex: number, spawnPosition: JQuery<HTMLElement>, expFrom: HTMLFormElement) {
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

    let descContainer = $("<div id='desc-container-" + descCounter + "' class='desc-container m-1 p-1'></div>")

    spawnPosition.before(descContainer);
    descContainer.append(inputDescriptionHTML);
    descContainer.append(spanValidationExpPointDesc);

    ResetFormValidationJQUnobtrusive(expFrom);

}


function RemoveExpPointField() {

    let expPointContainers = $(".exp-point-section");
    expPointContainers[expPointContainers.length - 1].remove();
}

function RemoveDescField(expPoint: JQuery<HTMLElement>) {
    let descFields = expPoint.children(".desc-container");
    descFields[descFields.length - 1].remove();
}


function ResetFormValidationJQUnobtrusive(formTag: HTMLFormElement) {

    /* The client side validation messages connected bu (JQ unobtrusive) to our DataAnnotations attributes on the server side are only loaded on page load/refresh .
     * So they won't work on AJAX requests, unless we force a reset on the form.
     */

    $(formTag).removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(formTag);
}

function OnBeginCreateEXP() {
    let inputBtn = $("#expCreateSubmitBtn").get(0) as HTMLInputElement // This should be cached but in global?
    inputBtn.disabled = true;
}

function OnCompleteCreateEXP() {
    let inputBtn = $("#expCreateSubmitBtn").get(0) as HTMLInputElement // // This should be cached but in global?
    inputBtn.disabled = false;
}

function OnSuccessfulCreateEXP(xhr: XMLHttpRequest) { // This only fires on 200


    let form = $("#newExpFrom")[0] as HTMLFormElement;
    form.reset();

    let expPointContainers = $(".exp-point-section");
    for (var i = expPointContainers.length - 1; i > 0; i--) {
        expPointContainers[i].remove();
    }

    $("#newExperienceModal").modal('hide');
}

function OnSuccessfulEditEXP() { // Successful

    $("#exp-grp-container").load("/Home/GetExperienceView", (responseText, textStatus, jqXHR) => {

        if (textStatus == "error") {
            ConnectAddExpPointBtn();
            alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
        }

        if (textStatus == "success") {
            ConnectAddExpPointBtn();
        }
    });

    alert("TEMP Make Pop-up to indicate successful edit");
}

function OnFailureEditEXP(xhr: XMLHttpRequest) {
    alert("On EDIT something went wrong | Status : " + xhr.status + " | Text = " + xhr.statusText); // i can set these in the controller

}

function OnFailureCreateEXP(xhr: XMLHttpRequest) { // jQuery XMLHttpRequest type ?
    alert("On Create something went wrong | Status : " + xhr.status + " | Text = " + xhr.statusText); // i can set these in the controller
}


//function CreateExp(expId: string): void {

//    // $("#createExpForm").hide("slow");
//    const form = $('#newExpFrom');
//    const formData = new FormData(form.get(0) as HTMLFormElement);
//    const actionURL = form.prop('action');

//    //formData.forEach(element => {
//    //    console.log(element.valueOf());
//    //});

//    $.ajax({

//        type: "POST",
//        url: actionURL,
//        data: formData,
//        dataType: "json",

//        processData: false,
//        contentType: false,

//        success: function (msg) {
//            console.log("SUCCESS" + msg);
//            //  alert("SUCCSESS !!!!!!!!!!!");
//            // Maybe add a SUCCSES message or Icon

//        },

//        error: function (req, status, error) {
//            alert(error);

//            // Maybe add a FAIL message or Icon IN CASE AN  UNAUTHIRZED PERSON TRYS IT
//        }
//    });
//}

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