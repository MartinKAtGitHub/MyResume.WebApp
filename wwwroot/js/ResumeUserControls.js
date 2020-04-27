var skillId = "";
var editMode;
$(document).ready(function () {
    if (editMode == true) {
        $("#deleteSkillModalBtn").on("click", function () {
            $("#skillDisplayContainer").load("/Home/DeleteSkill", { id: skillId }, function (responseText, textStatus, jqXHR) {
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
    }
    else {
        JQBarsSetupDisplay();
    }
});
function ShowAlert(message, type, fadeOutTimer) {
    var alert = $("#skillAlert");
    alert.addClass(type);
    alert.show("fast");
    alert.text("" + message);
    setTimeout(function () {
        alert.hide("slow");
    }, fadeOutTimer);
}
function onCreateSkillTagNameInputValueChange() {
    var inputTagName = $("#createSkillTagNameInput");
    var levelRating = $('#createSkillLevelRating');
    var addBtn = $("#createNewSkillBtn");
    var addBtnHtml = addBtn.get(0);
    var createNewSkillForm = $("#createNewSkillForm");
    addBtnHtml.disabled = true;
    inputTagName.on("input", function () {
        if (inputTagName.get(0).value == "") {
            addBtnHtml.disabled = true;
        }
        else {
            addBtnHtml.disabled = false;
        }
    });
    createNewSkillForm.on('submit', function () {
        setTimeout(function () {
            inputTagName.get(0).value = "";
            levelRating.barrating('set', "1");
        });
    });
}
function JQBarSetForCreatSkillField() {
    var rating = $("#createSkillLevelRating");
    var levelInput = $("#createSkillLevelInput").get(0);
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
    var skills = $(".skill");
    for (var i = 0; i < skills.length; i++) {
        UpdateSkillOnLoseFocus(i);
        JQBarsSetupEditing(i);
        OnClickSendDeleteSkillData(i);
    }
}
function UpdateSkillOnLoseFocus(index) {
    var form = $("#skillEditForm_" + index);
    $("#skillTagName_" + index).blur(function () {
        form.submit();
    });
}
function JQBarsSetupEditing(index) {
    var rating = $("#rating_" + index);
    var levelInput = $("#skillLevel_" + index).get(0);
    var form = $("#skillEditForm_" + index);
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
    var skills = $(".skill");
    for (var i = 0; i < skills.length; i++) {
        var rating = $("#ratingStatic_" + i);
        rating.barrating({
            theme: 'bars-square',
            showValues: true,
            showSelectedRating: false,
            readonly: true
        });
    }
}
function OnClickSendDeleteSkillData(index) {
    var btn = $("#calldeletModal_" + index);
    var dataId = btn.data("id");
    var dataTageName = btn.data("tagName");
    btn.on("click", function () {
        skillId = dataId;
        $("#modelDeleteHeaderTitle").text("Delete " + dataTageName + " skill?");
    });
}
function ConnectAddFieldsBtns() {
    var expGrps = $(".experience-section");
    for (var i = 0; i < expGrps.length; i++) {
        var addPointBtn = $("#addPoint_" + i + "");
        var sectionID = $("#experienceSectionID_" + i + "").get(0);
        var expSection = $("#experienceSection_" + i + "");
        UpdateWithNewPointField(addPointBtn, sectionID.value);
        var points = expSection.find(".point-section");
        for (var j = 0; j < points.length; j++) {
            var addDescBtn = $("#addDesc_" + i + "_" + j + "");
            var pointSectionID = $("#pointSectionID_" + i + "_" + j + "").get(0);
            UpdateWithNewDescField(addDescBtn, sectionID.value, pointSectionID.value);
        }
    }
}
function OnDeleteBtnToggleDeleteMark() {
    function toggleInputValueOnBtnClick(delBtn, inputField, toggler) {
        delBtn.click(function () {
            console.log("Works");
            toggler = !toggler;
            inputField.prop('checked', toggler);
            if (toggler == true) {
                delBtn.removeClass("btn-warning");
                delBtn.addClass("btn-danger");
            }
            else {
                delBtn.removeClass("btn-danger");
                delBtn.addClass("btn-warning");
            }
        });
    }
    var expGrps = $(".experience-section");
    for (var i = 0; i < expGrps.length; i++) {
        var delExpBtn = $("#deleteExpToggleMark_" + i);
        var markCheckBox = $("#deleteExpMarkInput_" + i);
        var toggler = false;
        toggleInputValueOnBtnClick(delExpBtn, markCheckBox, toggler);
        var expSection = $("#experienceSection_" + i + "");
        var points = expSection.find(".point-section");
        for (var j = 0; j < points.length; j++) {
            var delPointBtnToggle = $("#deletePointToggleMark_" + i + "_" + j);
            var delPointMarkInput = $("#deletePointMarkInput_" + i + "_" + j);
            var toggler_1 = false;
            toggleInputValueOnBtnClick(delPointBtnToggle, delPointMarkInput, toggler_1);
            var descSections = $(".desc-section");
            for (var k = 0; k < descSections.length; k++) {
                var delDescBtnToggle = $("#deleteDescToggleBtn" + i + "_" + j + "_" + k);
                var delDescMarkInput = $("#delDescMarkInput_" + i + "_" + j + "_" + k);
                var toggler_2 = false;
                toggleInputValueOnBtnClick(delDescBtnToggle, delDescMarkInput, toggler_2);
            }
        }
    }
}
function OnAddExpBtnClick() {
    $("#addNewExpSection").on("click", function () {
        $("#exp-grp-container").load("/Home/AddEXP", function (responseText, textStatus, jqXHR) {
            if (textStatus == "success") {
                OnAddExpBtnClick();
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
                //alert("SUCCESS creating EXP : " + jqXHR.status + " | " + jqXHR.statusText);
            }
            if (textStatus == "error") {
                OnAddExpBtnClick();
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
                alert(" On Creating new Experience section | something went wrong, please refresh and try again");
            }
        });
    });
}
function UpdateWithNewDescField(addDescbtn, sectionID, pointSectionId) {
    if (sectionID == undefined) {
        alert("Error: Cant find every experience section, pleas try to refresh or contact admins");
        return;
    }
    if (pointSectionId == undefined) {
        alert("Error: Cant find every experience highlight section, pleas try to refresh or contact admins");
        return;
    }
    addDescbtn.on("click", function () {
        console.log("section = " + sectionID + " point = " + pointSectionId);
        $("#exp-grp-container").load("/Home/AddDescFieldToExperienceView", { expGrpId: sectionID, pointId: pointSectionId }, function (responseText, textStatus, jqXHR) {
            if (textStatus == "success") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
            }
            if (textStatus == "error") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
                alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
            }
        });
    });
}
function UpdateWithNewPointField(addPointbtn, sectionID) {
    if (sectionID == undefined) {
        alert("Error: Cant find every experience section, pleas try to refresh or contact admins");
        return;
    }
    addPointbtn.on("click", function () {
        $("#exp-grp-container").load("/Home/AddpointFieldToExperienceView", { expGrpId: sectionID }, function (responseText, textStatus, jqXHR) {
            if (textStatus == "success") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
            }
            if (textStatus == "error") {
                OnDeleteBtnToggleDeleteMark();
                ConnectAddFieldsBtns();
                alert("Something went wrong code : " + jqXHR.status + " | " + jqXHR.statusText);
            }
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
function OnSuccessfulEditEXP() {
    OnDeleteBtnToggleDeleteMark();
    ConnectAddFieldsBtns();
    OnAddExpBtnClick();
    //alert("TEMP | EDIT | Successful");
}
function OnFailureEditEXP(xhr) {
    OnDeleteBtnToggleDeleteMark();
    ConnectAddFieldsBtns();
    OnAddExpBtnClick();
    alert(" On Editing |something went wrong, please refresh and try again");
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
function OnSuccessEditSkill(jqXHR) {
    AttachEventsToSkillOparations();
}
function OnFailEditSkill(jqXHR) {
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
//# sourceMappingURL=ResumeUserControls.js.map