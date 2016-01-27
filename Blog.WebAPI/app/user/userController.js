(function () {
    "use strict";

    app.controller("userController", function ($scope, userService, filterFilter) {

        $scope.users = [];
        $scope.EditMode = false;

        getAllUsers();

        $scope.currentPage = 1;
        $scope.entryLimit = 10;

        // get all users
        function getAllUsers() {
            userService.getUsers()
                .success(function (response) {
                    $scope.users = response;
                    $scope.totalItems = $scope.users.length;
                })
                .error(function (error) {
                    console.log(error.message);
                });
        };

        // $watch search to update pagination
        $scope.$watch("search", function (newVal) {
            $scope.filtered = filterFilter($scope.users, newVal);
            $scope.totalItems = $scope.filtered.length;
            $scope.currentPage = 1;
        }, true);

        // order by on grid
        $scope.reverse = true;
        $scope.order = function (predicate) {
            $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
            $scope.predicate = predicate;
        };


        $scope.save = function () {

            var file = $scope.myFile;
            userService.uploadAvatar(file);

            var user = {
                user_username: $scope.user_username,
                user_password: $scope.user_password,
                user_email: $scope.user_email,
                user_firstname: $scope.user_firstname,
                user_lastname: $scope.user_lastname,
                user_avatar: file.name,
                user_displayname: $scope.user_displayname,
                user_active: $scope.user_active
            }
            if ($scope.EditMode === false) {
                var insertUser = userService.createUser(user);
                insertUser.then(function () {
                    getAllUsers();
                    alert("Insert Success !");
                });
            }
        }
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

})();
