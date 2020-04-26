$(document).ready(() => {

    //const addExpPointBtn = $("#addExpPointBtn");
    //const removeExpPointBtn = $("#removeExpPointBtn");
    //const formCreateNewExp = $("#newExpFrom")[0] as HTMLFormElement;
    //let expPointCounter = 0;

    //AddExpPointFieldModal(expPointCounter, addExpPointBtn, formCreateNewExp);
    //expPointCounter++;

    //addExpPointBtn.on("click", () => {

    //    AddExpPointFieldModal(expPointCounter, addExpPointBtn, formCreateNewExp);
    //    expPointCounter++;

    //});

    //removeExpPointBtn.on("click", () => {
    //    if (expPointCounter >= 2) {
    //        RemoveExpPointField();
    //        expPointCounter--;
    //    } else {
    //        alert("Cant remove highlight! You need at least 1 highlight with a description to create a experience group | this is a temp window"); // TODO change removeExpPointBtn() from using inline warning rather then an Alert window
    //    }
    //});


    //$(document).ajaxSuccess(function (event, xhr, settings) { // Because of AJAX unobtrusive we need to use this global event on ajax success and filter out witch one succeeded

    //    if (settings.url == formCreateNewExp.action) {
    //        console.log("Creating new Grp");

    //        $("#exp-grp-container").load("/Home/GetExperienceView", (responseText, textStatus, jqXHR) => {
    //            if (textStatus == "error") {
    //                let msg = "Sorry but there was an error: ";
    //                ConnectAddFieldsBtns();
    //                alert(msg + jqXHR.status + " " + jqXHR.statusText);
    //            }

    //            if (textStatus == "success") {
    //                ConnectAddFieldsBtns();
    //                alert("Crate an pop-up to indicate the creation was successful");
    //            }
    //        });


    //        expPointCounter = 1; // we set this to 1 because 0 index is spawned at the start of the page
    //    }

    //});
    //let skills = $(".skill");
    //let skillId = "";

    //for (var i = 0; i < skills.length; i++) {
    //    let btn = $(`#calldeletModal_${i}`);
    //    let dataId = btn.data("id");

    //    btn.on("click", () => {
    //        skillId = dataId;
    //        console.log("Sending id" + skillId);
    //    });
    //}
});