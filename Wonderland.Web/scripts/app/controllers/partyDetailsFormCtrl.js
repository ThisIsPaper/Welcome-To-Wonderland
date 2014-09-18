wonderlandApp.controller('PartyDetailsFormCtrl', ['$scope', function ($scope) {

    $scope.tempModel = {
        dateTime: null
    };




    $scope.$watch('partyDetailsData.PartyDate', function (newVal, oldVal) {

        console.log('PartyDetailsFormCtrl::PartyDate', newVal, oldVal);

    }, true);

    $scope.$watch('tempModel.dateTime', function (newVal, oldVal) {

        console.log('tempModel.dateTime', newVal, oldVal);

        if (!newVal) {
            return;
        }

        $scope.partyDetailsData.PartyDate = newVal.format('YYYY/MM/DD');

    }, true);

}]);