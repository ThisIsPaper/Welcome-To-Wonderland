/*

 TODO - NEED THIS TO SPLIT OUT DATE AND TIME

 */

wonderlandApp.controller('PartyDetailsFormCtrl', ['$scope', '$init', function ($scope, $init) {

    var minute = [0, 30];

    $scope.tempModel = {
        partyDate: null,
        partyTime: null,
        times: []
    };

    /*********************
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

    var initialMoment = moment($init.partyDetailsData.PartyDateTime);
    if (initialMoment.isValid()) {
        $scope.tempModel.partyDate = initialMoment;
        $scope.tempModel.partyTime = $scope.tempModel.times[$scope.tempModel.times.indexOf(initialMoment.format("HH:mm"))];
    }


    $scope.$watch('tempModel.partyDate + tempModel.partyTime', function () {

        if (!$scope.tempModel.partyDate || !$scope.tempModel.partyTime) {
            return;
        }

        $scope.partyDetailsData.PartyDateTime = moment($scope.tempModel.partyDate.format("YYYY/MM/DD") + " " + $scope.tempModel.partyTime).format("YYYY/MM/DD HH:mm:SS");

    }, true);

}]);