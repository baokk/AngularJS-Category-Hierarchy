var app = angular.module("BlogApp", ["ui.router", "ui.bootstrap"]);

app.config(function ($stateProvider, $urlRouterProvider) {

    // For any unmatched url, send to /
    $urlRouterProvider.otherwise("/");
    $stateProvider
        .state("home", {
            url: "/",
            templateUrl: "index.html",
        })
        .state("user", {
            url: "/user",
            templateUrl: "app/user/user.html",
        })
        .state("category", {
            url: "/category",
            templateUrl: "app/category/category.html",
        });
});





