wonderlandApp.factory('paf', ['mHttp', '$q', function (mHttp, $q) {

    var HARDCODED_URL = "/umbraco/Api/PostcodeAnywhereApi/FindByPostcode?";

    return {

        getAddressFromPostcode: function (postCode) {

            var deferred = $q.defer(),
                promise = deferred.promise;


            var formSubmitRequest = mHttp.get(HARDCODED_URL, {
                data: {
                    postcode: postCode
                },
                dataType: 'json'
            });


            formSubmitRequest.then(
                function (response) {

                    console.log('POSTCODE SUCCESS', response);

                    if (response && response.length) {
                        deferred.resolve(response);
                    } else {
                        deferred.reject();
                    }

                },
                function () {
                    deferred.reject();
                }
            );


            return promise;

        }

    };

}]);