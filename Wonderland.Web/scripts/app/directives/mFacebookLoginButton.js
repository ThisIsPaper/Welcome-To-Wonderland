wonderlandApp.directive('mFacebookLoginButton', ['facebook', function (facebook) {

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

        link: function (scope) {

            scope.loading = false;
            scope.formattedDetails = null;

            var fbStatus = null,
                fbLogin = null,
                fbDetails = null,
                getUserDetails = function () {

                    fbDetails = facebook.getUserDetails();
                    fbDetails.then(function () {
                        scope.loading = false;
                        formatUserDetails();
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
                    if (status !== 'connected') {
                        // login
                        fbLogin = facebook.doLogin();
                        fbLogin.then(function (newStatus) {

                            if (newStatus !== 'connected') {
                                scope.loading = false;
                            } else {
                                getUserDetails();
                            }

                        });
                    } else { // already authorized
                        getUserDetails();
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