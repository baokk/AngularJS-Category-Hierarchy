var app = angular.module("BlogApp", ["ngAnimate", "ui.router", "ui.bootstrap", "ngMessages"]);

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

app.filter("startFrom", function () {
    return function (input, start) {
        if (input) {
            start = +start;
            return input.slice(start);
        }
        return [];
    };
});





