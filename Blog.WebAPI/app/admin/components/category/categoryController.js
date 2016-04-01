(function () {
    "use strict";

    app.controller("categoryController", ["$scope", "categoryService", "filterFilter",
        function ($scope, categoryService, filter) {

            getAllCategories();

            // get all categories
            function getAllCategories() {
                categoryService.getCategories()
                .success(function (response) {
                    $scope.categories = response;
                })
                .error(function (error) {
                    console.log(error.message);
                })
            }

            // sort on table
            $scope.reverse = true;
            $scope.order = function (predicate) {
                $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
                $scope.predicate = predicate;
            }

            // search on table
            $scope.$watch("search", function (newVal) {

                if (newVal != null) {
                    debugger;
                    $scope.filtered = filter($scope.categories, newVal);
                    $scope.filtered.categories = null;
                    //if ($scope.filtered == false) {

                    //}
                    $scope.filteredChild = filter($scope.categories, newVal);
                    //if ($scope.filtered == false) {
                    //    $scope.filtered = filter($scope.categories, newVal);
                    //}
                }
                else {
                    $scope.filtered = false;
                }
            }, true);
        }])
})();