var wonderlandApp = angular.module('wonderlandApp', ['masonry', 'oc.modal']);


wonderlandApp.config(['$provide', function($provide){

    $provide.decorator('$rootScope', ['$delegate', function ($delegate) {

        Object.defineProperty($delegate.constructor.prototype, '$onRootScope', {
            value: function(name, listener){
                var unsubscribe = $delegate.$on(name, listener);
                this.$on('$destroy', unsubscribe);

                return unsubscribe;
            },
            enumerable: false
        });

        return $delegate;
    }]);

}]);


wonderlandApp.run(['$rootScope', '$ocModal', function ($rootScope, $ocModal) {

    $rootScope.openPageModal = function (partial, modalId, initVars) {

        if (!partial) {
            return;
        }

        var modal = {
            url: partial,
            init: initVars
        };

        if (modalId) {
            modal['id'] = modalId;
        }

        $ocModal.open(modal);

    };


    moment.fn.toASP = function () {
//        return '/Date(' + (+this) + this.format('ZZ') + ')/'; // with timezone, maybe add later
        return '/Date(' + (+this) + ')/';
    }

}]);