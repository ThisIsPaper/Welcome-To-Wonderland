wonderlandApp.controller('PartyCtrl', ['safeApply', '$ocModal', '$scope', function (safeApply, $ocModal, $scope) {

    $scope.partyCopyData = null;


    $scope.partyCopyDataInit = function (partyCopyData) {
        $scope.partyCopyData = partyCopyData;
    };

    $scope.$onRootScope('partyCopyDataUpdated', function(event, response, dataObject) {
        console.log('partyCopyDataUpdated', event, response, dataObject);
        $scope.partyCopyData = dataObject;
        $ocModal.close('partyCopyModel');
    });

    $scope.openPageModal = function (partial) {

        $ocModal.open({
            id: 'partyCopyModel',
            url: partial,
            init: {
                partyCopyData: angular.copy($scope.partyCopyData)
            },
            onOpen: function () {console.log('onModalOpen')},
            onClose: function () { console.log('onModalClose')}
        });

    };

}]);