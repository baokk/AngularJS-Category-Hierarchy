var app = angular.module("BlogApp", ["ngAnimate", "ui.router", "ui.bootstrap", "ngMessages"]);

app.config(function ($stateProvider, $urlRouterProvider) {

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