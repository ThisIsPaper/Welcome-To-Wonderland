wonderlandApp.controller('PartyCtrl', ['$filter', '$ocModal', '$rootScope', '$sce', '$scope', '$timeout', function ($filter, $ocModal, $rootScope, $sce, $scope, $timeout) {


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

    $scope.$onRootScope('partyCopyDataUpdated', function (event, response, dataObject) {
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

        $scope.partyDetailsDataForForm.processing = false;

        if (response && response.Success === true) {
            dataObject.error = null;
            $scope.partyDetailsDataInit(dataObject);
            $ocModal.close('partyDetailsModal');
        } else if (response.Errors) {
            $scope.partyDetailsDataForForm.error = response.Errors[0].ErrorMessages[0];
        }
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

    $scope.$onRootScope('suggestedDonationDataUpdated', function (event, response, dataObject) {
        $scope.suggestedDonationDataInit(dataObject);
        $ocModal.close('suggestedDonationModal');
    });


    /**********************/
    /* FUNDRAISING TARGET */
    /**********************/
    $scope.fundraisingTargetData = null;
    $scope.fundraisingTargetDataForForm = null;
    var fundraisingTargetFirstSet = true;

    $scope.fundraisingTargetDataInit = function (fundraisingTargetData) {

        $timeout(function () {
                     $scope.fundraisingTargetData = fundraisingTargetData;
                     $scope.fundraisingTargetDataForForm = angular.copy($scope.fundraisingTargetData);

                     if (fundraisingTargetFirstSet) {
                         fundraisingTargetFirstSet = false;
                         if (fundraisingTargetData.FundraisingTarget <= 0) {
                             $rootScope.openPageModal('partials/fundraisingTargetForm.html', 'suggestedDonationModal', {'fundraisingTargetData': $scope.fundraisingTargetDataForForm});
                         }
                     }
                 }
        );
    };

    $scope.$onRootScope('fundraisingTargetDataUpdated', function (event, response, dataObject) {

        $scope.fundraisingTargetDataInit(dataObject);
        $ocModal.close('fundraisingTargetDataModal');
    });


    /**********************/
    /* PARTY IMAGE DETAILS */
    /**********************/
    var initUrl = null;
    $scope.partyImageData = null;
    $scope.partyImageDefaultData = null;
    $scope.partyImageDataForForm = null;
    $scope.partyCustomImage = {
        url: null
    };

    $scope.partyImageDefaultDataInit = function (defaultImages) {

        $scope.partyImageDefaultData = defaultImages;


        // hacky check to see if the url is one of the default images
        if ($scope.partyImageDefaultData.indexOf(initUrl) >= 0) {
            return;
        }

        $scope.partyCustomImage.url = initUrl;

    };

    $scope.$onRootScope('partyImageUpdated', function (event, response) {

        $scope.partyImageData.PartyImage = $scope.partyImageDataForForm.PartyImage;

        $ocModal.close('partyImageModal');
    });


    $scope.$onRootScope('partyImageCustomUrlUploaded', function (event, response) {
        if (response && response.Success === true && response.Message) {

            $timeout(function () {
                $scope.partyCustomImage.url = response.Message;
                $scope.partyImageDataForForm.PartyImage = $scope.partyCustomImage.url;
            });
        }
    });


    $scope.hardCodedCurrentPartyImageUrlInit = function (url) {

        initUrl = url;

        $scope.partyImageData = {
            'PartyImage': url
        };
        $scope.partyImageDataForForm = angular.copy($scope.partyImageData);
    };


}]);