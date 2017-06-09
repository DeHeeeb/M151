$(document).ready(function () {
    polluteForm();
    $("#saveChanges").click(function () {
        var btn = $(this).button('loading');
        var formData = objectifyForm($("#profileForm").serializeArray());
        $.ajax({method: "PUT", url: BASEURL + "/customers", data: formData})
            .done(function () {
                $.notify({
                    message: 'Data successfully updated!'
                }, {
                    type: 'success'
                });
            })
            .fail(function (response) {
                var statusCode = response.status;
                statusCodeHandler(statusCode);
            }).always(function () {
            btn.button("reset");
        });
    });
    function polluteForm() {
        $.get(BASEURL + "/customers/" + Cookies.get("token"))
            .done(function (data) {
               populate($("#profileForm"), data)
            })
            .fail(function (response) {
                var statusCode = response.status;
                statusCodeHandler(statusCode);
            }).always(function () {
        });
    }

    function populate(frm, data) {
        $.each(data, function (key, value) {
            var ctrl = $('[name=' + key + ']', frm);
            switch (ctrl.prop("type")) {
                case "radio":
                case "checkbox":
                    ctrl.each(function () {
                        if ($(this).attr('value') == value) $(this).attr("checked", value);
                    });
                    break;
                default:
                    ctrl.val(value);
            }
        });
    }
});