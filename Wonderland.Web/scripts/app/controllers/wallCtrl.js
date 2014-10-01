wonderlandApp.controller('WallCtrl', ['mHttp', 'safeApply', '$filter', '$scope', '$timeout', function (mHttp, safeApply, $filter, $scope, $timeout) {

    var wallFeed, partyGuid, feedRequest, initialFormModel, hasSetInitialFormModel=false;

    $scope.wall = {
        feedback: {
            imageProcessing: false,
            imageShowSuccess: false,
            imageShowError: false,

            messageProcessing: false
        },

        feed: null,
        formModel: null,
        previewImageUrl: null,
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
                    // TODO: need to remove this hardcoded time subtraction, server times differ, must be timezone issues
                    value.timestamp = moment(value.timestamp).subtract(1, 'hour').fromNow();

                    value.imageUrl = value.imageUrl.indexOf('null') >= 0 ? null : value.imageUrl;
                });
            }

            console.log('FEED', response);

            $scope.wall.hasDoneFirstLoad = true;
            $scope.wall.feed = response;
        });

    };

    $scope.wallFormModelInit = function (formModel) {

        if (!hasSetInitialFormModel) {
            hasSetInitialFormModel = true;
            initialFormModel = formModel;
        }

        safeApply($scope, function () {
            $scope.wall.formModel = angular.copy(formModel);

            $scope.wall.previewImageUrl = null;
            $scope.wall.feedback.imageProcessing = false;
            $scope.wall.feedback.messageProcessing = false;
        });
    };

    $scope.$onRootScope('wallMessagePosted', function() {
        $scope.wallFormModelInit(initialFormModel);
        $scope.getFeed();
    });

    $scope.$onRootScope('wallImageUploadStart', function() {
        safeApply($scope, function () {
            $scope.wall.previewImageUrl = null;
            $scope.wall.formModel.PartyWallImage = null;
            $scope.wall.feedback.imageProcessing = true;
        });
    });

    $scope.$onRootScope('wallImageUrlUploaded', function(event, response) {

        /**
         * safe apply the feedback response
         */
        safeApply($scope, function () {
            $scope.wall.feedback.imageProcessing = false;

            if (response && response.Success === true && response.Message) {
                $scope.wall.feedback.imageShowSuccess = true;

                // TODO: are we leaving this as is. i.e. returning the full uri of image location an javascript stripping out just the guid to save?

                $scope.wall.previewImageUrl = response.Message;
                $scope.wall.formModel.PartyWallImage = $scope.wall.previewImageUrl.replace('/Uploads/PartyWall/', '');

            } else {
                $scope.wall.feedback.imageShowError = true;
            }

            $timeout(function () {
                $scope.wall.feedback.imageShowSuccess = false;
                $scope.wall.feedback.imageShowError = false;
            }, 5000);
        });
    });

}]);