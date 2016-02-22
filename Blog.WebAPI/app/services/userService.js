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
            .success(function (data) {
                defer.resolve(data);
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

        // Check Username is exists            
        factory.checkUserNameExists = function (userName) {
            var deferred = $q.defer();
            $http.get(baseUrl + "CheckUserNameExists?userName=" + userName)
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        }
        return factory;

    });

})();