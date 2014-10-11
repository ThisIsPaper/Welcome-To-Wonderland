wonderlandApp.controller('PageCtrl', ['debounce', 'paf', 'safeApply', '$rootScope', '$scope', '$timeout', '$window', function (debounce, paf, safeApply, $rootScope, $scope, $timeout, $window) {

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

        safeApply($scope, function () {
            $scope.pageFeedback.profileImageProcessing = true;
        });

    });
    $scope.$onRootScope('profileImageUpdated', function(event, response, dataObject) {


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
        {imageUrl: '/img/test_default_image_1.jpg', title: 'Raise £150 or more', info: 'Sunglasses are essential Wonderland accessories - look cool as an alpine stream!'},
        {imageUrl: '/img/test_default_image_2.jpg', title: 'Raise £180 or more', info: 'Get the above and a T-Shirt, designed by world famous fashion designer Ashish - specifically designed for Macmillan\'s Wonderland.'},
        {imageUrl: '/img/test_default_image_3.jpg', title: 'Raise £240 or more', info: 'Get all the above and an invitation to an evening at the Snowdome for some real fake snow.'}
    ];

}]);