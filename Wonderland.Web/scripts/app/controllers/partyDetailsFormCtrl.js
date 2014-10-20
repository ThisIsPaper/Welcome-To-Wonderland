/*

 TODO - NEED THIS TO SPLIT OUT DATE AND TIME

 */

wonderlandApp.controller('PartyDetailsFormCtrl', ['$scope', function ($scope) {

    var hasSetInitialDate = false,
        minute = [0, 30];

    $scope.tempModel = {
        partyDate: null,
        partyTime: null,
        times: []
    };

    /**
     *
     * CREATE OPTIONS FOR TIME
     */
    var funcValueMinute = function (valueMinute) {

        var t = (i < 10 ? '0' : '') + i + ":" + (valueMinute < 10 ? '0' : '') + valueMinute;

        $scope.tempModel.times.push(t);
    };
    for (var i = 0; i < 24; i++) {
        angular.forEach(minute, funcValueMinute);
    }

    $scope.$watch('partyDetailsData.PartyDateTime', function (newVal, oldVal) {

        if (!hasSetInitialDate) {
            hasSetInitialDate = true;
            var mom = moment(newVal);
            if (mom.isValid()) {

                $scope.tempModel.partyDate = mom;
                $scope.tempModel.partyTime = $scope.tempModel.times[$scope.tempModel.times.indexOf(mom.format("HH:mm"))];
            }
        }

    });

    $scope.$watch('tempModel.partyDate + tempModel.partyTime', function () {

        if (!$scope.tempModel.partyDate || !$scope.tempModel.partyTime) {
            return;
        }

        $scope.partyDetailsData.PartyDateTime = moment($scope.tempModel.partyDate.format("YYYY/MM/DD") + " " + $scope.tempModel.partyTime).format("YYYY/MM/DD HH:mm:SS");

    }, true);

}]);