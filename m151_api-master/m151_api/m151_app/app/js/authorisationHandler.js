$.ajaxSetup({async: false});
var token = Cookies.get("token");
if (token != null)
    $.get(BASEURL + "/isTokenValid/" + token)
        .fail(function () {
            window.location.replace("/");
        });
else
    window.location.replace("/");
$.ajaxSetup({async: true});
