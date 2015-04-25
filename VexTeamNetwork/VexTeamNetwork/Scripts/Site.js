$(window).resize(function () {
    if ($(window).width() <= 768) {
        $(".navbar-search-input").addClass("navbar-search-hidden");
        $("#search-navbar").addClass("navbar-search-hidden");
    }
    else {
        $(".navbar-search-input").removeClass("navbar-search-hidden");
        $("#search-navbar").removeClass("navbar-search-hidden");
    }
});

$(function () {
    if ($(window).width() <= 768) {
        $(".navbar-search-input").addClass("navbar-search-hidden");
        $("#search-navbar").addClass("navbar-search-hidden");
    }
    else {
        $(".navbar-search-input").removeClass("navbar-search-hidden");
        $("#search-navbar").removeClass("navbar-search-hidden");
    }
});

$("#searchButton").click(function () {
    if ($("#searchInput").hasClass("navbar-search-hidden")) {
        $(".navbar-search-input").removeClass("navbar-search-hidden");
        $("#search-navbar").removeClass("navbar-search-hidden");
    }
    else if ($("#searchInput").val().length != 0) {
        alert($("#searchInput").val());
    }
    else {
        $(".navbar-search-input").addClass("navbar-search-hidden");
        $("#search-navbar").addClass("navbar-search-hidden");
    }

    if ($("#login-partial").hasClass("in"))
        $("#loginToggle").click();
});

$("#searchInput").keyup(function (e) {
    if (e.keyCode === 13)
        $("#searchButton").click();
})