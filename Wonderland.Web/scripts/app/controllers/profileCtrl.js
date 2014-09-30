wonderlandApp.controller('ProfileCtrl', ['safeApply', '$scope', '$timeout', function (safeApply, $scope, $timeout) {


    /***
     *
     * WHOLE PAGE FEEDBACK OBJECT
     *
     */
    $scope.feedback = {
        profileNameProcessing: false,
        profileNameShowSuccess: false,
        profileNameErrorMessage: null,

        profileImageProcessing:  false,
        profileImageShowSuccess: false,
        profileImageShowError: false
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
                $timeout(function () {
                    $scope.feedback.profileNameShowSuccess = false;
                }, 5000);

            } else {

                $scope.feedback.profileNameErrorMessage = "Your changed name couldn't be saved";

            }

        });

    });


    /***
     *
     * PROFILE IMAGE CHANGE
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



}]);