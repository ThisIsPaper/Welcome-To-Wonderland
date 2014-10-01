/**
 * <div m-address-finder="{POSTCODE}" m-address="" m-trigger-event=""></div>
 */

wonderlandApp.directive('mAddressFinder', ['paf', 'safeApply', '$timeout', function (paf, safeApply, $timeout) {

    return {

        scope: {
            postcode: '@mAddressFinder',
            m: '=mAddress'
        },

        template:   '' +
                        '<div class="margin-bottom-medium"><div class="btn-secondary" m-radius-picker="small" ng-click="findAddressFromPostcode()">Find address</div></div>' +
                        '<select ng-show="listOfAddresses" class="margin-bottom-large" ng-model="selectedAddress" ng-options="address.streetAddressField for address in listOfAddresses">' +
                        '<option value="">-- Select your address --</option>' +
                        '</select>' +
                        '<div class="errorred" ng-show="showErrorPostcode">That appears to be an invalid postcode, please try again</div>' +
                        '<div class="errorred" ng-show="showErrorAddress">That addresses full details couldn\'t be loaded, try again</div>' +
                        '<div class="successful" ng-show="showSuccess">Your address has been pre-filled!</div>' +
                        '<div class="loader pos-static" ng-show="loading"></div>' +
                    '',

        link: function (scope, elements, attrs) {

            scope.loading = false;
            scope.selectedAddress = null;
            scope.listOfAddresses = null;
            scope.showSuccess = false;
            scope.showErrorPostcode = false;
            scope.showErrorAddress = false;

            var triggerEvent = attrs.mTriggerEvent,
                pafPoscodeRequest,
                pafAddressRequest,
                getFullAddressFromId = function () {

                    scope.loading = true;

                    pafAddressRequest = paf.getAddressFromIdField(scope.selectedAddress.idField);
                    pafAddressRequest.then(
                        function (response) {

                            scope.m.Address1 = response.line1Field;
                            scope.m.Address2 = response.line2Field;
                            scope.m.TownCity = response.postTownField;

                            showFeedback("showSuccess");

                            clearData();

                        },
                        function () {
                            showFeedback("showErrorAddress");
                        }
                    );

                },
                showFeedback = function (message) {

                    scope[message] = true;

                    $timeout(function () {
                        scope.showSuccess = false;
                        scope.showErrorPostcode = false;
                        scope.showErrorAddress = false;
                    }, 5000);
                },
                clearData = function () {
                    scope.loading = false;
                    scope.selectedAddress=null;
                    scope.listOfAddresses=null;
                };



            scope.findAddressFromPostcode = function () {
                scope.loading = true;
                pafPoscodeRequest = paf.getAddressFromPostcode(scope.postcode);
                pafPoscodeRequest.then(
                    function (response) { // success
                        scope.loading = false;
                        scope.listOfAddresses=response;
                    },
                    function () { // failed
                        showFeedback("showErrorPostcode");
                        clearData();
                    }
                );
            };



            scope.$watch('selectedAddress', function (newVal, oldVal) {

                if (newVal==null || newVal==oldVal) {
                    return;
                }

                getFullAddressFromId();

            });

            if (triggerEvent) {
                scope.$onRootScope(triggerEvent, function() {

                    safeApply(scope, function () {
                        scope.findAddressFromPostcode();
                    });

                });
            }


        }

    };

}]);