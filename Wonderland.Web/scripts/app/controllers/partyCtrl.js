wonderlandApp.controller('PartyCtrl', ['$http', '$ocModal', '$scope', function ($http, $ocModal, $scope) {

    $scope.partyCopyData = null;


    $scope.partyCopyDataInit = function (partyCopyData) {
        console.log(partyCopyData);

        $scope.partyCopyData = partyCopyData;
    };

    $scope.openPageModal = function (partial) {

        $ocModal.open({
                          id: 'partyCopyModel',
                          url: partial,
                          controller: 'ModelPartyCopyCtrl',
                          init: {
                              partyCopyData: $scope.partyCopyData
                          },
                          onOpen: function (a, b, c, d, e, f) {console.log('onOpoen', a, b, c, d, e, f)},
                          onClose: function (a, b, c, d, e, f) { console.log('onClose', a, b, c, d, e, f)}
                      });

    };

}]);