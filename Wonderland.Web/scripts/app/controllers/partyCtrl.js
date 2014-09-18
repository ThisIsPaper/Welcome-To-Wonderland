wonderlandApp.controller('PartyCtrl', ['safeApply', '$ocModal', '$sce', '$scope', function (safeApply, $ocModal, $sce, $scope) {


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
    $scope.partyDetailsDataForForm = null;

    $scope.partyDetailsDataInit = function (partyDetailsData) {

        // convert date to javascript Date
        if (partyDetailsData && partyDetailsData.PartyDateTime) {
            partyDetailsData.PartyDateTime = moment(partyDetailsData.PartyDateTime).format('YYYY/MM/DD');
        }

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
    $scope.profileImageUrl = null;

    $scope.profileImageUrlInit = function (profileImageUrl) {
        $scope.profileImageUrl = profileImageUrl;
    };

    $scope.$onRootScope('profileImageUrlUpdated', function(event, response) {
        if (response && response.Success === true && response.Message) {
            $scope.profileImageUrlInit(response.Message);
        }

        $ocModal.close('profileImageModal');
    });



    /**********************/
    /* SUGGESTED DONATION */
    /**********************/
    $scope.suggestedDonationData = null;
    $scope.suggestedDonationDataForForm = null;

    $scope.suggestedDonationDataInit = function (suggestedDonationData) {
        console.log('suggestedDonationDataInit', suggestedDonationData);
        $scope.suggestedDonationData = suggestedDonationData;
        $scope.suggestedDonationDataForForm = angular.copy($scope.suggestedDonationData);
    };

    $scope.$onRootScope('suggestedDonationDataUpdated', function(event, response, dataObject) {
        console.log('partyCopyDataUpdated', response, dataObject);
        $scope.suggestedDonationDataInit(dataObject);
        $ocModal.close('suggestedDonationModal');
    });


}]);