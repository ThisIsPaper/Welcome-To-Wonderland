/**
 * <div m-ios-version m-min-ios="6" m-max-ios="5" m-ios-add-class="classname" m-ios-remove-class="classname"></div>
 */

wonderlandApp.directive('mIosVersion', [function () {

    var iOS = parseFloat(
        ('' + (/CPU.*OS ([0-9_]{1,5})|(CPU like).*AppleWebKit.*Mobile/i.exec(navigator.userAgent) || [0, ''])[1])
            .replace('undefined', '3_2').replace('_', '.').replace('_', '')
    ) || false;


    return {
        restrict: 'A',
        link: function (scope, element, attrs) {

            var addClassName = attrs["mIosAddClass"],
                removeClassName = attrs["mIosRemoveClass"],
                applyClasses = function () {
                    if (addClassName) {
                        angular.element(element).addClass(addClassName);
                    }
                    if (removeClassName) {
                        angular.element(element).removeClass(removeClassName);
                    }
                };

            if (!iOS) {
                if (!("mClassOnlyOnIos" in attrs)) {
                    applyClasses();
                }
                return;
            }

            if ("mMinIos" in attrs && parseInt(attrs["mMinIos"], 10) > iOS) {
                return;
            }

            if ("mMaxIos" in attrs && parseInt(attrs["mMaxIos"], 10) < iOS) {
                return;
            }

            applyClasses();
        }
    };

}]);