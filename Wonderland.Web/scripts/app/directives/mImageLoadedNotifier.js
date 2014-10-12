wonderlandApp.directive('mImageLoadedNotifier', [function () {

    return {
        restrict: 'A',
        link: function (scope, element) {
            angular.element(element).bind('load', function () {
                angular.element(element).unbind('load');
                scope.$emit('childImageLoaded');
            });
        }
    };

}]);