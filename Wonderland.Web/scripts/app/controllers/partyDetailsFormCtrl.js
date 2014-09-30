/*

TODO - NEED THIS TO SPLIT OUT DATE AND TIME

 */

wonderlandApp.controller('PartyDetailsFormCtrl', ['$scope', function ($scope) {

    var hasSetInitialDate = false,
        minute = [0, 30];

    /**
     *
     * CREATE OPTIONS FOR TIME
     */
    $scope.times = [];
    for (var i=0; i<24; i++) {
        angular.forEach(minute, function (valueMinute) {
            $scope.times.push((i<10?'0':'') + i + ":" + (valueMinute<10?'0':'') + valueMinute);
        });
    };



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