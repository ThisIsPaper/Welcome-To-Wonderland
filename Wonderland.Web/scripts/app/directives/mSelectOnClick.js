wonderlandApp.directive('mSelectOnClick', function () {
    return {
        restrict: 'A',
        link: function (scope, element) {

            var startEventType = 'mouseup';

            if (Modernizr.touch === true) {
                startEventType = 'touchstart';
            }

            element.on(startEventType, function () {
                this.select();
            });
        }
    };
});