wonderlandApp.directive('mSelectOnClick', function () {
    return {
        restrict: 'A',
        link: function (scope, element) {

            element.on('mousedown', function () {
                this.select();
            });
        }
    };
});