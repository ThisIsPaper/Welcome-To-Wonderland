wonderlandApp.directive('mProfileImage', ['safeApply', function (safeApply) {

    var MAIN_CLASS = 'profile-image',
        NOT_SET_CLASS = 'profile-image-notset',
        ALLOWED_EXTENSIONS = ['.jpg', '.jpeg', '.png'];

    return {
        scope: {},
        link: function (scope, element, attrs) {

            scope.hasBackgroundImage = false;

            element.addClass(MAIN_CLASS);


            var loadImage = function (newImgSrc) {

                var img = new Image();

                $(img).on('load', function () {
                    element.css('background-image', 'url('+ newImgSrc +')');
                    safeApply(scope, function () {
                        scope.hasBackgroundImage = true;
                    });
                });

                $(img).attr('src', newImgSrc);
            };



            attrs.$observe('mProfileImage', function (newVal) {
                var hasExt = false;
                for (var i=0; i<ALLOWED_EXTENSIONS.length; i++) {
                    hasExt = newVal.toLowerCase().indexOf(ALLOWED_EXTENSIONS[i]) >= 0 ? true : hasExt;
                }

                if (!newVal || !angular.isString(newVal) || !hasExt) {

                    scope.hasBackgroundImage = false;
                    return;
                }

                loadImage(newVal);

            });


            scope.$watch('hasBackgroundImage', function (newVal, oldVal) {

                // weird onload bug if the url is hard coded
                if (newVal===false && oldVal===true) {
                    return;
                }

                if (newVal===true) {
                    element.removeClass(NOT_SET_CLASS);
                } else {
                    element.addClass(NOT_SET_CLASS);
                }

            });

        }
    };

}]);