(function () {
    "use strict";

    app.controller("categoryController", ["$scope", "categoryService", "filterFilter", "flash",
        function ($scope, categoryService, filter, flash) {

            $scope.EditMode = false;

            // get all hierarchy categories
            function getAllHierarchyCategories() {
                categoryService.getCategories()
                    .success(function (response) {
                        $scope.categories = response;
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

            // search on table
            $scope.$watch("search", function (newVal) {
                $scope.filtered = filter($scope.categories, newVal);
            }, true);

            // GET parent categories
            $scope.getParentCategory = function (category) {

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
            $scope.getChildrenCategory = function (children) {
                var promiseGetSingle = categoryService.getCategory(children.category_id);
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

            $scope.editCategory = function (category) {
                $scope.category = category;
            }

            $scope.save = function () {
                var addCategory = categoryService.createCategory($scope.category);
                addCategory.then(function () {
                    $scope.resetForm();
                    getAllHierarchyCategories();
                    saveSuccess();
                });

            }

            // RESET form after insert
            $scope.resetForm = function clear() {

                $scope.category.category_name = "";
                $scope.category.category_slug = "";
                $scope.category.category_description = "";
                $scope.category.category_active = false;
                $scope.EditMode = false;

                //reset category dropdown after submits
                if (angular.isDefined($scope.category.category_parent)) {
                    delete $scope.category.category_parent;
                }

                // clear validation after submit
                $scope.categoryForm.$setPristine();
                $scope.categoryForm.$setUntouched();
            }

            // show flash message
            var saveSuccess = function () {
                flash.success = 'Saved Successfully !';
            }

            var deleteSuccess = function () {
                flash.success = 'Deleted Successfully !';
            }

        }]);
})();