wonderlandApp.controller('PartyDetailsFormCtrl', ['$scope', function ($scope) {

    $scope.tempModel = {
        dateTime: null
    };




    $scope.$watch('partyDetailsData.PartyDateTime', function (newVal, oldVal) {

        console.log('PartyDetailsFormCtrl::PartyDateTime', newVal, oldVal);

    }, true);

    $scope.$watch('tempModel.dateTime', function (newVal, oldVal) {

        console.log('tempModel.dateTime', newVal, oldVal);

        if (!newVal) {
            return;
        }

        $scope.partyDetailsData.PartyDateTime = newVal.format('YYYY/MM/DD');

    }, true);

}]);