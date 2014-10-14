wonderlandApp.directive('mSelectOnClick', function () {
    return {
        restrict: 'A',
        link: function (scope, element) {

            element.on('mouseup', function () {
                this.select();
            });
        }
    };
});