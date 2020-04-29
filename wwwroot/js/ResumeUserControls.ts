var skillId = "";
var editMode: any;

$(document).ready(() => {

   
    if (editMode == true) {

        $("#deleteSkillModalBtn").on("click", () => {
            $("#skillDisplayContainer").load("/Home/DeleteSkill", { id: skillId }, (responseText, textStatus, jqXHR) => {

                if (textStatus == "success") {

                    AttachEventsToSkillOparations();

                    ShowAlert("Skill was successfully deleted", "alert-success", 4000);
                }

                if (textStatus == "error") {

                    AttachEventsToSkillOparations();
                    ShowAlert("Something went wrong, please refresh and try again", "alert-danger", 8000);

                    //alert("ERROR on DELETE skill: " + jqXHR.status + " | " + jqXHR.statusText);
                }
            });
        });


        OnDeleteBtnToggleDeleteMark();

        AttachEventsToSkillOparations();

        OnAddExpBtnClick();
        ConnectAddFieldsBtns();

        onCreateSkillTagNameInputValueChange();
        JQBarSetForCreatSkillField();

    } else {
        JQBarsSetupDisplay();

    }



});

function ShowAlert(message: string, type: string, fadeOutTimer: number) {

    let alert = $("#skillAlert");
    alert.addClass(type);
    alert.show("fast");
    alert.text(`${message}`);
    setTimeout(() => {

        alert.hide("slow");
    }, fadeOutTimer);

}

function onCreateSkillTagNameInputValueChange() {
    let inputTagName = $("#createSkillTagNameInput");
    let levelRating = $('#createSkillLevelRating');
    let addBtn = $("#createNewSkillBtn");
    let addBtnHtml = addBtn.get(0) as HTMLButtonElement;
    let createNewSkillForm = $("#createNewSkillForm");

    addBtnHtml.disabled = true;

    inputTagName.on("input", function () {
        if ((inputTagName.get(0) as HTMLInputElement).value == "") {
            addBtnHtml.disabled = true;
        } else {
            addBtnHtml.disabled = false;
        }
    });


    createNewSkillForm.on('submit', function () {

        setTimeout(function () {
            (inputTagName.get(0) as HTMLInputElement).value = "";
            (levelRating as any).barrating('set', "1");

        });

    });
}

function JQBarSetForCreatSkillField() {
    let rating: any = $(`#createSkillLevelRating`);
    let levelInput = $(`#createSkillLevelInput`).get(0) as HTMLInputElement;


    rating.barrating({
        theme: 'bars-square',
        showValues: true,
        showSelectedRating: false,
        onSelect: function (value, text, event) {
            levelInput.value = value;

        }
    });
}

function AttachEventsToSkillOparations() {

    let skills = $(".skill");
    for (var i = 0; i < skills.length; i++) {
        UpdateSkillOnLoseFocus(i);
        JQBarsSetupEditing(i)
        OnClickSendDeleteSkillData(i);
    }
}


function UpdateSkillOnLoseFocus(index: number) {

    let form = $(`#skillEditForm_${index}`)

    $(`#skillTagName_${index}`).blur(function () {
        form.submit();
    });
}

function JQBarsSetupEditing(index: number) {

    let rating: any = $(`#rating_${index}`);
    let levelInput = $(`#skillLevel_${index}`).get(0) as HTMLInputElement;
    let form = $(`#skillEditForm_${index}`)

    rating.barrating({
        theme: 'bars-square',
        showValues: true,
        showSelectedRating: false,
        onSelect: function (value, text, event) {
            levelInput.value = value;
            form.submit();
        }
    });
}

function JQBarsSetupDisplay() {

    let skills = $(".skill");
    for (var i = 0; i < skills.length; i++) {
        let rating: any = $(`#ratingStatic_${i}`);

        rating.barrating({
            theme: 'bars-square',
            showValues: true,
            showSelectedRating: false,
            readonly: true
        });
    }

}

function OnClickSendDeleteSkillData(index: number) {

    let btn = $(`#calldeletModal_${index}`);
    let dataId = btn.data("id");
    let dataTageName = btn.data("tagName");

    btn.on("click", () => {
        skillId = dataId;
        $("#modelDeleteHeaderTitle").text(`Delete ${dataTageName} skill?`);
    });
}


function ConnectAddFieldsBtns() {

    let expGrps = $(".experience-section");
    for (var i = 0; i < expGrps.length; i++) {
        let addPointBtn = $("#addPoint_" + i + "");
        let sectionID = $("#experienceSectionID_" + i + "").get(0) as HTMLInputElement;
        let expSection = $("#experienceSection_" + i + "");

        UpdateWithNewPointField(addPointBtn, sectionID.value);


        let points = expSection.find(".point-section");
        for (var j = 0; j < points.length; j++) {
            let addDescBtn = $("#addDesc_" + i + "_" + j + "");
            let pointSectionID = $("#pointSectionID_" + i + "_" + j + "").get(0) as HTMLInputElement;

            UpdateWithNewDescField(addDescBtn, sectionID.value, pointSectionID.value);
        }
    }
}


function OnDeleteBtnToggleDeleteMark() {

    function toggleInputValueOnBtnClick(delBtn: JQuery<HTMLElement>, inputField: JQuery<HTMLElement> ,toggler: boolean) {
        delBtn.click(() => {

            console.log("Works");
            toggler = !toggler;
            inputField.prop('checked', toggler);

            if (toggler == true) {
                delBtn.removeClass("btn-warning");
                delBtn.addClass("btn-danger");
            } else {
                delBtn.removeClass("btn-danger");
                delBtn.addClass("btn-warning");
            }
        });
    }

    let expGrps = $(".experience-section");
    for (var i = 0; i < expGrps.length; i++) {


        let delExpBtn = $(`#deleteExpToggleMark_${i}`);
        let markCheckBox = $(`#deleteExpMarkInput_${i}`);
        let toggler = false;

        toggleInputValueOnBtnClick(delExpBtn, markCheckBox, toggler);
  

        let expSection = $("#experienceSection_" + i + "");
        let points = expSection.find(".point-section");
        for (var j = 0; j < points.length; j++) {

            let delPointBtnToggle = $(`#deletePointToggleMark_${i}_${j}`);
            let delPointMarkInput = $(`#deletePointMarkInput_${i}_${j}`);
            let toggler = false;

            toggleInputValueOnBtnClick(delPointBtnToggle, delPointMarkInput, toggler);

            let descSections = $(".desc-section");
            for (var k = 0; k < descSections.length; k++) {

                let delDescBtnToggle = $(`#deleteDescToggleBtn${i}_${j}_${k}`);
                let delDescMarkInput = $(`#delDescMarkInput_${i}_${j}_${k}`);
                let toggler = false;
     
                toggleInputValueOnBtnClick(delDescBtnToggle, delDescMarkInput, toggler);

            }

        }

    }

}

function OnAddExpBtnClick() {

    $("#addNewExpSection").on("click", () => {

        ($(this).get(0) as HTMLButtonElement).disabled = true;
       
        $("#exp-grp-container").load("/Home/AddEXP", (responseText, textStatus, jqXHR) => {

            if (textStatus == "success") {
                OnAddExpBtnClick();
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
              
                ShowAlert("Add new Experience section", "alert-success", 3000);

            }
            if (textStatus == "error") {
                OnAddExpBtnClick();
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
                alert(" On Creating new Experience section | something went wrong =" + jqXHR.status);

                ($(this).get(0) as HTMLButtonElement).disabled = false;
            }
        });

    });
}

function UpdateWithNewDescField(addDescbtn: JQuery<HTMLElement>, sectionID: string, pointSectionId: string) {

    if (sectionID == undefined) {
        alert("Error: Cant find every experience section, pleas try to refresh or contact admins");
        return;
    }

    if (pointSectionId == undefined) {
        alert("Error: Cant find every experience highlight section, pleas try to refresh or contact admins");
        return;
    }

    addDescbtn.on("click", () => {

        ($(this).get(0) as HTMLButtonElement).disabled = true;

        $("#exp-grp-container").load("/Home/AddDescFieldToExperienceView", { expGrpId: sectionID, pointId: pointSectionId }, (responseText, textStatus, jqXHR) => {

            if (textStatus == "success") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
            }
            if (textStatus == "error") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
                alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
            }

            ($(this).get(0) as HTMLButtonElement).disabled = false;
        });

    });
}


function UpdateWithNewPointField(addPointbtn: JQuery<HTMLElement>, sectionID: string) {
    if (sectionID == undefined) {
        alert("Error: Cant find every experience section, pleas try to refresh or contact admins");
        return;
    }

    addPointbtn.on("click", () => {

       // $("#editExpFrom").submit();

        ($(this).get(0) as HTMLButtonElement).disabled = true;
        $("#exp-grp-container").load("/Home/AddpointFieldToExperienceView", { expGrpId: sectionID }, (responseText, textStatus, jqXHR) => {

            if (textStatus == "success") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
            }
            if (textStatus == "error") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
                alert("Something went wrong code : " + jqXHR.status + " | " + jqXHR.statusText);
            }

            ($(this).get(0) as HTMLButtonElement).disabled = false;
        });
    });
}

// Using HTML onClick Inline Event ! 
function confirmItemDelete(uniqueId, isDeleteClicked) {
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

//function AddExpPointFieldModal(expPointCounter: number, addExpPointBtn: JQuery<HTMLElement>, expFrom: HTMLFormElement) {

//    let parentIndex = expPointCounter;
//    let descCounter = 0;

//    let inputExpPointMaxLength = 30; // Get this from the server MaxLentgth Attribute
//    let inputExpPointHTML = "<input type='text' \
//        placeholder = 'PointTitle["+ parentIndex + "]' \
//        class = 'exp-point-title' \
//        data-val='true' \
//        data-val-maxlength='The field PointTitle must be a string or array type with a maximum length of "+ inputExpPointMaxLength + ".' \
//        data-val-maxlength-max='"+ inputExpPointMaxLength + "' data-val-required='The PointTitle field is required.' \
//        id = 'NewExpGrp_ExpPoints_" + parentIndex + "__PointTitle' \
//        maxlength = '"+ inputExpPointMaxLength + "' \
//        name = 'NewExpGrp.ExpPoints["+ parentIndex + "].PointTitle' value>";

//    let spanValidationExpPointTitle = "<span \
//        class='text-danger field-validation-valid' \
//        data-valmsg-for= 'NewExpGrp.ExpPoints["+ parentIndex + "].PointTitle' \
//        data-valmsg-replace='true'></span>";


//    let addDiscriptionBtnHTML = $("<button id='point" + parentIndex + "AddDescBtn' type = 'button'> Add Desc </button>");
//    let removeDiscriptionBtnHTML = $("<button id='point" + parentIndex + "RemoveDescBtn' type = 'button'> Remove Desc </button>");
//    let expPointDiv = $("<div id='point-" + parentIndex + "' class='exp-point-section m-3 p-2'></div>");

//    expPointDiv.append(inputExpPointHTML);
//    expPointDiv.append(spanValidationExpPointTitle);
//    expPointDiv.append(addDiscriptionBtnHTML);
//    expPointDiv.append(removeDiscriptionBtnHTML);

//    addExpPointBtn.before(expPointDiv);

//    AddDescriptionFieldModal(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom); //  we crate 1 desc field on spawn as it is an mandatory requirement for a point
//    descCounter++

//    addDiscriptionBtnHTML.on('click', () => {

//        AddDescriptionFieldModal(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom);
//        descCounter++;
//    });

//    removeDiscriptionBtnHTML.on('click', () => {
//        if (descCounter >= 2) {
//            RemoveDescField(expPointDiv);
//            descCounter--;
//        } else {
//            alert("Cant remove description! You need at least 1 description with a highlight to create a experience group | this is a temp window"); // TODO change removeExpPointBtn() from using inline warning rather then an Alert window
//        }
//    });

//    ResetFormValidationJQUnobtrusive(expFrom);
//}


//function AddDescriptionFieldModal(descCounter: number, parentIndex: number, spawnPosition: JQuery<HTMLElement>, expFrom: HTMLFormElement) {
//    let inputDescMaxLength = 60; // Get this from the server MaxLentgth Attribute

//    var inputDescriptionHTML = " <input type='text' \
//            placeholder ='desc ["+ descCounter + "]' \
//            class = 'exp-point-desc' \
//            data-val='true' \
//            data-val-maxlength='The field Desc must be a string or array type with a maximum length of '"+ inputDescMaxLength + "'.' \
//            data-val-maxlength-max='"+ inputDescMaxLength + "' \
//            data-val-required='The Desc field is required.' \
//            id = 'NewExpGrp_ExpPoints_"+ parentIndex + "__Descriptions_" + descCounter + "__Desc' \
//            maxlength = '"+ inputDescMaxLength + "' \
//            name = 'NewExpGrp.ExpPoints["+ parentIndex + "].Descriptions[" + descCounter + "].Desc' > ";

//    let spanValidationExpPointDesc = "<span \
//            class='text-danger field-validation-valid' \
//            data-valmsg-for= 'NewExpGrp.ExpPoints["+ parentIndex + "].Descriptions[" + descCounter + "].Desc' \
//            data-valmsg-replace='true'></span>";

//    let descContainer = $("<div id='desc-container-" + descCounter + "' class='desc-container m-1 p-1'></div>")

//    spawnPosition.before(descContainer);
//    descContainer.append(inputDescriptionHTML);
//    descContainer.append(spanValidationExpPointDesc);

//    ResetFormValidationJQUnobtrusive(expFrom);

//}


//function RemoveExpPointField() {

//    let expPointContainers = $(".exp-point-section");
//    expPointContainers[expPointContainers.length - 1].remove();
//}

//function RemoveDescField(expPoint: JQuery<HTMLElement>) {
//    let descFields = expPoint.children(".desc-container");
//    descFields[descFields.length - 1].remove();
//}


//function ResetFormValidationJQUnobtrusive(formTag: HTMLFormElement) {

//    /* The client side validation messages connected bu (JQ unobtrusive) to our DataAnnotations attributes on the server side are only loaded on page load/refresh .
//     * So they won't work on AJAX requests, unless we force a reset on the form.
//     */

//    $(formTag).removeData("validator")
//        .removeData("unobtrusiveValidation");
//    $.validator.unobtrusive.parse(formTag);
//}

//function OnBeginCreateEXP() {
//    let inputBtn = $("#expCreateSubmitBtn").get(0) as HTMLInputElement // This should be cached but in global?
//    inputBtn.disabled = true;
//}

//function OnCompleteCreateEXP() {
//    let inputBtn = $("#expCreateSubmitBtn").get(0) as HTMLInputElement // // This should be cached but in global?
//    inputBtn.disabled = false;
//}

//function OnSuccessfulCreateEXP(xhr: XMLHttpRequest) { // This only fires on 200
//    let form = $("#newExpFrom")[0] as HTMLFormElement;
//    form.reset();

//    let expPointContainers = $(".exp-point-section");
//    for (var i = expPointContainers.length - 1; i > 0; i--) {
//        expPointContainers[i].remove();
//    }

//    $("#newExperienceModal").modal('hide');
//}

function OnSuccessfulEditEXP() { // Successful

    OnDeleteBtnToggleDeleteMark();
    ConnectAddFieldsBtns();
    OnAddExpBtnClick();

    ShowAlert("Sections Updated!", "alert-success", 4000);
}

function OnFailureEditEXP(xhr: XMLHttpRequest) {
    OnDeleteBtnToggleDeleteMark();
    ConnectAddFieldsBtns();
    OnAddExpBtnClick();
    alert(" On Editing | something went wrong, please refresh and try again");
} 

function OnSuccessCreatNewSkill() {

    AttachEventsToSkillOparations();
    ShowAlert("Request to create new skill sent!", "alert-success", 4000);
}
function OnFailCreatNewSkill() {

    AttachEventsToSkillOparations();
    ShowAlert("Something went wrong, please refresh and try again", "alert-danger", 8000);


    // If the jQuery Unobtrusive AJAX fails the div is not updated, which means no validation messages. we can manually refresh but its not intuitive
    //$("#skillDisplayContainer").load("/Home/GetSkillsContainerEditing", (responseText, textStatus, jqXHR) => {
    //    if (textStatus == "error") {
    //        alert("Failed ACTIVE RELOAD = " + jqXHR.status + " | " + jqXHR.statusText);
    //        AttachEventsToSkillOparations();

    //    }

    //    if (textStatus == "success") {
    //        alert("Success ACTIVE RELOAD = " + jqXHR.status + " | " + jqXHR.statusText);
    //        AttachEventsToSkillOparations();

    //    }
    //});

}

function OnSuccessEditSkill(jqXHR: JQueryXHR) {

    AttachEventsToSkillOparations();
}
function OnFailEditSkill(jqXHR: JQueryXHR) {

    AttachEventsToSkillOparations();
    // alert(`FAIL | EDIT skill | ${jqXHR.status} , ${jqXHR.statusText}`);
}



//function OnFailureCreateEXP(xhr: XMLHttpRequest) { // jQuery XMLHttpRequest type ?
//    //$("#newExperienceModal").load("/Home/UserResume/XXXXXXXXXXXX", (responseText, textStatus, jqXHR) => {
//    //    if (textStatus == "error") {
//    //        alert("Failed to load Resume = " + jqXHR.status + " | " + xhr.statusText);
//    //    }

//    //    if (textStatus == "success") {
//    //        alert("Success to load Resume ");
//    //    }
//    //});

//    if (xhr.status == 400) {
//        // TODO make this into a validation error ResumeControles.ts 328 and remove hard coded values
//        alert("Error you might have gone over the max limit of experience(6), highlight(6) or description(6) sections. | Status : " + xhr.status + " | Text = " + xhr.statusText);

//    } else {

//        alert("On Create something went wrong | Status : " + xhr.status + " | Text = " + xhr.statusText);
//    }

//}

