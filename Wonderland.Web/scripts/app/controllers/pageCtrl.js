wonderlandApp.controller('PageCtrl', ['debounce', 'safeApply', '$rootScope', '$scope', '$timeout', '$window', function (debounce, safeApply, $rootScope, $scope, $timeout, $window) {

    $scope.pageHeaderCompress = false;
    $scope.pageShowingSideNavMenu = false;
    $scope.pageShowingSideAccountMenu = false;
    $scope.pageProfile = {
        imageUrl: null
    };
    $scope.pageFeedback = {

        profileImageProcessing:  false,
        profileImageShowSuccess: false,
        profileImageShowError: false

    };






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







     /***
     *
     * PROFILE IMAGE CHANGE
     *
     */
    $scope.$onRootScope('profileImageUploadStart', function() {
        console.log('Event::profileImageUploadStart');

        safeApply($scope, function () {
            $scope.pageFeedback.profileImageProcessing = true;
        });

    });
    $scope.$onRootScope('profileImageUpdated', function(event, response, dataObject) {
        console.log('Event::profileImageUpdated', response, dataObject);


        /**
         * safe apply the feedback response
         */
        safeApply($scope, function () {
            $scope.pageFeedback.profileImageProcessing = false;

            if (response && response.Success === true && response.Message) {
                $scope.pageProfile.imageUrl = response.Message;
                $scope.pageFeedback.profileImageShowSuccess = true;
            } else {
                $scope.pageFeedback.profileImageShowError = true;
            }

            $timeout(function () {
                $scope.pageFeedback.profileImageShowSuccess = false;
                $scope.pageFeedback.profileImageShowError = false;
            }, 5000);
        });
    });






    /*************************
    HARDCODED
    /*************************/
    $rootScope.hardcodedFundraisingTargets = [
        {imageUrl: '/img/test_default_image_1.jpg', title: 'Standard', info: 'Get a round of applause and enjoy your party kit'},
        {imageUrl: '/img/test_default_image_2.jpg', title: '£150 or more', info: 'Party kit and an exclusive Wonderland t-shirt by Ashish'},
        {imageUrl: '/img/test_default_image_3.jpg', title: '£300 or more', info: 'Party kit, t-shirt and an exclusive invite to our celebrity after party'}
    ];

}]);