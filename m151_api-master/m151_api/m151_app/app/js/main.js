$(document).ready(function () {
    $("#logout").click(function () {
        Cookies.remove("token");
        window.location = "/";
    });
});
function statusCodeHandler(statusCode) {
    switch (statusCode) {
        case 500:
            internalServerError(statusCode);
            break;
    }
}
function internalServerError(statusCode) {
    alert("Server error: " + statusCode + "!");
}
function checkIfUserIsAlreadyAuthorized() {
    $.ajaxSetup({async: false});
    var token = Cookies.get("token");
    if (token != null)
        $.get(BASEURL + "/isTokenValid/" + token)
            .done(function () {
                window.location.replace("lend.html");
            });
    $.ajaxSetup({async: true});
}
function objectifyForm(formArray) {//serialize data function
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

var BASEURL = "http://localhost:56600/api";

$.ajaxSetup({
    headers: {
        "Authorization": Cookies.get("token"),
        "Content-Type": "application/json",
        dataType: "json"
    }
});