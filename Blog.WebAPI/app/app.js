var app = angular.module("BlogApp", ["ngAnimate", "ui.router", "ui.bootstrap", "ngMessages",
    "angular-flash.service", "angular-flash.flash-alert-directive"]);

app.config(function ($stateProvider, $urlRouterProvider, flashProvider) {

    // config flash message
    // Support bootstrap 3.0 "alert-danger" class with error flash types
    flashProvider.errorClassnames.push("alert-danger");
    flashProvider.warnClassnames.push("alert-warning");
    flashProvider.infoClassnames.push("alert-info");
    flashProvider.successClassnames.push("alert-success");

    // For any unmatched url, send to /
    $urlRouterProvider.otherwise("/");
    // public
    $stateProvider
        .state("home", {
            url: "/",
            templateUrl: "index.html",
        });

    // private
    $stateProvider
       .state("admin", {
           url: "/admin",
           templateUrl: "app/admin/index.html",
       })
    .state("admin.user", {
        url: "/user",
        templateUrl: "app/admin/components/user/user.html",
    })
    .state("admin.category", {
        url: "/category",
        templateUrl: "app/admin/components/category/category.html",
    });
});

app.filter("startFrom", function () {
    return function (input, start) {
        if (input) {
            start = +start;
            return input.slice(start);
        }
        return [];
    };
});