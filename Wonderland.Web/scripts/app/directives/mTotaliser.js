/***
 *
 * HTML FORMAT
 *
 * <m-totaliser m-raised="" m-total="" m-rank="" m-total-rank=""></m-totaliser>
 *
 *
 */

wonderlandApp.directive('mTotaliser', ['$filter', function ($filter) {

    return {

        restrict: 'A',

        scope: {
            'total': '@mTotal'
        },

        template:   '<div>' +
                        '<span class="body-mega color-brand margin-right-tiny" ng-bind="raised|mCurrency:\'£\'"></span>' +
                        '<span class="dis-inline-block"><span> raised of the </span>' +
                        '<span class="body-highlight color-brand" ng-bind="total|mCurrency:\'£\'"></span>' +
                        '<span>&nbsp;target</span></span>' +
                    '</div>' +

                    '<div class="totaliser">' +
                        '<div class="current-total" ng-style="{\'width\': cappedCurrentPercentage + \'%\'}"></div>' +
                    '</div>' +

                    '<div class="form-info">' +
                        '<span class="float-left" ng-show="total>0" ng-cloak><span ng-bind="currentPercentage"></span>%</span>' +
                    '</div>',

        link: function (scope, element, attrs) {

            var updatePercentage = function () {

                var cp = $filter('percentageRounder')(scope.raised, scope.total, false);
                scope.currentPercentage = cp;
                var cpc = cp > 100 ? 100 : cp;
                scope.cappedCurrentPercentage = cpc;

            };

            attrs.$observe('mRaised', function (raised) {
                scope.raised = scope.$parent.$eval(raised);
                updatePercentage();
            });

            scope.$watch('total', updatePercentage);

        }

    };

}]);