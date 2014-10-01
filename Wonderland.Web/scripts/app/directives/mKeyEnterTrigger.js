wonderlandApp.directive('onKeyEnterTrigger', ['$rootScope', function($rootScope) {
    return function(scope, element, attrs) {

        var onEnterEvent = attrs.onKeyEnterTrigger;

        element.bind('keydown', function(e) {

            if (e.keyCode === 13) { // enter key

                if (onEnterEvent) {
                    $rootScope.$emit(onEnterEvent);
                }

                e.preventDefault();
                return false;
            }
        });
    };
}]);