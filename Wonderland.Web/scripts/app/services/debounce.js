/*
var myFunc = function() {};
var myFuncDebounced = debounce(myFunc, 10);

myFuncDebounced(1, 2, 3);
if (myFuncDebounced.running) {
}
myFuncDebounced.cancel()

*/
wonderlandApp.factory('debounce', function() {
    return function(fn, wait) {
        var TO = null,
            getWait = angular.isFunction(wait) ? wait : function() {
                return wait;
            },
            wrapper = function() {
                var self = this,
                    args = Array.prototype.slice.call(arguments);

                wrapper.cancel();
                TO = setTimeout(function() {
                    TO = null;
                    fn.apply(self, args);
                }, getWait());
            };

        wrapper.cancel = function() {
            if (TO !== null) {
                clearTimeout(TO);
                TO = null;
            }
        };

        wrapper.running = function() {
            return TO !== null;
        };

        return wrapper;
    };
});