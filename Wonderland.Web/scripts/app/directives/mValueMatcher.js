wonderlandApp.directive('mValueMatcher', [function () {

    return {
        scope: {
            'm': '=ngModel'
        },
        link: function (scope, element, attrs) {

            var hasInitiallySet = false;

            scope.$watch('m', function () {

                if (hasInitiallySet) {
                    return;
                }

                hasInitiallySet = true;
                scope.m = attrs["value"];

            });


        }
    };

}]);