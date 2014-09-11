wonderlandApp.controller('PartyCtrl', ['$http', '$scope', function ($http, $scope) {

    $scope.basicModel = null;
    $scope.basicModelEditable = null;



    $scope.init = function (basicModel) {

        $scope.basicModel = angular.fromJson(basicModel);
        $scope.basicModelEditable = {
            'partyHeading': '1',
            'partyCopy': '2'
        };//angular.copy($scope.basicModel);

    };


    $scope.submitBasicModel = function () {



    };



}]);