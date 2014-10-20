wonderlandApp.factory('safeApply', [function() {
    // Usage:
    //    safeApply(scope)
    //       calls scope.$apply() if not already in a digest
    //    safeApply(scope, fn)
    //       calls scope.$apply(fn) if not already in a digest, fn(scope) otherwise
    return function(scope, fn) {

        if (!scope.$$phase) {
            scope.$apply(fn);
        } else if (fn) {
            fn(scope);
        }
    };
}]);