(function () {
    "use strict";

    app.controller("userController", function ($scope, $uibModal, userService, filterFilter) {

        getAllUsers();
        // reset isUpdate after submt
        var isUpdate = false;

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

        // paging users
        $scope.users = [];
        $scope.currentPage = 1;
        $scope.entryLimit = 10;

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

        // show modal
        $scope.animationsEnabled = true;
        $scope.open = function (size) {
            $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'userModal.html',
                size: size,
                controller: function ($scope, $uibModalInstance) {
                    $scope.save = function () {
                        //debugger;
                        var file = $scope.myFile;
                        userService.uploadAvatar(file)
                        .then(function (data) {
                            alert(data.fileName);
                        });
                        //var user = {
                        //    user_username: $scope.user_username,
                        //    user_password: $scope.user_password,
                        //    user_email: $scope.user_email,
                        //    user_firstname: $scope.user_firstname,
                        //    user_lastname: $scope.user_lastname,
                        //    user_avatar: file.name,
                        //    user_displayname: $scope.user_displayname,
                        //    user_active: $scope.user_active
                        //}

                        //if (isUpdate === false) {
                        //    var insertUser = userService.createUser(user);
                        //    insertUser.then(function () {
                        //        getAllUsers();
                        //        $uibModalInstance.close();
                        //    });
                        //}
                    };

                    $scope.cancel = function () {
                        $uibModalInstance.dismiss('cancel');
                    };
                }
            });
        };

        $scope.toggleAnimation = function () {
            $scope.animationsEnabled = !$scope.animationsEnabled;
        };

    });
})();
