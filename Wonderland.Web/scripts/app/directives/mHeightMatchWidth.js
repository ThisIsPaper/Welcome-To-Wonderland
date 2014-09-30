wonderlandApp.directive('mHeightMatchWidth', ['debounce', '$window', function (debounce, $window) {

    return {

        scope: false,
        link: function (scope, element) {

            var makeSquare = debounce(function () {
                element.css('height', element.width() + Number(element.css('padding-top').replace('px', '')) + Number(element.css('padding-bottom').replace('px', '')));
            }, 300);

            scope.myWidth = function () {
                return element.width();
            };

            scope.$watch('myWidth()', makeSquare);

            makeSquare();
            angular.element($window).bind('resize', makeSquare);

        }

    };

}]);