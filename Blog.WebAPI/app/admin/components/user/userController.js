(function () {
    "use strict";

    app.controller("userController", ['$scope', '$uibModal', 'userService', 'filterFilter', 'modalService', 'flash',
    function ($scope, $uibModal, userService, filterFilter, modalService, flash) {
        getAllUsers();
        $scope.user_password = null;
        $scope.passwordConfirmation = null;

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

        // add user
        $scope.addUser = function () {
            $scope.user = { id: 0 };
            $scope.open();
        }

        // edit user
        $scope.editUser = function (user) {
            $scope.user = user;
            $scope.user.passwordConfirmation = user.user_password;
            $scope.open();
        }

        // show modal Insert and Update User
        $scope.animationsEnabled = true;
        $scope.toggleAnimation = function () {
            $scope.animationsEnabled = !$scope.animationsEnabled;
        };
        $scope.open = function () {
            $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'userModal.html',
                controller: function ($scope, $uibModalInstance, user) {
                    $scope.user = user;
                    if (user.id == 0)
                        $scope.headerTitle = 'Add User';
                    else
                        $scope.headerTitle = 'Edit User';

                    $scope.save = function () {
                        var file = $scope.myFile;
                        if (file == null) {
                            if (user.id == 0) {
                                $scope.user.user_avatar = null;
                                var addUser = userService.createUser($scope.user);
                                addUser.then(function () {
                                    getAllUsers();
                                })
                            }
                            else {
                                if (!$scope.user.user_avatar) {
                                    $scope.user.user_avatar = null;
                                }
                                else {
                                    $scope.user.user_avatar = user.user_avatar;
                                }
                                var editUser = userService.updateUser($scope.user);
                                editUser.then(function () {
                                    getAllUsers();

                                })
                            }
                        }
                        else {
                            var extall = "jpg,jpeg,png";
                            var ext = file.name.split('.').pop().toLowerCase();
                            if (parseInt(extall.indexOf(ext)) < 0)
                                return alert("Please upload image with file types are 'jpg', 'jpeg', 'png' !");
                            if (file.size > 1024000)
                                return alert("Please upload image less than 1 MB !");
                            else
                                userService.uploadAvatar(file)
                                .then(function (data) {
                                    var oldFile = $scope.user.user_avatar;
                                    $scope.newFileName = data;
                                    $scope.user.user_avatar = $scope.newFileName;
                                    if (user.id == 0) {
                                        var addUser = userService.createUser($scope.user);
                                        addUser.then(function () {
                                            getAllUsers();
                                        })
                                    }
                                    else {
                                        var editUser = userService.updateUser($scope.user);
                                        userService.deleteFile(oldFile);
                                        editUser.then(function () {
                                            getAllUsers();
                                        })
                                    }
                                });
                        }
                        saveSuccess();
                        $uibModalInstance.close();
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss('cancel');
                    };
                },
                resolve: {
                    user: function () {
                        return $scope.user;
                    }
                }
            });
        }

        $scope.removeUser = function (user) {

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete User',
                headerText: 'Delete User',
                bodyText: 'Are you sure you want to delete user ' + user.user_username + ' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                var deleteUser = userService.deleteUser(user.id);
                userService.deleteFile(user.user_avatar);
                deleteUser.then(function () {
                    getAllUsers();
                    deleteSuccess();
                });
            });
        };

        // show flash message
        var saveSuccess = function () {
            flash.success = 'Saved Successfully !';
        }

        var deleteSuccess = function () {
            flash.success = 'Deleted Successfully !'
        }

    }]);
})();