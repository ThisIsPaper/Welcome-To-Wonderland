/**
 * This is a horrid hack to allow both href and target to be set dynamically on a link in IE7
 */

wonderlandApp.directive('mLinkTarget', [function () {

    return {
        scope: {
            'mLinkTarget': '=mLinkTarget'
        },
        link: function (scope, element) {

            scope.$watch('mLinkTarget', function (link) {

                $(element).attr("href", link.link);
                $(element).attr("target", link.target);

            });

        }
    };

}]);