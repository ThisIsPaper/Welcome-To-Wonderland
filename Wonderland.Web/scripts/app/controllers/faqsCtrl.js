wonderlandApp.controller('FaqsCtrl', ['$scope', function ($scope) {

    $scope.toggleFaq = function (target) {

        $scope[target] = !$scope[target];
    };

}]);