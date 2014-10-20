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

            var newAddress = angular.copy(address);

            /**
             * Combine 3 line address into 2 lines
             */
            if (address.line3Field) {
                newAddress.line1Field = [address.line1Field, address.line2Field].join(", ");
                newAddress.line2Field = address.line3Field;
            }

            /**
             * If there is a companyField move everything down a line
             */
            if (address.companyField) {
                newAddress.line1Field = address.companyField;
                if (address.line2Field) {
                    newAddress.line2Field = [address.line1Field, address.line2Field].join(", ");
                } else {
                    newAddress.line2Field = address.line1Field;
                }
            }

            return newAddress;
        }
    };


    return returnObj;

}]);