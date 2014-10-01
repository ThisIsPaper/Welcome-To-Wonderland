/**
 * <div m-address-finder="{POSTCODE}" m-address=""></div>
 */

wonderlandApp.directive('mAddressFinder', ['paf', 'safeApply', function (paf, safeApply) {

    return {

        scope: {
            postcode: '@mAddressFinder',
            m: '=mAddress'
        },

        template:   '<div class="margin-bottom-large">' +
                        '<div class="btn-secondary" m-radius-picker="small" ng-click="findAddress()">Find postcode</div>' +
                        '<select ng-show="listOfAddresses" class="margin-bottom-large" ng-model="selectedAddressId" ng-options="address.streetAddressField for address in listOfAddresses" ></select>' +
                    '</div>',

        link: function (scope) {

            var pafRequest;
            scope.selectedAddressId = null;
            scope.listOfAddresses = null;

            scope.findAddress = function () {
                pafRequest = paf.getAddressFromPostcode(scope.postcode);
                pafRequest.then(
                    function (response) { // success
                        scope.listOfAddresses=response;
                    },
                    function () { // failed
                        scope.listOfAddresses=null;
                    }
                );
            };


            scope.$watch('selectedAddressId', function (newVal, oldVal) {

                console.log('selectedAddressId', newVal, oldVal);

            });


        }

    };

}]);