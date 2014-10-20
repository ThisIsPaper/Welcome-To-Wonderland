wonderlandApp.factory('paf', ['mHttp', '$q', function (mHttp, $q) {

    var FIND_BY_POSTCODE = "/umbraco/Api/PostcodeAnywhereApi/FindByPostcode",
        RETRIEVE_BY_ID = "/umbraco/Api/PostcodeAnywhereApi/RetrieveById",
        returnObj = {

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

                        deferred.resolve(returnObj.formatAddress(response[0]));
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

        formatAddress: function (address) {

            var newAddress = angular.copy(address),
                combiners = [
                    address.companyField,
                    address.line1Field,
                    address.line2Field,
                    address.line3Field,
                    address.line4Field,
                    address.line5Field
                ],
                firstValidInLoop = true,
                remainder = [];

            /**
             * Combine the company field and lines 1-5 all together on add2
             */
            angular.forEach(combiners, function (value) {
                if (value) {
                    if (firstValidInLoop) {
                        firstValidInLoop = false;
                        newAddress.line1Field = value;
                    } else {
                        remainder.push(value);
                    }
                }
            });
            newAddress.line2Field = remainder.join(", ");

            return newAddress;
        }
    };


    return returnObj;

}]);