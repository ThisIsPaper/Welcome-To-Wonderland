wonderlandApp.controller('FundraisingTargetCtrl', ['$scope', '$init', function ($scope, $init) {

    $scope.fixedTargets = [180, 250, 400];
    $scope.showChoose = $scope.fixedTargets.indexOf($init.fundraisingTargetData.FundraisingTarget) < 0;

}]);