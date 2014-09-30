wonderlandApp.controller('ProfileCtrl', ['safeApply', '$scope', '$timeout', function (safeApply, $scope, $timeout) {


    /***
     *
     * WHOLE PAGE FEEDBACK OBJECT
     *
     */
    $scope.feedback = {
        profileNameProcessing: false,
        profileNameShowSuccess: false,
        profileNameShowError: false,

        profileImageProcessing:  false,
        profileImageShowSuccess: false,
        profileImageShowError: false,

        profileBillingAddressProcessing: false,
        profileBillingAddressShowSuccess: false,
        profileBillingAddressShowError: false
    };



    /***
     *
     * NAME CHANGE
     *
     */
    $scope.$onRootScope('profileNameDataUpdated', function(event, response, dataObject) {
        console.log('Event::profileNameDataUpdated', response, dataObject);

        safeApply( $scope, function () {

            $scope.feedback.profileNameProcessing = false;

            if (response && response.Success === true) {
                $scope.feedback.profileNameShowSuccess = true;
            } else {
                $scope.feedback.profileNameShowError = true;
            }


            $timeout(function () {
                $scope.feedback.profileNameShowSuccess = false;
                $scope.feedback.profileNameShowError = false;
            }, 5000);

        });

    });


    /***
     *
     * PROFILE IMAGE CHANGE
     * maybe change this so this it belongs in the pageCtrl and used globally?
     *
     */
    $scope.$onRootScope('profileImageUploadStart', function(event, response, dataObject) {
        console.log('Event::profileImageUploadStart', response, dataObject);

        safeApply($scope, function () {
            $scope.feedback.profileImageProcessing = true;
        });

    });
    $scope.$onRootScope('profileImageUpdated', function(event, response, dataObject) {
        console.log('Event::profileImageUpdated', response, dataObject);


        /**
         * safe apply the feedback response
         */
        safeApply($scope, function () {
            $scope.feedback.profileImageProcessing = false;

            if (response && response.Success === true && response.Message) {
                $scope.pageProfile.imageUrl = response.Message;
                $scope.feedback.profileImageShowSuccess = true;
            } else {
                $scope.feedback.profileImageShowError = true;
            }

            $timeout(function () {
                $scope.feedback.profileImageShowSuccess = false;
                $scope.feedback.profileImageShowError = false;
            }, 5000);
        });


    });


    /**
     *
     *
     * BILLING ADDRESS CHANGE
     *
     */
    $scope.$onRootScope('profileBillingAddressDataUpdated', function(event, response, dataObject) {
        console.log('Event::profileBillingAddressDataUpdated', response, dataObject);

        safeApply( $scope, function () {

            $scope.feedback.profileBillingAddressProcessing = false;

            if (response && response.Success === true) {
                $scope.feedback.profileBillingAddressShowSuccess = true;
            } else {
                $scope.feedback.profileBillingAddressShowError = true;
            }

            $timeout(function () {
                $scope.feedback.profileBillingAddressShowSuccess = false;
                $scope.feedback.profileBillingAddressShowError = false;
            }, 5000);

        });

    });


}]);