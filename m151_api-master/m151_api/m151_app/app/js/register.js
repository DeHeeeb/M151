checkIfUserIsAlreadyAuthorized();
$(document).ready(function () {
    $("#register").click(function () {
        var btn = $(this).button('loading');
        var formData = objectifyForm($("#registerForm").serializeArray());
        $.post(BASEURL + "/customers", JSON.stringify(formData))
            .done(function (customer) {
                window.location.replace("login.html?email=" + formData.email);
            })
            .fail(function (response) {
                var statusCode = response.status;
                statusCodeHandler(statusCode);
            }).always(function () {
            btn.button("reset");
        });
    });
});
