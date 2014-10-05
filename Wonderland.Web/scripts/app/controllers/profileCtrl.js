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

        profileBillingAddressProcessing: false,
        profileBillingAddressShowSuccess: false,
        profileBillingAddressShowError: false,

        profilePasswordProcessing: false,
        profilePasswordShowSuccess: false,
        profilePasswordShowError: false,

        profileShirtProcessing: false,
        profileShirtShowSuccess: false,
        profileShirtShowError: false
    };



    /***
     *
     * NAME CHANGE
     *
     */
    $scope.$onRootScope('profileNameDataUpdated', function(event, response, dataObject) {

        safeApply( $scope, function () {

            $scope.feedback.profileNameProcessing = false;

            if (response && response.Success === true) {
                $scope.pageProfile.firstName = dataObject.FirstName;
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



    /**
     *
     *
     * BILLING ADDRESS CHANGE
     *
     */
    $scope.$onRootScope('profileBillingAddressDataUpdated', function(event, response, dataObject) {

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


    /***
     *
     *
     * PASSWORD CHANGE
     *
     */
    $scope.$onRootScope('profilePasswordUpdated', function(event, response, dataObject) {

        safeApply( $scope, function () {

            $scope.feedback.profilePasswordProcessing = false;

            if (response && response.Success === true) {
                $scope.profilePasswordData = null;
                $scope.feedback.profilePasswordShowSuccess = true;
            } else {
                $scope.feedback.profilePasswordShowError = true;
            }


            $timeout(function () {
                $scope.feedback.profilePasswordShowSuccess = false;
                $scope.feedback.profilePasswordShowError = false;
            }, 5000);

        });

    });


    /***
     *
     *
     * T-SHIRT CHANGE
     *
     */
    $scope.$onRootScope('profileShirtUpdated', function(event, response) {

        safeApply( $scope, function () {

            $scope.feedback.profileShirtProcessing = false;

            if (response && response.Success === true) {
                $scope.feedback.profileShirtShowSuccess = true;
            } else {
                $scope.feedback.profileShirtShowError = true;
            }


            $timeout(function () {
                $scope.feedback.profileShirtShowSuccess = false;
                $scope.feedback.profileShirtShowError = false;
            }, 5000);

        });

    });


}]);