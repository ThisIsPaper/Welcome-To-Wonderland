wonderlandApp.controller('WallCtrl', ['mHttp', '$filter', '$scope', function (mHttp, $filter, $scope) {

    var wallFeed, partyGuid, feedRequest;

    $scope.wall = {
        feed: null,
        formModel: null,
        hasDoneFirstLoad: false
    };

    $scope.init = function (baseWallFeed, basePartyGuid) {
        wallFeed = baseWallFeed;
        partyGuid = basePartyGuid;

        $scope.getFeed();
    };


    $scope.getFeed = function () {

        feedRequest = mHttp.get(wallFeed, {
            data: {
                'partyGuid': partyGuid,
                'take': 10
            },
            dataType: 'json'
        });

        feedRequest.then(function (response) {

            // TODO: hardcoded - work out donations and re-format
            if (response) {

                angular.forEach(response, function (value) {
                    if (!isNaN(Number(value.text))) {
                        value.wallPostType = "Donation";
                        value.text = $filter('mCurrency')(value.text, '£');
                    }
                    value.timestamp = moment(value.timestamp).fromNow();
                });
            }

            $scope.wall.hasDoneFirstLoad = true;
            $scope.wall.feed = response;
        });

    };

    $scope.wallFormModelInit = function (formModel) {
        $scope.wall.formModel = formModel;
    };

    $scope.$onRootScope('wallMessagePosted', function(event, response, dataObject) {
        $scope.wallFormModelInit();
        $scope.getFeed();
    });

    $scope.$onRootScope('wallImageUrlUploaded', function(event, response) {
        if (response && response.Success === true && response.Message) {

            var url = response.Message;

            console.log('wallImageUrlUploaded', url);

            $scope.wall.formModel.PartyWallImage = url;
        }
    });

}]);