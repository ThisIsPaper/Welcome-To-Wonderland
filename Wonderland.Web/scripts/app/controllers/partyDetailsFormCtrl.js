wonderlandApp.controller('PartyDetailsFormCtrl', ['$scope', function ($scope) {

    $scope.tempModel = {
        dateTime: null
    };

    $scope.$watch('tempModel.dateTime', function (newVal) {
        
        if (!newVal) {
            return;
        }

        $scope.partyDetailsData.PartyDateTime = newVal.format('YYYY/MM/DD');

    }, true);

}]);