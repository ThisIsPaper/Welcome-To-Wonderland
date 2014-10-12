wonderlandApp.directive('mImageLoadedNotifier', [function () {

    return {
        restrict: 'A',
        link: function (scope, element) {
            angular.element(element).bind('load', function () {
                scope.$emit('childImageLoaded');
            });
        }
    };

}]);