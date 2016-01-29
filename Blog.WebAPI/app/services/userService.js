(function () {
    "use strict";

    app.factory("userService", function ($http, $q) {

        var baseUrl = "api/User/";
        var factory = {};

        // GET all users
        factory.getUsers = function () {
            return $http.get(baseUrl);
        };

        // upload user's avatar
        factory.uploadAvatar = function (file) {
            var fd = new FormData();
            fd.append("file", file);

            var defer = $q.defer();

            $http.post("/Helper/UploadFile", fd, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .success(function (d) {
                defer.resolve(d);
            })
            .error(function () {
                defer.reject("File Upload Failed!");
            });

            return defer.promise;
        };

        // INSERT new user
        factory.createUser = function (user) {
            return $http.post(baseUrl, user);
        }

        return factory;

    });

})();