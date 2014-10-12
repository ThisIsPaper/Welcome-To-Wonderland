wonderlandApp.factory('facebook', ['$window', '$q', function ($window, $q) {

    var fbDeferred = $q.defer(),
        fbReady = fbDeferred.promise,
        fbAuthentication = null,
        fbUserDetails = null,
        fbAppId = {
            live: '670543089727850'
        };


    $window.fbAsyncInit = function () {
        FB.init({
                    appId: fbAppId.live,
                    xfbml: false,
                    version: 'v2.1'
                });
        fbDeferred.resolve();
    };


    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {
            return;
        }
        js = d.createElement(s);
        js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));



    return {

        getLoginStatus: function () {
            /**
             * getLoginStatus will determine whether the user is logged into
             * facebook and whether they have authenticated the Wonderland app.
             * The method returns a promise that will return the logged in status.
             * It also saves the resulting authentication details.
             */
            var loginStatusDeferred = $q.defer();
            fbReady.then(function () {

                FB.getLoginStatus(function (response) {

                    fbAuthentication = response;
                    var resultingStatus = 'not_logged_in';
                    if (response.status === 'connected' || response.status === 'not_authorized') {
                        resultingStatus = response.status
                    }
                    loginStatusDeferred.resolve(resultingStatus);

                });
            });
            return loginStatusDeferred.promise;
        },

        doLogin: function () {
            /**
             * doLogin will open up the authentication dialog to allow the user
             * to connect to the app
             */
            var loginDeferred = $q.defer();
            FB.login(function (response) {

                console.log('facebook.doLogin::FB.login', response);
                fbAuthentication = response;
                var resultingStatus = 'not_logged_in';
                if (response.status === 'connected' || response.status === 'not_authorized') {
                    resultingStatus = response.status
                }
                loginDeferred.resolve(resultingStatus);

            }, {scope: 'public_profile,email'});

            return loginDeferred.promise;
        },

        getUserDetails: function () {

            /**
             * Get the users details
             */
            var userDeferred = $q.defer();
            FB.api('/me', function (response) {
                fbUserDetails = response;
                userDeferred.resolve(response);
            });
            return userDeferred.promise;
        },

        authenticationDetails: function () {
            return fbAuthentication;
        },

        userDetails: function () {
            return fbUserDetails;
        }

    };

}])
;