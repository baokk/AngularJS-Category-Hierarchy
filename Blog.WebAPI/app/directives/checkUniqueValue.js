app.directive('checkValueExists', ['userService', function(userService){
   return directive = {
       restrict: 'A',
       require: 'ngModel',
       link: function(scope, element, attrs, ngModel){
           element.on('blur', function(evt){
               if(!ngModel || !element.val())
                   return;
               var currentValue = element.val();
               userService.checkUserNameExists(currentValue)
               .then(function (response){
                   ngModel.$setValidity('unique', response);
               }, function(){
                   ngModel.$setValidity('unique', true);
               })
           })
       }
   }
}]);