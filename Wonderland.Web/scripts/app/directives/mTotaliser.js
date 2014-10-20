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

        restrict: 'AE',

        scope: {
            raised: '@mRaised',
            total: '@mTotal',
            rank: '@mRank',
            rankTotal: '@mRankTotal'
        },

        template:   '<div>' +
                        '<span class="body-mega color-brand margin-right-tiny" ng-bind="raised|mCurrency:\'£\'"></span>' +
                        '<span class="dis-inline-block"><span> raised of the </span>' +
                        '<span class="body-highlight color-brand" ng-bind="total|mCurrency:\'£\'"></span>' +
                        '<span> target</span></span>' +
                    '</div>' +

                    '<div class="totaliser">' +
                        '<div class="current-total" ng-style="{\'width\': cappedCurrentPercentage + \'%\'}"></div>' +
                    '</div>' +

                    '<div class="form-info">' +
                        '<span class="float-left" ng-show="total>0" ng-cloak><span ng-bind="currentPercentage"></span>%</span>' +
                        '<span class="float-right" ng-show="rank">Ranked #<span ng-bind="rank"></span><span ng-show="rankTotal"> of <span ng-bind="rankTotal"></span>&nbsp;<div ng-pluralize count="rankTotal" when="{\'1\': \'party\', \'other\': \'parties\'}"></div></span></span>' +
                    '</div>',

        link: function (scope) {

            scope.$watch('raised + total', function () {

                var cp = $filter('percentageRounder')(scope.raised, scope.total, false);
                scope.currentPercentage = cp;
                var cpc = cp > 100 ? 100 : cp;
                scope.cappedCurrentPercentage = cpc;
            });

        }

    };

}]);