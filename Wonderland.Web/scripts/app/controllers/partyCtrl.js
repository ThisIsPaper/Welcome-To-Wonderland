wonderlandApp.controller('PartyCtrl', ['safeApply', '$ocModal', '$sce', '$scope',
                                        function (safeApply, $ocModal, $sce, $scope) {

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

        console.log('partyDetailsDataInit', partyDetailsData);

        $scope.partyDetailsData = partyDetailsData;
        $scope.partyDetailsDataForForm = angular.copy($scope.partyDetailsData);
    };

    $scope.$onRootScope('partyDetailsDataUpdated', function(event, response, dataObject) {
        $scope.partyDetailsDataInit(dataObject);
        $ocModal.close('partyDetailsModal');
    });



    /**********************/
    /* PROFILE IMAGE DETAILS */
    /**********************/
    $scope.profileImageUrl = null;

    $scope.profileImageUrlInit = function (profileImageUrl) {

        console.log('profileImageUrlInit', profileImageUrl);

        $scope.profileImageUrl = profileImageUrl;
    };

    $scope.$onRootScope('profileImageUrlUpdated', function(event, response) {
        console.log('profileImageUrlUpdated', response);

        if (response && response.Success === true && response.Message) {
            $scope.profileImageUrlInit(response.Message);
        }

        $ocModal.close('profileImageModal');
    });


}]);