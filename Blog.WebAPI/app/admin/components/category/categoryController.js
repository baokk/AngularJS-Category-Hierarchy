(function () {
    "use strict";

    app.controller("categoryController", ["$scope", "categoryService",
        function ($scope, categoryService) {

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

            // order by on grid
            $scope.reverse = true;
            $scope.order = function (predicate) {
                $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
                $scope.predicate = predicate;
            }
        }])

})();