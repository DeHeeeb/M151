$(document).ready(function () {
    var domCartTable = $('#cart table');
    $(domCartTable).wrap('<div id="carthide" style="display:none"/>');
    var cartTable = domCartTable.DataTable({
        columns: [
            {data: "number"},
            {data: "name"},
            {data: "tariff"},
            {data: "age"},
            {data: "organisation"},
            {data: "publisher"},
            {data: "cart"}
        ],
        responsive: true
    });
    polluteDataTables();

    navigateToTab();
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href").substr(1); // activated tab
        var domTable = $('#' + target + ' table');
        if (domTable.is(':visible'))
            domTable.DataTable().responsive.recalc();
        history.pushState(target, target, "lend.html?tab=" + target);
    });

    function navigateToTab() {
        $('.nav-tabs a[href="#' + getParameterByName("tab") + '"]').tab('show');
    }

    function polluteDataTables() {
        polluteAllGames();
        polluteCart();
    }

    function polluteCart() {
        $("#cart .loader").fadeOut(function () {
            var itemsInCart = Cookies.get("cart");
            if (itemsInCart != null && JSON.parse(itemsInCart).length != 0) {
                $('#carthide').css('display', 'block');
                domCartTable.fadeIn();
                cartTable.responsive.recalc();
                JSON.parse(itemsInCart).forEach(function (game) {
                    addGameToCart(game)
                });
                cartTable.draw();
                $('#rent').removeClass("hide");
                if (cartTable.data().count() > 0) $("#rent").prop('disabled', false);
            } else {
                $('#cart').append('<div id="cartIsEmptyInfo" class="alert alert-info"><strong>Your cart is empty!</strong> You should add some items!</div>');
            }
        });

    }

    function polluteAllGames() {
        $.get(BASEURL + "/games").done(function (games) {
            $("#games .loader").fadeOut(function () {
                var domTable = $('#games table');
                domTable.fadeIn();
                var table = domTable.DataTable({
                    columns: [
                        {data: "number"},
                        {data: "name"},
                        {data: "tariff"},
                        {data: "age"},
                        {data: "organisation"},
                        {data: "publisher"},
                        {data: "cart"}
                    ],
                    responsive: true
                });
                games.forEach(function (game) {
                    var btnState = game.isAvailable && !isInCart(game) ? "" : "disabled";
                    var btnText = game.isAvailable ? isInCart(game) ? "Already in cart" : "Add to cart" : "Is lend out";
                    table.row.add({
                        "number": game.number,
                        "name": game.name,
                        "tariff": game.tariff,
                        "age": game.age,
                        "organisation": game.organisation.name,
                        "publisher": game.publisher.name,
                        "cart": "<button data-gameNumber='" + game.id + "' class='btn btn-primary addToCart' " + btnState + "><span class='glyphicon glyphicon-shopping-cart' aria-hidden='true'></span> " + btnText + "</button>"
                    });
                });
                table.draw();
                lockCart();
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
        function isInCart(game) {
            var cart = Cookies.get("cart");
            if (cart == null)
                return false;
            cart = JSON.parse(cart);
            var found = false;
            cart.forEach(function (gameInCart) {
                if (gameInCart.number == game.number) found = true;
            });
            return found;
        }
    }

    $("body").on("click", ".addToCart", function () {
        $(this).html("<span class='glyphicon glyphicon-shopping-cart' aria-hidden='true'></span> Already in cart");
        $(this).prop('disabled', true);
        $.get(BASEURL + "/games/" + $(this).data("gamenumber")).done(function (game) {
            if (Cookies.get("cart") == null)
                Cookies.set("cart", []);
            var cart = Cookies.getJSON("cart");
            cart.push(game);
            Cookies.set("cart", cart);
            $('#carthide').css('display', 'block');
            domCartTable.fadeIn();
            cartTable.responsive.recalc();
            addGameToCart(game);
            lockCart();
            $("#rent").removeClass("hide");
            if (cartTable.data().count() > 0) $("#rent").prop('disabled', false);
            $("#cartIsEmptyInfo").hide();
        }).fail(function (response) {
            var statusCode = response.status;
            console.dir(statusCode);
            if (statusCode === 401) {
                window.location = "/";
            } else {
                statusCodeHandler(statusCode)
            }
        });
    });
    function addGameToCart(game) {
        cartTable.row.add({
            "number": game.number,
            "name": game.name,
            "tariff": game.tariff,
            "age": game.age,
            "organisation": game.organisation.name,
            "publisher": game.publisher.name,
            "cart": "<button data-gameNumber='" + game.id + "' class='btn btn-primary removeFromCart' ><span class='glyphicon glyphicon-shopping-cart' aria-hidden='true'></span> Remove from cart</button>"
        });
        cartTable.draw();
    }

    $("body").on("click", ".removeFromCart", function () {
        cartTable
            .row($(this).parents('tr'))
            .remove()
            .draw();
        var cart = JSON.parse(Cookies.get("cart"));
        var i = 0;
        cart.forEach(function (game) {
            i++;
            if (game.number == $(this).data("gamenumber"))return;
        });
        cart.splice(i - 1, 1);
        Cookies.set("cart", cart);
        var gameButton = $('#games table').find("[data-gameNumber='" + $(this).data("gamenumber") + "']");
        gameButton.html("<span class='glyphicon glyphicon-shopping-cart' aria-hidden='true'></span> Add to cart");
        gameButton.prop('disabled', false);
        unlockCart();
        if (cartTable.data().count() <= 0) $("#rent").prop('disabled', true);
    });
    $("#rent").click(function () {
        var btn = $(this).button('loading');
        var data = cartTable.rows().data();
        var games = [];
        data.each(function (game) {
            delete game.cart;
            games.push(game);
        });
        var payload = JSON.stringify({"games": games, "customer" : {
            "email": "joan.kuenzler@gmail.com",
            "number": 1,
            "surname": "Kuenzler",
            "prename": "Joan",
            "phoneNumber": "0712901214",
            "city": "St.Gallen",
            "zipCode": "9300",
            "street": "Neusteig",
            "streetNumber": "14",
            "isPremiumMember": true
        }});
        $.post(BASEURL + "/lendings", payload)
            .done(function () {
                Cookies.set("cart", "[]");
                window.location = "lend.html";
            })
            .fail(function (response) {
                var statusCode = response.status;
                console.dir(statusCode);
                if (statusCode === 401) {
                    window.location = "/";
                } else {
                    statusCodeHandler(statusCode)
                }
                btn.button("reset");
            });

    });

    function lockCart() {
        if (cartTable.data().count() >= 3)
            $('#games table button').each(function () {
                if ($(this).prop('disabled') == false) {
                    $(this).prop('disabled', true);
                    $(this).html("<span class='glyphicon glyphicon-shopping-cart' aria-hidden='true'></span> Cart is full");
                }
            });
    }

    function unlockCart() {
        if (cartTable.data().count() < 3)
            $('#games table button').each(function () {
                if ($(this).text() == " Cart is full") {
                    $(this).prop('disabled', false);
                    $(this).html("<span class='glyphicon glyphicon-shopping-cart' aria-hidden='true'></span> Add to cart");
                }
            });
    }
});