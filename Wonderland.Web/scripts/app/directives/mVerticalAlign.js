wonderlandApp.directive('mVerticalAlign', ['debounce', '$timeout', '$window', function (debounce, $timeout, $window) {

    return {

        link: function (scope, element, attrs) {

            var hasInited = false,
                myParent = angular.element(element).parent(),
                reAlign = debounce(function () {
                    element.css('top', (scope.parentHeight() - element.height())/2);
                }, 200);

            scope.parentHeight = function () { return myParent.height(); };
            scope.parentWidth = function () { return myParent.width(); };

            attrs.$observe('mVerticalAlign', function (newVal) {
                if (!hasInited && !!newVal===true) {

                    scope.$watch('parentHeight() + parentWidth()', reAlign);
                    angular.element($window).bind('resize', reAlign);

                    reAlign();
                    $timeout(reAlign, 500);
                }
                if (!hasInited) {
                    hasInited=true;
                }
            });

        }

    };

}]);