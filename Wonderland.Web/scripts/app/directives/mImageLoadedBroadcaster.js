wonderlandApp.directive('mImageLoadedBroadcaster', [function () {

    return {
        restrict: 'A',
        link: function (scope) {
            scope.$on('childImageLoaded', function () {
                scope.$broadcast('childImageLoadedBroadcast');
            });
        }
    };

}]);