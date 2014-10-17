wonderlandApp.directive('mFacebookLoginButton', ['facebook', 'mHttp', function (facebook, mHttp) {

    return {

        restrict: 'E',
        replace: true,
        scope: {
            label: '='
        },
        template: '<div>' +
            '<div class="btn-facebook-login radius-v2-small" ng-click="login()" ng-class="{\'disabled\': loading}" ng-bind="label"></div>' +
            '<div class="loader pos-static" ng-show="loading"></div>' +
            '<div class="successful" ng-show="formattedDetails">The user has logged in via Facebook! Hendy at this stage, we need to log the users in using their facebook app id.</div>' +
            '<div class="boxx internal highlight radius-v3-small" ng-show="formattedDetails">' +
            '<table>' +
            '<tr ng-repeat="deet in formattedDetails">' +
            '<td ng-bind="deet.key"></td>' +
            '<td ng-bind="deet.value"></td>' +
            '</tr>' +
            '</table>' +
            '</div>' +
            '</div>',

        link: function (scope, element, attrs) {

            scope.loading = false;
            scope.formattedDetails = null;

            var loginUrl = attrs['loginUrl'],
                fbStatus = null,
                fbLogin = null,
                fbDetails = null,
                getUserDetails = function () {

                    fbDetails = facebook.getUserDetails();
                    fbDetails.then(function () {
                        scope.loading = false;
                        formatUserDetails();
                    });
                },
                loginToServer = function () {

                    if (!loginUrl) {
                        return;
                    }

                    var auth = facebook.authenticationDetails().authResponse;
                    console.log('LOGIN', loginUrl, auth);
                    var serverLogin = mHttp.post(loginUrl, {
                        data: {
                            'accessToken': auth.accessToken,
                            'userId': auth.userID,
                            'signedRequest': auth.signedRequest
                        },
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    });
                    serverLogin.then(function (response) {
                        console.log('BANG', response);
                    });

                },
                formatUserDetails = function () {
                    var deets = [];

                    angular.forEach(facebook.authenticationDetails().authResponse, function (value, key) {
                        deets.push({
                                       'key': key,
                                       'value': value
                                   });
                    });

                    angular.forEach(facebook.userDetails(), function (value, key) {
                        deets.push({
                                       'key': key,
                                       'value': value
                                   });
                    });

                    if (deets.length) {
                        scope.formattedDetails = deets;
                    }
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
                    fbLogin = facebook.doLogin();
                    fbLogin.then(function (newStatus) {

                        if (newStatus !== 'connected') {
                            scope.loading = false;
                        } else {

                            loginToServer();
                        }

                    });
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