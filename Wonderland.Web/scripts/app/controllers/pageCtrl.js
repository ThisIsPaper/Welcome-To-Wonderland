wonderlandApp.controller('PageCtrl', ['debounce', '$rootScope', '$scope', '$window', function (debounce, $rootScope, $scope, $window) {

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





    /*************************
    HARDCODED
    /*************************/
    $rootScope.hardcodedFundraisingTargets = [
        {imageUrl: '/img/test_default_image_1.jpg', title: 'Standard', info: 'Get a round of applause and enjoy your party kit'},
        {imageUrl: '/img/test_default_image_2.jpg', title: '£150 or more', info: 'Party kit and an exclusive Wonderland t-shirt by Ashish'},
        {imageUrl: '/img/test_default_image_3.jpg', title: '£300 or more', info: 'Party kit, t-shirt and an exclusive invite to our celebrity after party'}
    ];

}]);