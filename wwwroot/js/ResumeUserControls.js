$(document).ready(function () {
    var addExpPointBtn = $("#addExpPointBtn");
    var removeExpPointBtn = $("#removeExpPointBtn");
    var formCreateNewExp = $("#newExpFrom")[0];
    var expPointCounter = 0;
    AddExpPointFieldModal(expPointCounter, addExpPointBtn, formCreateNewExp);
    expPointCounter++;
    addExpPointBtn.on("click", function () {
        AddExpPointFieldModal(expPointCounter, addExpPointBtn, formCreateNewExp);
        expPointCounter++;
    });
    removeExpPointBtn.on("click", function () {
        if (expPointCounter >= 2) {
            RemoveExpPointField();
            expPointCounter--;
        }
        else {
            alert("Cant remove highlight! You need at least 1 highlight with a description to create a experience group | this is a temp window"); // TODO change removeExpPointBtn() from using inline warning rather then an Alert window
        }
    });
    $(document).ajaxSuccess(function (event, xhr, settings) {
        if (settings.url == formCreateNewExp.action) {
            console.log("Creating new Grp");
            $("#exp-grp-container").load("/Home/GetExperienceView", function (responseText, textStatus, jqXHR) {
                if (textStatus == "error") {
                    var msg = "Sorry but there was an error: ";
                    ConnectAddFieldsBtns();
                    alert(msg + jqXHR.status + " " + jqXHR.statusText);
                }
                if (textStatus == "success") {
                    ConnectAddFieldsBtns();
                    alert("Crate an pop-up to indicate the creation was successful");
                }
            });
            // GenerateMainPageHTML(xhr);
            expPointCounter = 1; // we set this to 1 because 0 index is spawned at the start of the page
        }
    });
    UpdateWithNewExpGrp();
    ConnectAddFieldsBtns();
});
//function ConnectAddDescBtn() { 
//    let pointSections = $(".point-section");
//    for (var i = 0; i < pointSections.length; i++) {
//        console.log("Points = " +i)
//    }
//}
function ConnectAddFieldsBtns() {
    var expGrps = $(".experience-section");
    for (var i = 0; i < expGrps.length; i++) {
        var addPointBtn = $("#addPoint_" + i + "");
        var sectionID = $("#experienceSectionID_" + i + "").get(0);
        var expSection = $("#experienceSection_" + i + "");
        UpdateWithNewPointField(addPointBtn, sectionID.value);
        var points = expSection.children(".point-section");
        for (var j = 0; j < points.length; j++) {
            var desc = points.children(".desc-section");
            var addDescBtn = $("#addDesc_" + i + "_" + j + "");
            var pointSectionID = $("#pointSectionID_" + i + "_" + j + "").get(0);
            UpdateWithNewDescField(addDescBtn, sectionID.value, pointSectionID.value);
        }
    }
}
function UpdateWithNewExpGrp() {
    $("#addNewExpSection").on("click", function () {
        console.log("Adding new EXP");
        $("#exp-grp-container").load("/Home/AddEXP", function (responseText, textStatus, jqXHR) {
            if (textStatus == "error") {
                alert("ERROR creating EXP : " + jqXHR.status + " | " + jqXHR.statusText);
            }
            if (textStatus == "success") {
                alert("SUCCESS creating EXP : " + jqXHR.status + " | " + jqXHR.statusText);
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
        // console.log(`section = ${sectionID} point = ${pointSectionId}`);
        $("#exp-grp-container").load("/Home/AddDescFieldToExperienceView", { expGrpId: sectionID, pointId: pointSectionId }, function (responseText, textStatus, jqXHR) {
            if (textStatus == "error") {
                ConnectAddFieldsBtns();
                alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
            }
            if (textStatus == "success") {
                ConnectAddFieldsBtns();
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
            if (textStatus == "error") {
                ConnectAddFieldsBtns();
                alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
            }
            if (textStatus == "success") {
                ConnectAddFieldsBtns();
            }
        });
    });
}
function GenerateMainPageHTML(xhr) {
    if (xhr != undefined) { // or Null ?
        // we need to crate a class for the newExpGrpObject or else it will be an any type
        var newExpGrpObject = xhr.responseJSON.newExpGrp; // this works LEL
        //console.log(newExpGrpObject);
        // Use json object (newExpGrpObject)
        // dynamic html construction
        // ajax into partial view ?
        // generate fields using data (newExpGrpObject) pass this as model
    }
    else {
        // AJAX Get JSON From SEVER
        // use json for dynamic html construction
    }
}
function AddExpPointFieldModal(expPointCounter, addExpPointBtn, expFrom) {
    var parentIndex = expPointCounter;
    var descCounter = 0;
    var inputExpPointMaxLength = 30; // Get this from the server MaxLentgth Attribute
    var inputExpPointHTML = "<input type='text' \
        placeholder = 'PointTitle[" + parentIndex + "]' \
        class = 'exp-point-title' \
        data-val='true' \
        data-val-maxlength='The field PointTitle must be a string or array type with a maximum length of " + inputExpPointMaxLength + ".' \
        data-val-maxlength-max='" + inputExpPointMaxLength + "' data-val-required='The PointTitle field is required.' \
        id = 'NewExpGrp_ExpPoints_" + parentIndex + "__PointTitle' \
        maxlength = '" + inputExpPointMaxLength + "' \
        name = 'NewExpGrp.ExpPoints[" + parentIndex + "].PointTitle' value>";
    var spanValidationExpPointTitle = "<span \
        class='text-danger field-validation-valid' \
        data-valmsg-for= 'NewExpGrp.ExpPoints[" + parentIndex + "].PointTitle' \
        data-valmsg-replace='true'></span>";
    var addDiscriptionBtnHTML = $("<button id='point" + parentIndex + "AddDescBtn' type = 'button'> Add Desc </button>");
    var removeDiscriptionBtnHTML = $("<button id='point" + parentIndex + "RemoveDescBtn' type = 'button'> Remove Desc </button>");
    var expPointDiv = $("<div id='point-" + parentIndex + "' class='exp-point-section m-3 p-2'></div>");
    expPointDiv.append(inputExpPointHTML);
    expPointDiv.append(spanValidationExpPointTitle);
    expPointDiv.append(addDiscriptionBtnHTML);
    expPointDiv.append(removeDiscriptionBtnHTML);
    addExpPointBtn.before(expPointDiv);
    AddDescriptionFieldModal(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom); //  we crate 1 desc field on spawn as it is an mandatory requirement for a point
    descCounter++;
    addDiscriptionBtnHTML.on('click', function () {
        AddDescriptionFieldModal(descCounter, parentIndex, addDiscriptionBtnHTML, expFrom);
        descCounter++;
    });
    removeDiscriptionBtnHTML.on('click', function () {
        if (descCounter >= 2) {
            RemoveDescField(expPointDiv);
            descCounter--;
        }
        else {
            alert("Cant remove description! You need at least 1 description with a highlight to create a experience group | this is a temp window"); // TODO change removeExpPointBtn() from using inline warning rather then an Alert window
        }
    });
    ResetFormValidationJQUnobtrusive(expFrom);
}
function AddDescriptionFieldModal(descCounter, parentIndex, spawnPosition, expFrom) {
    var inputDescMaxLength = 60; // Get this from the server MaxLentgth Attribute
    var inputDescriptionHTML = " <input type='text' \
            placeholder ='desc [" + descCounter + "]' \
            class = 'exp-point-desc' \
            data-val='true' \
            data-val-maxlength='The field Desc must be a string or array type with a maximum length of '" + inputDescMaxLength + "'.' \
            data-val-maxlength-max='" + inputDescMaxLength + "' \
            data-val-required='The Desc field is required.' \
            id = 'NewExpGrp_ExpPoints_" + parentIndex + "__Descriptions_" + descCounter + "__Desc' \
            maxlength = '" + inputDescMaxLength + "' \
            name = 'NewExpGrp.ExpPoints[" + parentIndex + "].Descriptions[" + descCounter + "].Desc' > ";
    var spanValidationExpPointDesc = "<span \
            class='text-danger field-validation-valid' \
            data-valmsg-for= 'NewExpGrp.ExpPoints[" + parentIndex + "].Descriptions[" + descCounter + "].Desc' \
            data-valmsg-replace='true'></span>";
    var descContainer = $("<div id='desc-container-" + descCounter + "' class='desc-container m-1 p-1'></div>");
    spawnPosition.before(descContainer);
    descContainer.append(inputDescriptionHTML);
    descContainer.append(spanValidationExpPointDesc);
    ResetFormValidationJQUnobtrusive(expFrom);
}
function RemoveExpPointField() {
    var expPointContainers = $(".exp-point-section");
    expPointContainers[expPointContainers.length - 1].remove();
}
function RemoveDescField(expPoint) {
    var descFields = expPoint.children(".desc-container");
    descFields[descFields.length - 1].remove();
}
function ResetFormValidationJQUnobtrusive(formTag) {
    /* The client side validation messages connected bu (JQ unobtrusive) to our DataAnnotations attributes on the server side are only loaded on page load/refresh .
     * So they won't work on AJAX requests, unless we force a reset on the form.
     */
    $(formTag).removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(formTag);
}
function OnBeginCreateEXP() {
    var inputBtn = $("#expCreateSubmitBtn").get(0); // This should be cached but in global?
    inputBtn.disabled = true;
}
function OnCompleteCreateEXP() {
    var inputBtn = $("#expCreateSubmitBtn").get(0); // // This should be cached but in global?
    inputBtn.disabled = false;
}
function OnSuccessfulCreateEXP(xhr) {
    var form = $("#newExpFrom")[0];
    form.reset();
    var expPointContainers = $(".exp-point-section");
    for (var i = expPointContainers.length - 1; i > 0; i--) {
        expPointContainers[i].remove();
    }
    $("#newExperienceModal").modal('hide');
}
function OnSuccessfulEditEXP() {
    $("#exp-grp-container").load("/Home/GetExperienceView", function (responseText, textStatus, jqXHR) {
        if (textStatus == "error") {
            ConnectAddFieldsBtns();
            alert("Something went wrong  code : " + jqXHR.status + " | " + jqXHR.statusText);
        }
        if (textStatus == "success") {
            ConnectAddFieldsBtns();
        }
    });
    alert("TEMP successful edit");
}
function OnFailureEditEXP(xhr) {
    alert("On EDIT something went wrong | Status : " + xhr.status + " | Text = " + xhr.statusText);
}
function OnFailureCreateEXP(xhr) {
    //$("#newExperienceModal").load("/Home/UserResume/XXXXXXXXXXXX", (responseText, textStatus, jqXHR) => {
    //    if (textStatus == "error") {
    //        alert("Failed to load Resume = " + jqXHR.status + " | " + xhr.statusText);
    //    }
    //    if (textStatus == "success") {
    //        alert("Success to load Resume ");
    //    }
    //});
    if (xhr.status == 400) {
        // TODO make this into a validation error ResumeControles.ts 328 and remove hard coded values
        alert("Error you might have gone over the max limit of experience(6), highlight(6) or description(6) sections. | Status : " + xhr.status + " | Text = " + xhr.statusText);
    }
    else {
        alert("On Create something went wrong | Status : " + xhr.status + " | Text = " + xhr.statusText);
    }
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