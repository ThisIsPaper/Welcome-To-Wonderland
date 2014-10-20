wonderlandApp.directive('mVerticalAlign', ['debounce', '$timeout', '$window', function (debounce, $timeout, $window) {

    return {

        link: function (scope, element, attrs) {

            var hasInited = false,
                isActive = false,
                $possibleParent = $(element).closest("." + attrs.mAlignParent),
                $myParent = $possibleParent && $possibleParent.length ? $possibleParent[0] : $(element).parent(),

                reAlign = debounce(function () {
                    element.css('top', (parentHeight() - myHeight())/2);
                }, 200),
                parentHeight = function () { return $($myParent).height() + Number($($myParent).css('padding-top').replace('px', '')) + Number($($myParent).css('padding-bottom').replace('px', ''));},
                myHeight = function () { return $(element).height() + Number($(element).css('padding-top').replace('px', '')) + Number($(element).css('padding-bottom').replace('px', '')); };


            attrs.$observe('mVerticalAlign', function (newVal) {

                if (!hasInited && (newVal === 'true' || newVal === true)) {
                    isActive = true;

                    angular.element($window).bind('resize', reAlign);

                    reAlign();
                    $timeout(reAlign, 500);
                }

                if (!hasInited) {
                    hasInited=true;
                }

                if (newVal !== 'true' && newVal !== true) {
                    isActive = false;
                }
            });


            scope.$on('childImageLoadedBroadcast', function () {
                if (isActive) {
                    reAlign();
                }
            });

        }

    };

}]);