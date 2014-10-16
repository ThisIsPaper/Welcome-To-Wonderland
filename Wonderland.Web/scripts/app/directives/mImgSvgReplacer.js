wonderlandApp.directive('mImgSvgReplacer', [function () {

    return {

        link: function (scope, element, attrs) {

            if (!Modernizr.svg) {
                angular.element(element).attr('src', attrs["mImgSvgReplacer"]);
            }

        }

    };

}]);