wonderlandApp.directive('mFacebookLoginButton', ['facebook', 'mHttp', '$timeout', '$window', function (facebook, mHttp, $timeout, $window) {

    return {

        restrict: 'A',
        replace: true,
        scope: {
            label: '='
        },
        template: '<div>' +
            '<div class="btn-facebook-login radius-v2-small" ng-click="login()" ng-class="{\'disabled\': loading}" ng-bind="label"></div>' +
            '<div class="loader pos-static" ng-show="loading"></div>' +
            '<div class="successful" ng-show="formattedDetails">The user has logged in via Facebook! Hendy at this stage, we need to log the users in using their facebook app id.</div>' +
            '<div class="errorred" ng-show="showError" ng-bind="errorMessage"></div>' +
            '</div>',

        link: function (scope, element, attrs) {

            scope.loading = false;
            scope.formattedDetails = null;
            scope.showError = false;
            scope.errorMessage = attrs['errorMessage'] || "Oops, something went wrong!";


            var loginUrl = attrs['loginUrl'],
                fbStatus = null,
                fbLogin = null,
                loginToServer = function () {

                    if (!loginUrl) {
                        return;
                    }

                    var auth = facebook.authenticationDetails().authResponse,
                        jsonData = {
                            'accessToken': auth.accessToken,
                            'userId': auth.userID,
                            'signedRequest': auth.signedRequest
                        },
                        postHeaders = {
                            'Content-Type': 'application/json'
                        };

                    if ("mAntiForgeryToken" in attrs) {
                        angular.extend(postHeaders, {
                            'RequestVerificationToken': attrs.mAntiForgeryToken
                        });
                    }

                    var serverLogin = mHttp.post(loginUrl, {
                        data: angular.toJson(jsonData),
                        headers: postHeaders
                    });
                    serverLogin.then(function (response) {
                        if (response && response.success === true) {

                            if ("message" in response && response.message.length > 0) {
                                $window.location = response.message;
                            } else {
                                $window.location.reload();
                            }

                        } else {

                            if (response && "errors" in response && response.errors.length > 0) {
                                scope.errorMessage = response.errors[0].errorMessages[0];
                            }

                            scope.loading = false;
                            scope.showError = true;
                        }

                        $timeout(function () {
                            scope.showError = false;
                        }, 5000);
                    });

                };


            /**
             * Method called by the login button to start the login dialog process
             */
            scope.login = function () {
                if (scope.loading) {
                    return;
                }
                scope.loading = true;

                fbStatus.then(function (status) {

                    if (status !== 'connected') {
                        fbLogin = facebook.doLogin();
                        fbLogin.then(function (newStatus) {

                            if (newStatus !== 'connected') {
                                scope.loading = false;
                            } else {

                                loginToServer();
                            }

                        });
                    } else {
                        loginToServer();
                    }
                });
            };


            /**
             * This needs to run on page load. If it is part of the login sequence
             * the login dialogs will be blocked as popups because they are an action
             * as a result of an ajax call.
             */
            fbStatus = facebook.getLoginStatus();

        }

    };

}]);