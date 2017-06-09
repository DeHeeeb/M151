checkIfUserIsAlreadyAuthorized();
$(document).ready(function () {
    fillOutForm();
    $("#login").click(function () {
        var btn = $(this).button('loading');
        var formData = JSON.stringify(objectifyForm($("#loginForm").serializeArray()));

        $.post(BASEURL + "/login", formData)
            .done(function (token) {
                Cookies.set('token', token.value);
                window.location.replace("lend.html");
            })
            .fail(function (response) {
                var statusCode = response.status;
                console.dir(statusCode);
                if (statusCode === 401) {
                    unauthorized();
                } else {
                    statusCodeHandler(statusCode)
                }
            }).always(function () {
            btn.button("reset");
        });
    });
    function fillOutForm() {
        var grabbedEmail = getParameterByName("email");
        if (grabbedEmail !== null)
            $("#email").val(grabbedEmail);
    }

    function unauthorized() {
        $("#loginForm").addClass("has-error");
        $.notify({
            message: 'Wrong username and/or password!'
        }, {
            type: 'danger'
        });
    }
});



