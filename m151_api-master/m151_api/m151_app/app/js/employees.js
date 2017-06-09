var employeeToEdit;
$(document).ready(function () {
    var domEmployeesTable = $('#employees-table');
    var domEmployeesModal = $("#employees-modal");
    var domEmployeesModalTitle = domEmployeesModal.find(".modal-title");
    var domEmployeesModalBody = domEmployeesModal.find(".modal-body");
    var domEmployeesModalFooter = domEmployeesModal.find(".modal-footer");
    $.get(BASEURL + "/employees").done(function (employees) {
        $("#employees-loader").fadeOut(function () {
            domEmployeesTable.fadeIn();
            var employeesTable = domEmployeesTable.DataTable({
                columns: [
                    {data: "number"},
                    {data: "prename"},
                    {data: "surname"},
                    {data: "organisation"},
                    {data: "edit"}
                ],
                responsive: true
            });
            employees.forEach(function (employee) {
                employeesTable.row.add({
                    "number": employee.number,
                    "prename": employee.prename,
                    "surname": employee.surname,
                    "organisation": employee.organisation.name,
                    "edit": "<button data-employee-number='" + employee.number + "' class='btn btn-primary open-edit-employee'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span> Edit employee</button>"
                });
            });
            employeesTable.draw();
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
    $("body").on("click", ".open-edit-employee", function () {
        $.get(BASEURL + "/employees/" + $(this).data("employee-number"), function (employee) {
            employeeToEdit = employee;
            domEmployeesModal.modal('show');
            domEmployeesModalTitle.html("Edit employee");
            $.get("employee_update_modal.html", function (content) {
                domEmployeesModalBody.html(content);
            });
            domEmployeesModalFooter.html('<button type="button" onclick="updateEmployee()" class="btn btn-primary">Save changes</button>');
        });

    });
    $("#open-add-employee").click(function () {
        domEmployeesModal.modal('show');
        domEmployeesModalTitle.html("Add employee");
        $.get("employee_add_modal.html", function (content) {
            domEmployeesModalBody.html(content);
        });
        domEmployeesModalFooter.html('<button type="button" onclick="newEmployee()" class="btn btn-primary">Add</button>');
    });
});
function newEmployee() {
    $.post(BASEURL + "/employees", objectifyForm($("#add-employee-form").serializeArray()), function () {
        document.location.reload();
    });
}
function updateEmployee() {
    $.ajax({
        method: "PUT",
        url: BASEURL + "/employees/" + employeeToEdit.number,
        data: objectifyForm($("#edit-employee-form").serializeArray())
    }).done(function () {
        document.location.reload();
    });
}