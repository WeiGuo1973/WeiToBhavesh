/*Toggling between on and off images */
var timer1;
var isOn = '';
var isAdjusted = 0;
var isMoved = 0;
var isPromoed = 0;
var isDOTDheight = 0;
var _DOTDComplete = 0;

function TurnOnMenu(txtMenu) {
    $("#navbar_dropdown").attr("style", "z-index:5;");
    if (txtMenu != "") {
        isOn = txtMenu;
        if (txtMenu == "divBusiness") { $("#button_Business img").attr('src', '../Images/buttons_Business_on.png'); }
        if (txtMenu == "divResidents") { $("#button_Residents img").attr('src', '../Images/buttons_Residents_on.png'); }
        if (txtMenu == "divGovernment") { $("#button_Government img").attr('src', '../Images/buttons_Government_on.png'); }
        if (txtMenu == "divInside_LaDOTD") { $("#button_Inside_LaDOTD img").attr('src', '../Images/buttons_Inside_LaDOTD_on.png'); }
    }
    else { txtMenu = isOn; }

    if (txtMenu == "divPortalMenu") {
        $("#divPortalMenu").css("display", "inline");
    }
    else {
        var OuterMenuDiv = document.getElementById('navbar_dropdown');
        OuterMenuDiv.style.display = "inline";
        if (txtMenu != "") {
            var InnerMenuDiv = document.getElementById(txtMenu);
            InnerMenuDiv.style.display = "inline";
        }
    }
}


function TurnOffMenu() {
    $("#button_Business img").attr('src', '../Images/buttons_Business_off.png');
    $("#button_Residents img").attr('src', '../Images/buttons_Residents_off.png');
    $("#button_Government img").attr('src', '../Images/buttons_Government_off.png');
    $("#button_Inside_LaDOTD img").attr('src', '../Images/buttons_Inside_LaDOTD_off.png');

    $("#navbar_dropdown").css("display", "none");
    $("#navbar_dropdown").css("z-index", "-1");

    $("#divInside_LaDOTD").css("display", "none");
    $("#divGovernment").css("display", "none");
    $("#divBusiness").css("display", "none");
    $("#divResidents").css("display", "none");

    $("#divPortalMenu").css("display", "none");

    clearTimeout(timer1);
}

/*Search Functionality*/
$(document).ready(function () {
    $("#SearchTxt").on("keydown", function (e) {
        if (e.keyCode === 13) {
            performSearch();
        }
    });
});

function performSearch() {
    // Get the search text
    var srchTxt = document.getElementById('SearchTxt').value;

    // If we have a value, search for the text
    if (srchTxt.length > 0 && srchTxt != 'Search for Projects, Services, Manuals or Other Information')
        // window.location = "http://search.usa.gov/search?utf8=%E2%9C%93&sc=0&m=&affiliate=ladotd&commit=Search&query=" + srchTxt;
        window.location = "https://www.google.com/search?q=site%3Adotd.la.gov%20" + srchTxt;
}

