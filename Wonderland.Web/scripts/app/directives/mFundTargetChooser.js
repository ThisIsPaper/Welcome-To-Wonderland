/*

m-fund-target-chooser: scoped attr to set chosen or not
m-fund-target-chooser-value: scoped attr to determine chosen state

 */


wonderlandApp.directive('mFundTargetChooser', ['safeApply', function (safeApply) {

    return {

        scope: {
            chooser: '=mFundTargetChooser',
            value: '=mFundTargetChooserValue'
        },
        link: function (scope, element) {

            scope.$watch('value', function (newVal) {

                scope.chooser = ['100', '250', '300'].indexOf(String(newVal)) < 0;

            });

            element.bind('click', function () {

                safeApply(scope, function () { scope.value = 0; });

            });

        }

    };

}]);