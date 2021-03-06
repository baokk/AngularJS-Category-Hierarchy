﻿(function () {
    "use strict";

    app.controller("categoryController", ["$scope", "categoryService", "filterFilter", "flash", "modalService",
        function ($scope, categoryService, filter, flash, modalService) {

            $scope.EditMode = false;
            $scope.panelTitle = "Add";

            // get all hierarchy categories
            function getAllHierarchyCategories() {
                categoryService.getCategories()
                    .success(function (response) {
                        $scope.categories = response;
                        $scope.totalItems = $scope.categories.length;
                    })
                    .error(function (error) {
                        console.log(error.message);
                    });
            }

            getAllHierarchyCategories();

            // sort on table
            $scope.reverse = true;
            $scope.order = function (predicate) {
                $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
                $scope.predicate = predicate;
            };

            // paging categories
            $scope.categories = [];
            $scope.currentPage = 1;
            $scope.entryLimit = 5;

            // search on table
            $scope.$watch("search", function (newVal) {
                $scope.filtered = filter($scope.categories, newVal);
                $scope.totalItems = $scope.filtered.length;
                $scope.currentPage = 1;
            }, true);

            // GET parent categories
            $scope.getParentCategory = function (category) {
                $scope.panelTitle = "Edit";
                $scope.EditMode = true;
                var promiseGetSingle = categoryService.getCategory(category.category_id);
                promiseGetSingle.then(function (pl) {
                    var res = pl.data;
                    $scope.category = res;
                    $scope.category_parent = res.category_parent;

                }, function (error) {
                    console.log("message error: " + error);
                });
            };

            // GET children categories
            $scope.getChildrenCategory = function (category) {
                $scope.panelTitle = "Edit";
                $scope.EditMode = true;
                var promiseGetSingle = categoryService.getCategory(category.category_id);
                promiseGetSingle.then(function (pl) {
                    var res = pl.data;
                    $scope.category = res;
                    $scope.category_parent = res.category_parent;
                }, function (error) {
                    console.log("message error: " + error);
                });
            };

            // Replace blank with underscore
            $scope.categoryNameOnBlur = function () {
                var slug = ($scope.category.category_name).toString().replace(/_| /g, "-").toLowerCase();
                $scope.category.category_slug = slug;
            }

            $scope.save = function () {
                if ($scope.EditMode == false) {
                    var addCategory = categoryService.createCategory($scope.category);
                    addCategory.then(function () {
                        getAllHierarchyCategories();
                        $scope.resetForm();
                        saveSuccess();
                    }, function () {
                        errorMessage();
                    });
                } else {
                    var updateCategory = categoryService.updateCategory($scope.category);
                    updateCategory.then(function (data) {
                        getAllHierarchyCategories();
                        $scope.resetForm();
                        saveSuccess();
                    }, function () {
                        errorMessage();
                    });
                }
            }

            // Delete category
            $scope.removeCategory = function (category) {
                var promiseGetSingle = categoryService.getCategory(category.category_id);
                promiseGetSingle.then(function (data) {
                    var detail = data.data;
                    var modalOptions = {
                        closeButtonText: 'Cancel',
                        actionButtonText: 'Delete Category',
                        headerText: 'Delete Category',
                        bodyText: 'Are you sure you want to delete category is ' +  detail.category_name + ' ?'
                    };

                    modalService.showModal({}, modalOptions).then(function (result) {
                        var deleteCategory = categoryService.deleteCategory(category.category_id);
                        deleteCategory.then(function () {
                            getAllHierarchyCategories();
                            deleteSuccess();
                        });
                    });

                }, function () {
                    errorMessage();
                });
            };

            // RESET form after insert
            $scope.resetForm = function clear() {
                // reset all controls
                $scope.category = null;
                // reset default index of category combobox after submit
                if (angular.isDefined($scope.category_parent)) {
                    delete $scope.category_parent;
                }
                // clear validation after submit
                $scope.categoryForm.$setPristine();
                $scope.categoryForm.$setUntouched();
                // reset EditMode is false
                $scope.EditMode = false;
                // change panel title is add
                $scope.panelTitle = "Add";
            }

            // show flash message
            var saveSuccess = function () {
                flash.success = 'Saved Successfully !';
            }

            var deleteSuccess = function () {
                flash.success = 'Deleted Successfully !';
            }

            var errorMessage = function () {
                flash.error = 'Save failed !';
            }

        }]);
})();