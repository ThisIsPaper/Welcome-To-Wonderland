wonderlandApp.directive('mProfileImage', [function () {

    var MAIN_CLASS = 'profile-image',
        NOT_SET_CLASS = 'profile-image-notset';

    return {
        scope: false,
        link: function (scope, element, attrs) {

            scope.hasBackgroundImage = false;

            element.addClass(MAIN_CLASS);



            attrs.$observe('mProfileImage', function (newVal) {

                if (!newVal || !angular.isString(newVal)) {

                    scope.hasBackgroundImage = false;

                    return;
                }

                scope.hasBackgroundImage = true;

                element.css('background-image', 'url('+ newVal +')');

            });



            scope.$watch('hasBackgroundImage', function (newVal) {

                if (newVal===true) {
                    element.removeClass(NOT_SET_CLASS);
                } else {
                    element.addClass(NOT_SET_CLASS);
                }

            });

        }
    };

}]);