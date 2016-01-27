(function () {
    "use strict";

    app.service("userService", function ($http) {

        var baseUrl = "api/User/";

        // GET all users
        this.getUsers = function () {
            return $http.get(baseUrl);
        }

        // INSERT new user
        this.createUser = function (user) {
            var request = $http({
                method: "post",
                url: baseUrl,
                data: user,
                headers: {
                    'Content-Type': undefined
                }
            });
            return request
                    .success(function (d) {
                        alert(d);
                    })
                    .error(function () {
                    });
        }
    });

})();