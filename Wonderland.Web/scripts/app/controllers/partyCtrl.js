wonderlandApp.controller('PartyCtrl', ['safeApply', '$ocModal', '$sce', '$scope', '$timeout', function (safeApply, $ocModal, $sce, $scope, $timeout) {


    /**********************/
    /* PARTY COPY */
    /**********************/
    $scope.partyCopyData = null;
    $scope.partyCopyDataForForm = null;

    $scope.partyCopyDataInit = function (partyCopyData) {
        $scope.partyCopyData = partyCopyData;

        $scope.partyCopyDataForForm = angular.copy($scope.partyCopyData);
        $scope.partyCopyData.CopyHtmlSafe = $sce.trustAsHtml($scope.partyCopyData.Copy);
    };

    $scope.$onRootScope('partyCopyDataUpdated', function(event, response, dataObject) {
        console.log('partyCopyDataUpdated', response, dataObject);
        $scope.partyCopyDataInit(dataObject);
        $ocModal.close('partyCopyModal');
    });



    /**********************/
    /* PARTY DETAILS */
    /**********************/
    $scope.partyDetailsData = null;
    $scope.partyDetailsFormattedAddress = null;
    $scope.partyDetailsDataForForm = null;

    $scope.partyDetailsDataInit = function (partyDetailsData) {

        // convert date to javascript Date
        if (partyDetailsData && partyDetailsData.PartyDateTime) {
            partyDetailsData.PartyDateTime = moment(partyDetailsData.PartyDateTime).format('YYYY/MM/DD');
        }

        //*** format address
        var address = [];
        if (partyDetailsData.Address1) address.push(partyDetailsData.Address1);
        if (partyDetailsData.Address2) address.push(partyDetailsData.Address2);
        if (partyDetailsData.TownCity) address.push(partyDetailsData.TownCity);
        if (partyDetailsData.Postcode) address.push(partyDetailsData.Postcode);
        $scope.partyDetailsFormattedAddress = address.join(", ");

        console.log('partyDetailsDataInit', partyDetailsData);

        $scope.partyDetailsData = partyDetailsData;
        $scope.partyDetailsDataForForm = angular.copy($scope.partyDetailsData);
    };

    $scope.$onRootScope('partyDetailsDataUpdated', function(event, response, dataObject) {

        console.log('partyDetailsDataUpdated', response, dataObject);

        $scope.partyDetailsDataInit(dataObject);
        $ocModal.close('partyDetailsModal');
    });



    /**********************/
    /* PROFILE IMAGE DETAILS */
    /**********************/
    $scope.profileImage = {
        url: null,
        showFeedbackSuccess: false,
        showFeedbackError: false
    };


    $scope.profileImageUrlInit = function (profileImageUrl) {
        $scope.profileImage.url = profileImageUrl;
    };

    $scope.$onRootScope('profileImageUrlUpdated', function(event, response) {

        var timeoutTime = 3000;

        if (response && response.Success === true && response.Message) {
            $scope.profileImage.url = response.Message;
            $scope.profileImage.showFeedbackSuccess = true;

            // close modal
            $timeout(function () {
                $ocModal.close('profileImageModal');
            }, timeoutTime);

        } else {
            $scope.profileImage.showFeedbackError = true;
        }

        // hide feedback
        $timeout(function () {
            $scope.profileImage.showFeedbackSuccess = false;
            $scope.profileImage.showFeedbackError = false;
        }, timeoutTime);
    });



    /**********************/
    /* SUGGESTED DONATION */
    /**********************/
    $scope.suggestedDonationData = null;
    $scope.suggestedDonationDataForForm = null;

    $scope.suggestedDonationDataInit = function (suggestedDonationData) {
        $scope.suggestedDonationData = suggestedDonationData;
        $scope.suggestedDonationDataForForm = angular.copy($scope.suggestedDonationData);
    };

    $scope.$onRootScope('suggestedDonationDataUpdated', function(event, response, dataObject) {
        $scope.suggestedDonationDataInit(dataObject);
        $ocModal.close('suggestedDonationModal');
    });



    /**********************/
    /* FUNDRAISING TARGET */
    /**********************/
    $scope.fundraisingTargetData = null;
    $scope.fundraisingTargetDataForForm = null;

    $scope.fundraisingTargetDataInit = function (fundraisingTargetData) {
        $scope.fundraisingTargetData = fundraisingTargetData;
        $scope.fundraisingTargetDataForForm = angular.copy($scope.fundraisingTargetData);
    };

    $scope.$onRootScope('fundraisingTargetDataUpdated', function(event, response, dataObject) {
        $scope.fundraisingTargetDataInit(dataObject);
        $ocModal.close('fundraisingTargetDataModal');
    });



    /**********************/
    /* PARTY IMAGE DETAILS */
    /**********************/
    var initUrl = null;
    $scope.partyImageData = null;
    $scope.partyImageDataForForm = null;
    $scope.partyCustomImage = {
        url: null
    };

    $scope.partyImageDataInit = function (partyImageData) {

        console.log('partyImageDataInit', partyImageData);
        $scope.partyImageData = partyImageData;
        $scope.partyImageDataForForm = angular.copy(partyImageData);

        // TODO - remove
        hardCodedFixForRedundantInitialImage(initUrl);
    };

    $scope.$onRootScope('partyImageUpdated', function(event, response) {
        if (response && response.Success === true && response.Message) {

            // might have to only update a certain part of the $scope.partyImageData due to return data

//            $scope.partyImageDataInit(response.Message);
        }

        $scope.partyImageData.PartyImage = $scope.partyImageDataForForm.PartyImage;

        // TODO - need this to work before remove the hardcoded fix
//        hardCodedFixForRedundantInitialImage();

        $ocModal.close('partyImageModal');
    });

    $scope.$onRootScope('partyImageUrlUpdated', function(event, response) {
        if (response && response.Success === true && response.Message) {

            console.log('partyImageUrlUpdated', response.Message);
            safeApply($scope, function () {
                $scope.partyCustomImage.url = response.Message;
                $scope.partyImageDataForForm.PartyImage = $scope.partyCustomImage.url;
            });
        }
    });



    $scope.hardCodedCurrentPartyImageUrlInit = function (url) {
        console.log('hardCodedCurrentPartyImageUrlInit', url);

        // TODO - remove 2 lines
        initUrl = url;
        hardCodedFixForRedundantInitialImage(url);

        if (url && url.indexOf('test_') >= 0) {
            return;
        }

        $scope.partyCustomImage.url = url;
    };


    // TODO - remove: eurgh - hardcoding this before it is put back into form
    var hardCodedFixForRedundantInitialImage = function (url) {
        safeApply($scope, function () {

            if (url && angular.isObject($scope.partyImageData) && angular.isObject($scope.partyImageDataForForm)) {
                $scope.partyImageData.PartyImage = url;
                $scope.partyImageDataForForm.PartyImage = url;
            }

        });
    };


}]);