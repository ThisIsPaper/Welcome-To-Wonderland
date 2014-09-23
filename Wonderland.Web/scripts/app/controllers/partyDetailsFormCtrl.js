/*

TODO - NEED THIS TO SPLIT OUT DATE AND TIME

 */

wonderlandApp.controller('PartyDetailsFormCtrl', ['$scope', function ($scope) {

    var hasSetInitialDate = false;

    $scope.tempModel = {
        PartyDateTime: null
    };

    $scope.$watch('partyDetailsData.PartyDateTime', function (newVal, oldVal) {

        if (!hasSetInitialDate) {
            hasSetInitialDate = true;
            $scope.tempModel.PartyDateTime = moment(newVal);
        }

    });

    $scope.$watch('tempModel.PartyDateTime', function (newVal) {
        
        if (!newVal) {
            return;
        }

        $scope.partyDetailsData.PartyDateTime = newVal.format('YYYY/MM/DD');

    }, true);

}]);