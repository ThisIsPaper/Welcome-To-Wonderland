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
    $scope.partyDetailsForForm = null;

    $scope.partyDetailsDataInit = function (partyDetailsData) {

        console.log('partyDetailsDataInit', partyDetailsData);

        $scope.partyDetailsData = partyDetailsData;
        $scope.partyDetailsDataForForm = angular.copy($scope.partyDetailsData);
    };

    $scope.$onRootScope('partyDetailsDataUpdated', function(event, response, dataObject) {
        $scope.partyDetailsDataInit('partyDetailsDataUpdated', response, dataObject);
        $scope.partyDetailsDataInit(dataObject);
        $ocModal.close('partyDetailsModal');
    });


}]);