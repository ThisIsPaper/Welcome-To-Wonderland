wonderlandApp.controller('WallCtrl', ['mHttp', '$filter', '$scope', '$timeout', function (mHttp, $filter, $scope, $timeout) {

    var wallFeed, partyGuid, wallDelete, deleteRequest, feedRequest, initialFormModel, hasSetInitialFormModel = false;

    $scope.wall = {
        feedback: {
            imageProcessing: false,
            imageShowSuccess: false,
            imageShowError: false,

            messageProcessing: false,
            messageShowSuccess: false,
            messageShowError: false,

            feedProcessingPost: false,

            removeProcessingId: null
        },

        feed: null,
        feedLimit: 10,
        feedLastDate: null,
        feedLastResponseCount: 0,
        formModel: null,
        previewImageUrl: null,
        hasDoneFirstLoad: false
    };

    $scope.init = function (baseWallFeed, basePartyGuid, baseWallFeedDelete) {
        wallFeed = baseWallFeed;
        partyGuid = basePartyGuid;
        wallDelete = baseWallFeedDelete;

        $scope.getFeed();
    };


    $scope.getFeed = function (beforeDateTime) {

        $timeout(function () {
            $scope.wall.feedback.feedProcessingPost = true;
        });

        var sendFormData = {
            'partyGuid': partyGuid,
            'take': $scope.wall.feedLimit
        };
        if (beforeDateTime) {
            sendFormData.beforeDateTime = beforeDateTime;
        }

        feedRequest = mHttp.get(wallFeed, {
            data: sendFormData,
            dataType: 'json'
        });

        feedRequest.then(function (response) {

            $scope.wall.feedback.feedProcessingPost = false;

            // TODO: hardcoded - work out donations and re-format
            if (response) {

                angular.forEach(response, function (value) {
                    if (Number(value.id < 0)) { // is a donation
                        value.isDonation = true;
                        value.text = $filter('mCurrency')(value.text, "£");
                    }
                    value.timeFormatted = moment(value.timestamp).fromNow();
                    value.text = value.text == "null" ? null : value.text;
                    value.imageUrl = value.imageUrl && value.imageUrl.indexOf('null') >= 0 ? null : value.imageUrl;
                });
            }

            if (response.length) {
                $scope.wall.feedLastResponseCount = response.length;
                $scope.wall.feedLastDate = response[(response.length - 1)].timestamp;
            }

            $scope.wall.hasDoneFirstLoad = true;
            $scope.wall.feed = beforeDateTime ? $scope.wall.feed.concat(response) : response;
        });

    };

    $scope.deleteFeedItem = function (messageId) {

        if (!wallDelete || !messageId || messageId < 0) {
            return;
        }

        $scope.wall.feedback.removeProcessingId = messageId;

        deleteRequest = mHttp.post(wallDelete + '?messageId=' + messageId, {
            dataType: 'json'
        });

        deleteRequest.then(
            function (response) { // success

                var removeIndex = null;
                angular.forEach($scope.wall.feed, function (value, key) {
                    removeIndex = value.id === messageId ? key : removeIndex;
                });

                if (removeIndex >= 0) {
                    $scope.wall.feed.splice(removeIndex, 1);
                }
                $scope.wall.feedback.removeProcessingId = null;
            },
            function (response) { // failed
                $scope.wall.feedback.removeProcessingId = null;
            }
        );

    };


    $scope.wallFormModelInit = function (formModel) {

        if (!hasSetInitialFormModel) {
            hasSetInitialFormModel = true;
            initialFormModel = formModel;
        }

        $timeout(function () {
            $scope.wall.formModel = angular.copy(formModel);

            $scope.wall.previewImageUrl = null;
            $scope.wall.feedback.imageProcessing = false;
            $scope.wall.feedback.messageProcessing = false;
        });
    };

    $scope.$onRootScope('wallMessagePosted', function (event, response, dataObject) {

        if (response && response.Success === true) {
            $scope.wall.feedback.messageShowSuccess = true;
            $scope.wallFormModelInit(initialFormModel);
            $scope.getFeed();
        } else {
            $scope.wall.feedback.messageProcessing = false;
            $scope.wall.feedback.messageShowError = true;
        }


        $timeout(function () {
            $scope.wall.feedback.messageShowSuccess = false;
            $scope.wall.feedback.messageShowError = false;
        }, 5000);

    });

    $scope.$onRootScope('wallMessagePostError', function () {

        $scope.wall.feedback.messageProcessing = false;
        $scope.wall.feedback.messageShowError = true;

        $timeout(function () {
            $scope.wall.feedback.messageShowSuccess = false;
            $scope.wall.feedback.messageShowError = false;
        }, 5000);

    });

    $scope.$onRootScope('wallImageUploadStart', function () {
        $timeout(function () {
            $scope.wall.previewImageUrl = null;
            $scope.wall.formModel.PartyWallImage = null;
            $scope.wall.feedback.imageProcessing = true;
        });
    });

    $scope.$onRootScope('wallImageUrlUploaded', function (event, response) {

        /**
         * safe apply the feedback response
         */
        $timeout(function () {
            $scope.wall.feedback.imageProcessing = false;

            if (response && response.Success === true && response.Message) {
                $scope.wall.feedback.imageShowSuccess = true;

                var imageObject = angular.fromJson(response.Message);

                $scope.wall.previewImageUrl = imageObject;
                $scope.wall.formModel.PartyWallImage = imageObject.id;

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