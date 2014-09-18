wonderlandApp.controller('WallCtrl', ['mHttp', '$scope', function (mHttp, $scope) {

    var wallFeed, partyGuid, feedRequest;

    $scope.wall = {
        feed: null,
        formModel: null
    };

    $scope.init = function (baseWallFeed, basePartyGuid) {
        wallFeed = baseWallFeed;
        partyGuid = basePartyGuid;

        $scope.getFeed();
    };


    $scope.getFeed = function () {

        console.log('GET FEED!');

        feedRequest = mHttp.get(wallFeed, {
            data: {
                'partyGuid': partyGuid,
                'take': 10
            },
            dataType: 'json'
        });

        feedRequest.then(function (response) {
            console.log('Wall::feed', response);
            $scope.wall.feed = response;
        });

    };

    $scope.wallFormModelInit = function (m) {
        console.log('wallModelInit', m);
        $scope.wall.formModel = m;
    };

    $scope.$onRootScope('wallMessagePosted', function(event, response, dataObject) {
        console.log('wallMessagePosted', response, dataObject);
    });

}]);