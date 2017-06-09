/// <reference path="authorisationHandler.js" />
var organisationToEdit;
$(document).ready(function () {
    var domOrganisationsTable = $('#organisations-table');
    var domOrganisationsModal = $("#organisations-modal");
    var domOrganisationsModalTitle = domOrganisationsModal.find(".modal-title");
    var domOrganisationsModalBody = domOrganisationsModal.find(".modal-body");
    var domOrganisationsModalFooter = domOrganisationsModal.find(".modal-footer");
    $.get(BASEURL + "/organisations").done(function (organisations) {
        $("#organisations-loader").fadeOut(function () {
            domOrganisationsTable.fadeIn();
            var organisationsTable = domOrganisationsTable.DataTable({
                columns: [
                    {data: "name"},
                    {data: "city"},
                    {data: "zip"},
                    {data: "street"},
                    {data: "streetnumber"},
                    {data: "edit"}
                ],
                responsive: true
            });
            organisations.forEach(function (organisations) {
                organisationsTable.row.add({
                    "name": organisations.name,
                    "city": organisations.location,
                    "zip": organisations.zip,
                    "street": organisations.street,
                    "streetnumber": organisations.nr,
                    "edit": "<button data-organisation-number='" + organisations.number + "' class='btn btn-primary open-edit-organisation'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span> Edit organisation</button>"
                });
            });
            organisationsTable.draw();
        });
    }).fail(function (response) {
        var statusCode = response.status;
        console.dir(statusCode);
        if (statusCode === 401) {
            window.location = "/";
        } else {
            statusCodeHandler(statusCode)
        }
    });
    $("body").on("click", ".open-edit-organisation", function () {
        $.get(BASEURL + "/organisations/" + $(this).data("organisation-number"), function (organisation) {
            organisationToEdit = organisation;
            domOrganisationModal.modal('show');
            domOrganisationModalTitle.html("Edit organisation");
            $.get("organisation_update_modal.html", function (content) {
                domOrganisationModalBody.html(content);
            });
            domOrganisationsModalFooter.html('<button type="button" onclick="updateOrganisation()" class="btn btn-primary">Save changes</button>');
        });

    });
    $("#open-add-organisation").click(function () {
        domOrganisationsModal.modal('show');
        domOrganisationsModalTitle.html("Add organisation");
        $.get("organisation_add_modal.html", function (content) {
            domOrganisationsModalBody.html(content);
        });
        domOrganisationsModalFooter.html('<button type="button" onclick="newOrganisation()" class="btn btn-primary">Add</button>');
    });
});
function newOrganisation() {
    $.post(BASEURL + "/organisations", objectifyForm($("#add-organisation-form").serializeArray()), function () {
        document.location.reload();
    });
}
function updateOrganisation() {
    $.ajax({
        method: "PUT",
        url: BASEURL + "/organisations/" + organisationToEdit.number,
        data: objectifyForm($("#edit-organisation-form").serializeArray())
    }).done(function () {
        document.location.reload();
    });
}
