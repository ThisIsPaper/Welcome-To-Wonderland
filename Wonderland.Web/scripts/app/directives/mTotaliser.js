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
                        '<span class="body-mega" ng-bind="raised|mCurrency:\'£\'"></span>' +
                        '<span> raised of the </span>' +
                        '<span class="body-highlight" ng-bind="total|mCurrency:\'£\'"></span>' +
                        '<span> target</span>' +
                    '</div>' +

                    '<div class="totaliser">' +
                        '<div class="current-total" ng-style="{\'width\': currentPercentage + \'%\'}"></div>' +
                    '</div>' +

                    '<div class="form-info">' +
                        '<span class="float-left"><span ng-bind="currentPercentage"></span>%</span>' +
                        '<span class="float-right" ng-show="rank">Ranked #<span ng-bind="rank"></span><span ng-show="rankTotal"> of <span ng-bind="rankTotal"></span>&nbsp;<ng-pluralize count="rankTotal" when="{\'1\': \'party\', \'other\': \'parties\'}"></ng-pluralize></span></span>' +
                    '</div>',

        link: function (scope, element, attrs) {

            scope.$watch('raised + total', function () {

                console.log('raised', scope.raised);
                console.log('total', scope.total);

                scope.currentPercentage = $filter('percentageRounder')(scope.raised, scope.total);
            });

        }

    };

}]);