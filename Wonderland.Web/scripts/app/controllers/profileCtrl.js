wonderlandApp.controller('ProfileCtrl', ['safeApply', '$scope', '$timeout', function (safeApply, $scope, $timeout) {


    /***
     *
     * WHOLE PAGE FEEDBACK OBJECT
     *
     */
    $scope.feedback = {
        profileNameProcessing: false,
        profileNameShowSuccess: false,
        profileNameErrorMessage: null
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



}]);