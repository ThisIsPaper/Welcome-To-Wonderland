var wonderlandApp = angular.module('wonderlandApp', ['Mac', 'masonry', 'oc.modal']);


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


wonderlandApp.run(['$rootScope', '$ocModal', '$window', function ($rootScope, $ocModal, $window) {


    /**
     * ng-template workaround for IE7
     */
    var registeredTemplates = {};
    $rootScope.$on('newTemplatePartial', function (event, partialId, partialTemplate) {
        registeredTemplates[partialId] = partialTemplate;
    });


    $rootScope.currentPageUrl = $window.location.href;

    $rootScope.openPageModal = function (partial, modalId, initVars, controller) {

        if (!partial) {
            return;
        }

        var modal = {
            template: registeredTemplates[partial],
            init: initVars,
            controller: controller
        };

        if (modalId) {
            modal.id = modalId;
        }

        try {
            $ocModal.open(modal);
        } catch(er) {
            console.log('modal error', er);
        }

    };

    $rootScope.closePageModal = function () {
        try {
            $ocModal.close();
        } catch (er) {
            console.log('modal close error', er);
        }
    };

    moment.fn.toASP = function () {
//        return '/Date(' + (+this) + this.format('ZZ') + ')/'; // with timezone, maybe add later
        return '/Date(' + (+this) + ')/';
    };

}]);