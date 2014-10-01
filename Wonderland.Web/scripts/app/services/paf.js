wonderlandApp.factory('paf', ['mHttp', '$q', function (mHttp, $q) {

    var FIND_BY_POSTCODE = "/umbraco/Api/PostcodeAnywhereApi/FindByPostcode",
        RETRIEVE_BY_ID = "/umbraco/Api/PostcodeAnywhereApi/RetrieveById";

    return {

        getAddressFromPostcode: function (postCode) {

            var deferred = $q.defer(),
                promise = deferred.promise;

            var formSubmitRequest = mHttp.get(FIND_BY_POSTCODE, {
                data: {
                    postcode: postCode
                },
                dataType: 'json'
            });


            formSubmitRequest.then(
                function (response) {

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

        },


        getAddressFromIdField: function (idField) {

            var deferred = $q.defer(),
                promise = deferred.promise;


            var formSubmitRequest = mHttp.get(RETRIEVE_BY_ID, {
                data: {
                    'idField': idField
                },
                dataType: 'json'
            });


            formSubmitRequest.then(
                function (response) {

                    if (response && response.length) {
                        deferred.resolve(response[0]);
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