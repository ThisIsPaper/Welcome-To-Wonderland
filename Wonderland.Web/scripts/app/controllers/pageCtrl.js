wonderlandApp.controller('PageCtrl', ['debounce', 'paf', '$rootScope', '$scope', '$timeout', '$window', function (debounce, paf, $rootScope, $scope, $timeout, $window) {

    $scope.pageHeaderCompress = false;
    $scope.pageShowingSideNavMenu = false;
    $scope.pageShowingSideAccountMenu = false;
    $scope.pageProfile = {
        image: null
    };
    $scope.pageFeedback = {

        profileImageProcessing:  false,
        profileImageShowSuccess: false,
        profileImageShowError: false

    };

    /*************************
    SITE HEADER COMPRESS VIEW
    /*************************/
    var scrollCheckLimit = 50;
    var runScrollCheck = function () {

            var diff = ((angular.element($window).scrollTop() > scrollCheckLimit && !$scope.pageHeaderCompress) ||
                        (angular.element($window).scrollTop() <= scrollCheckLimit && $scope.pageHeaderCompress));

            if (diff) {
                if ($scope.$$phase) {
                    updateCompressValue();
                } else {
                    $scope.$apply(updateCompressValue);
                }
            }

        },
        updateCompressValue = function () {
            $scope.pageHeaderCompress = angular.element($window).scrollTop() > scrollCheckLimit;
        };


    runScrollCheck();
    angular.element($window).bind('scroll', runScrollCheck);




     /***
     *
     * PROFILE IMAGE CHANGE
     *
     */
    $scope.onPageLoadProfileImage = function (profileImageData) {
        $scope.pageProfile.image = angular.fromJson(profileImageData);
console.log('onPageLoadProfileImage', $scope.pageProfile.image);
    };


    $scope.$onRootScope('profileImageUploadStart', function() {

        $timeout(function () {
            $scope.pageFeedback.profileImageProcessing = true;
        });

    });
    $scope.$onRootScope('profileImageUpdated', function(event, response, dataObject) {

        /**
         * safe apply the feedback response
         */
        $timeout(function () {
            $scope.pageFeedback.profileImageProcessing = false;

            if (response && response.Success === true) {
                $scope.pageProfile.image = angular.fromJson(response.Message);
console.log('uploaded new image', $scope.pageProfile.image);
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


    /**
     * HACKY TEMPLATE FILTER
     */
    $scope.isExternalLink = function (url) {
        return url.indexOf('http') === 0;
    };


    /**
     * HACKY FORM TOKEN SCRAPE
     */
    $scope.$on('formToken', function (event, token) {
        $scope.$broadcast('requestVerificationToken', token);
    });


    /*************************
    HARDCODED
    /*************************/
    $rootScope.hardcodedFundraisingTargets = [
        {imageUrl: '/img/sunnies.png', title: 'Raise £180 or more', info: 'Sunglasses are essential Wonderland accessories - look cool as an alpine stream!'},
        {imageUrl: '/img/tee.png', title: 'Raise £250 or more', info: 'Also get a T-Shirt, specifically designed for Macmillan\'s Wonderland.'},
        {imageUrl: '/img/mystery.png', title: 'Raise £400 or more', info: 'On top of the sunnies and t-shirt, be entered into the prize draw to win a ski-tastic prize.'}
    ];

}]);