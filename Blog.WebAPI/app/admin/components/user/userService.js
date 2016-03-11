(function () {
    "use strict";

    app.factory("userService", function ($http, $q) {

        var baseUrl = "api/User/";
        var factory = {};

        // GET all users
        factory.getUsers = function () {
            return $http.get(baseUrl);
        };

        // GET single user
        factory.getUser = function (id) {
            return $http.get(baseUrl + id);
        }

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

        // Delete file in folder 
        factory.deleteFile = function (fileName) {
            return $http.post("/Helper/DeleteFile/?fileName=" + fileName);
        }

        // INSERT new user
        factory.createUser = function (user) {
            return $http.post(baseUrl, user);
        }

        // Check Username is exists            
        factory.checkUniqueValue = function (key, value, property) {
            var deferred = $q.defer();
            $http.get(baseUrl + "CheckUniqueValue?key=" + key + "&value=" + value + "&property=" + property)
            .success(deferred.resolve)
            .error(deferred.reject);
            return deferred.promise;
        }

        // UPDATE user
        factory.updateUser = function (user) {
            return $http.put(baseUrl + user.id, user);
        }

        // DELETE user
        factory.deleteUser = function (id) {
            return $http.delete(baseUrl + id);
        }

        return factory;

    });

})();