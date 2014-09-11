wonderlandApp.controller('PageCtrl', ['debounce', '$scope', '$window', function (debounce, $scope, $window) {

    $scope.pageHeaderCompress = false;
    $scope.pageShowingSideNavMenu = false;
    $scope.pageShowingSideAccountMenu = false;






    /*************************
    SITE HEADER COMPRESS VIEW
    /*************************/
    var runScrollCheck = function () {

            var diff = ((angular.element($window).scrollTop() > 100 && !$scope.pageHeaderCompress) ||
                        (angular.element($window).scrollTop() <= 100 && $scope.pageHeaderCompress));

            if (diff) {
                if ($scope.$$phase) {
                    updateCompressValue();
                } else {
                    $scope.$apply(updateCompressValue);
                }
            }

        },
        updateCompressValue = function () {
            $scope.pageHeaderCompress = angular.element($window).scrollTop() > 100;
        };


    runScrollCheck();
    angular.element($window).bind('scroll', runScrollCheck);

}]);