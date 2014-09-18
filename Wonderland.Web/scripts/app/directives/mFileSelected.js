wonderlandApp.directive('mFileSelected', ['$parse', function($parse) {
    return {
        controller: function() {}, // controller required for prerequisites
        link: function(scope, element, attrs) {
            var fn = $parse(attrs.mFileSelected);

            element.on('change', function() {
                scope.$apply(function() {
                    fn(scope);
                });
            });
        }
    };
}]);
