app.directive('knUnique', ['userService', function (userService) {
    var directive = {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            element.on('blur', function (evt) {
                var currentValue = ngModel.$viewValue || ngModel.$modelValue;
                var key = attrs.knUniqueKey;
                if (key === "") key = "0";
                var property = attrs.knUniqueProperty;
                if (key && property) {
                    userService.checkUniqueValue(key, currentValue, property)
                        .then(function (response) {
                            ngModel.$setValidity('unique', response);
                        }, function () {
                            ngModel.$setValidity('unique', true);
                        });
                }
            });
        }
    }
    return directive;
}]);

