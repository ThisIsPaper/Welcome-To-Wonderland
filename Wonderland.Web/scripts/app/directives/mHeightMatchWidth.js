wonderlandApp.directive('mHeightMatchWidth', ['$window', function ($window) {

    return {

        scope: false,
        link: function (scope, element) {

            var makeSquare = function () {
                element.css('height', element.width());
            };

            makeSquare();
            angular.element($window).bind('resize', makeSquare);

        }

    };

}]);