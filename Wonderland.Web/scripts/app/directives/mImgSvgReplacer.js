wonderlandApp.directive('mImgSvgReplacer', [function () {

    return {

        link: function (scope, element, attrs) {

            if (!Modernizr.svg) {
                alert('image replace!');
            }

        }

    };

}]);