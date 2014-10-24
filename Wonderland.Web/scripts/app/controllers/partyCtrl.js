wonderlandApp.controller('PartyCtrl', ['$filter', '$ocModal', '$rootScope', '$sce', '$scope', '$timeout', function ($filter, $ocModal, $rootScope, $sce, $scope, $timeout) {

    /*************************
     *
     * PARTY COPY
     *
     */
    $scope.partyCopyData = null;
    $scope.partyCopyDataForForm = null;
    $scope.partyCopyDataFeedback = {};

    $scope.partyCopyDataInit = function (partyCopyData) {

        $scope.partyCopyData = partyCopyData;

        $scope.partyCopyDataForForm = angular.copy($scope.partyCopyData);
        $scope.partyCopyData.CopyHtmlSafe = $sce.trustAsHtml($scope.partyCopyData.Copy);
    };

    $scope.$onRootScope('partyCopyDataUpdated', function (event, response, dataObject) {
        $scope.partyCopyDataFeedback.processing = false;
        $scope.partyCopyDataInit(dataObject);
        $ocModal.close('partyCopyModal');
    });

    $scope.$onRootScope('partyCopyDataErrored', function () {
        $scope.partyCopyDataFeedback.processing = false;
        $scope.partyCopyDataFeedback.showNetworkError = true;

        $timeout(function () {
            $scope.partyCopyDataFeedback.showNetworkError = false;
        }, 5000);
    });


    /************************
     *
     * PARTY DETAILS
     *
     */
    $scope.partyDetailsData = null;
    $scope.partyDetailsFormattedAddress = null;
    $scope.partyDetailsDataForForm = null;
    $scope.partyDetailsDataFeedback = {};

    $scope.partyDetailsDataInit = function (partyDetailsData) {

        angular.forEach(partyDetailsData, function (value, key) {
            partyDetailsData[key] = value === "null" ? null : value;
        });

        // convert date to javascript Date
        if (partyDetailsData && partyDetailsData.PartyDateTime) {
//            var forceFormattedDate = new Date(parseInt(partyDetailsData.PartyDateTime.replace(/\/Date\((-?\d+)\)\//gi, "$1") ));
            partyDetailsData.PartyDateTime = moment(partyDetailsData.PartyDateTime).format('YYYY/MM/DD HH:mm:SS');
        }

        //*** format address
        var address = [];
        if (partyDetailsData.Address1) address.push(partyDetailsData.Address1);
        if (partyDetailsData.Address2) address.push(partyDetailsData.Address2);
        if (partyDetailsData.TownCity) address.push(partyDetailsData.TownCity);
        if (partyDetailsData.Postcode) address.push(partyDetailsData.Postcode);

        $scope.partyDetailsFormattedAddress = $filter('addressJoiner')(address.join(":~:"), ":~:");

        $scope.partyDetailsData = partyDetailsData;
        $scope.partyDetailsDataForForm = angular.copy($scope.partyDetailsData);
    };

    $scope.$onRootScope('partyDetailsDataUpdated', function (event, response, dataObject) {

        $scope.partyDetailsDataFeedback.processing = false;

        if (response && response.Success === true) {
            dataObject.error = null;
            $scope.partyDetailsDataInit(dataObject);
            $ocModal.close('partyDetailsModal');
        } else if (response.Errors) {
            $scope.partyDetailsDataFeedback.error = response.Errors[0].ErrorMessages[0];
        }
    });

    $scope.$onRootScope('partyDetailsDataError', function () {

        $scope.partyDetailsDataFeedback.processing = false;
        $scope.partyDetailsDataFeedback.showNetworkError = true;

        $timeout(function () {
            $scope.partyDetailsDataFeedback.showNetworkError = false;
        }, 5000);
    });


    /************************
     *
     * SUGGESTED DONATION
     *
     */
    $scope.suggestedDonationData = null;
    $scope.suggestedDonationDataForForm = null;
    $scope.suggestedDonationDataFeedback = {};

    $scope.suggestedDonationDataInit = function (suggestedDonationData) {
        $scope.suggestedDonationData = suggestedDonationData;
        $scope.suggestedDonationDataForForm = angular.copy($scope.suggestedDonationData);
    };

    $scope.$onRootScope('suggestedDonationDataUpdated', function (event, response, dataObject) {
        $scope.suggestedDonationDataFeedback.processing = false;
        $scope.suggestedDonationDataInit(dataObject);
        $ocModal.close('suggestedDonationModal');
    });

    $scope.$onRootScope('suggestedDonationDataError', function () {
        $scope.suggestedDonationDataFeedback.processing = false;
        $scope.suggestedDonationDataFeedback.showNetworkError = true;

        $timeout(function () {
            $scope.suggestedDonationDataFeedback.showNetworkError = false;
        }, 5000);
    });


    /************************
     *
     * FUNDRAISING TARGET
     *
     */
    $scope.fundraisingTargetData = null;
    $scope.fundraisingTargetDataForForm = null;
    $scope.fundraisingTargetDataFeedback = {};
    var fundraisingTargetFirstSet = true;

    $scope.fundraisingTargetDataInit = function (fundraisingTargetData) {

        $scope.fundraisingTargetData = fundraisingTargetData;
        $scope.fundraisingTargetDataForForm = angular.copy($scope.fundraisingTargetData);

        if (fundraisingTargetFirstSet) {
            fundraisingTargetFirstSet = false;
            if (fundraisingTargetData.FundraisingTarget <= 0) {
                $timeout(function () {
                    $rootScope.openPageModal('fundraisingTargetPartial', 'fundraisingTargetModal', {'fundraisingTargetData': $scope.fundraisingTargetDataForForm, 'hardcodedFundraisingTargets': $rootScope.hardcoded.fundraisingTargets, 'feedback': $scope.fundraisingTargetDataFeedback}, 'FundraisingTargetCtrl');
                });
            }
        }
    };

$scope.$onRootScope('fundraisingTargetDataUpdated', function (event, response, dataObject) {

    $scope.fundraisingTargetDataFeedback.processing = false;
    $scope.fundraisingTargetDataInit(dataObject);
    $ocModal.close('fundraisingTargetDataModal');
});

$scope.$onRootScope('fundraisingTargetDataError', function () {
    $scope.fundraisingTargetDataFeedback.processing = false;
    $scope.fundraisingTargetDataFeedback.showNetworkError = true;

    $timeout(function () {
        $scope.fundraisingTargetDataFeedback.showNetworkError = false;
    }, 5000);
});


/****************************
 *
 * PARTY IMAGE DETAILS
 *
 */
var initUrl = null;
$scope.partyImageData = {};
$scope.partyImageDefaultData = null;
$scope.partyImageDataForForm = null;
$scope.partyImageDataFeedback = {};
$scope.partyCustomImage = {
    image: null,
    feedback: {}
};

$scope.partyImageDefaultDataInit = function (defaultImages) {

    $scope.partyImageDefaultData = defaultImages;

    // hacky check to see if the url is one of the default images
    var isCustom = true;
    if (initUrl) {
        angular.forEach(defaultImages, function (value) {
            isCustom = value.id === initUrl.id ? false : isCustom;
        });
    }

    if (isCustom) {
        $scope.partyCustomImage.image = initUrl;
    }

};

$scope.$onRootScope('partyImageUpdated', function (event, response) {

    $scope.partyImageDataFeedback.processing = false;
    $scope.partyImageData.PartyImage = $scope.partyImageDataForForm.PartyImage;

    $ocModal.close('partyImageModal');
});

$scope.$onRootScope('partyImageUpdateError', function () {
    $scope.partyImageDataFeedback.processing = false;
    $scope.partyImageDataFeedback.showNetworkError = true;

    $timeout(function () {
        $scope.partyImageDataFeedback.showNetworkError = false;
    }, 5000);
});


$scope.$onRootScope('partyImageCustomUrlUploaded', function (event, response) {
    $timeout(function () {
        $scope.partyCustomImage.feedback.processing = false;

        if (response && response.Success === true && response.Message) {

            var newImage = angular.fromJson(response.Message);
            $scope.partyCustomImage.feedback.showSuccess = true;

            if (newImage) {
                $scope.partyCustomImage.image = newImage;
                $scope.partyImageDataForForm.PartyImage = newImage;
            }
        } else {
            $scope.partyCustomImage.feedback.showError = true;
        }
    });

    $timeout(function () {
        $scope.partyCustomImage.feedback.showSuccess = false;
        $scope.partyCustomImage.feedback.showError = false;
    }, 5000);
});


$scope.hardCodedCurrentPartyImageUrlInit = function (imageData) {

    if (imageData) {
        initUrl = angular.fromJson(imageData);
    }

    $scope.partyImageData.PartyImage = initUrl;
    $scope.partyImageDataForForm = angular.copy($scope.partyImageData);
};


}])
;