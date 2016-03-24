(function () {
    "use strict";

    app.controller("categoryController", ["$scope", "categoryService",
        function ($scope, categoryService) {

            getAllCategories();

            function getAllCategories() {
                categoryService.getCategories()
                .success(function (response) {
                    $scope.categories = response;
                })
                .error(function (error) {
                    console.log(error.message);
                })
            }
        }])

})();