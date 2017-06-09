var gameToEdit;
$(document).ready(function () {
    var domGamesTable = $('#games-table');
    var domGamesModal = $("#games-modal");
    var domGamesModalTitle = domGamesModal.find(".modal-title");
    var domGamesModalBody = domGamesModal.find(".modal-body");
    var domGamesModalFooter = domGamesModal.find(".modal-footer");
    $.get(BASEURL + "/games").done(function (games) {
        $("#games-loader").fadeOut(function () {
            domGamesTable.fadeIn();
            var gamesTable = domGamesTable.DataTable({
                columns: [
                    { data: "number" },
                    { data: "prename" },
                    { data: "surname" },
                    { data: "organisation" },
                    { data: "edit" }
                ],
                responsive: true
            });
            games.forEach(function (game) {
                gamesTable.row.add({
                    "number": game.number,
                    "prename": game.prename,
                    "surname": game.surname,
                    "organisation": game.organisation.name,
                    "edit": "<button data-game-number='" + game.number + "' class='btn btn-primary open-edit-game'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span> Edit game</button>"
                });
            });
            gamesTable.draw();
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
    $("body").on("click", ".open-edit-game", function () {
        $.get(BASEURL + "/games/" + $(this).data("game-number"), function (game) {
            gameToEdit = game;
            domGamesModal.modal('show');
            domGamesModalTitle.html("Edit game");
            $.get("game_update_modal.html", function (content) {
                domGamesModalBody.html(content);
            });
            domGamesModalFooter.html('<button type="button" onclick="updateGames()" class="btn btn-primary">Save changes</button>');
        });

    });
    $("#open-add-game").click(function () {
        domGamesModal.modal('show');
        domGamesModalTitle.html("Add game");
        $.get("game_add_modal.html", function (content) {
            domGamesModalBody.html(content);
        });
        domGamesModalFooter.html('<button type="button" onclick="newGame()" class="btn btn-primary">Add</button>');
    });
});
function newGame() {
    $.post(BASEURL + "/games", objectifyForm($("#add-game-form").serializeArray()), function () {
        document.location.reload();
    });
}
function updateGame() {
    $.ajax({
        method: "PUT",
        url: BASEURL + "/games/" + gameToEdit.number,
        data: objectifyForm($("#edit-game-form").serializeArray())
    }).done(function () {
        document.location.reload();
    });
}
