app.directive('usernameavail', ['userService', function (userService) {
    var directive = {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            element.on('blur', function (evt) {
                if (!ngModel || !element.val()) return;
                var curValue = element.val();
                userService.checkUserNameExists(curValue)
                .then(function (response) {
                    ngModel.$setValidity('unique', response);
                }, function () {
                    ngModel.$setValidity('unique', true);
                });
            });

        }
    }
    return directive;
}]);

app.directive('emailavail', ['userService', function (userService) {
    var directive = {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            element.on('blur', function (evt) {
                if (!ngModel || !element.val()) return;
                var curValue = element.val();
                userService.checkEmailExists(curValue)
                .then(function (response) {
                    ngModel.$setValidity('unique', response);
                }, function () {
                    ngModel.$setValidity('unique', true);
                });
            });

        }
    }
    return directive;
}]);

