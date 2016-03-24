(function () {
    "use strict";
    
    app.factory("categoryService", function($http) {

        var baseUrl = "api/Category/"
        var factory = {};
        
        //GET all categories
        factory.getCategories = function() {
            return $http.get(baseUrl);
        }

        return factory;

    })

})();