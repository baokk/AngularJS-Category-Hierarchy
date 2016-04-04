(function () {
    "use strict";
    
    app.factory("categoryService", function($http) {

        var baseUrl = "api/Category/";
        var factory = {};
        
        //GET all categories
        factory.getCategories = function() {
            return $http.get(baseUrl);
        }; // GET single category
        factory.getCategory = function (category_id) {
            return $http.get(baseUrl + category_id);
        }; // INSERT a new category
        factory.createCategory = function (category) {
            return $http.post(baseUrl, category);
        };
        return factory;

    });
})();