﻿(function () {
    "use strict";

    app.controller("categoryController", ["$scope", "categoryService", "filterFilter",
        function ($scope, categoryService, filter) {

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
                //debugger;
                $scope.showParent = true;
                $scope.showChildren = true;
                $scope.filtered = filter($scope.categories, newVal);
                if ($scope.filtered.length) {
                    $scope.showChildren = false;
                }
                if (!$scope.filtered.length) {
                    $scope.showParent = false;
                }
                if (newVal.category_name === "") {
                    $scope.showParent = true;
                    $scope.showChildren = true;
                }
                //if (!$scope.filtered.length) {

                //} else {
                //    for (var i = 0; i < $scope.filtered.length; i++) {
                //        var a = $scope.filtered[i].categories;
                //        $scope.filteredChildren = filter(a, newVal);
                //    }
                //}
            }, true);

            // GET parent categories
            $scope.getParentCategory = function (category) {
                var promiseGetSingle = categoryService.getCategory(category.category_id);
                promiseGetSingle.then(function (pl) {
                    var res = pl.data;
                    $scope.category_id = res.category_id;
                    $scope.category_name = res.category_name;
                    $scope.category_slug = res.category_slug;
                    $scope.category_description = res.category_description,
                    $scope.category_parent = res.category_parent;
                    $scope.category_active = res.category_active;

                }, function (error) {
                    console.log("message error: " + error);
                });
            };
            // GET children categories
            $scope.getChildrenCategory = function (children) {
                var promiseGetSingle = categoryService.getCategory(children.category_id);
                promiseGetSingle.then(function (pl) {
                    var res = pl.data;
                    $scope.category_id = res.category_id;
                    $scope.category_name = res.category_name;
                    $scope.category_slug = res.category_slug;
                    $scope.category_description = res.category_description,
                    $scope.category_parent = res.category_parent;
                    $scope.category_active = res.category_active;

                }, function (error) {
                    console.log("message error: " + error);
                });
            };

            // Replace blank with underscore
            $scope.categoryNameOnBlur = function () {
                var slug = ($scope.category.category_name).toString().replace(/_| /g, "-").toLowerCase();
                $scope.category_slug = slug;
            }
        }]);
})();