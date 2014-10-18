wonderlandApp.directive('mWindowClickToggle', ['safeApply', '$document', function (safeApply, $document) {

    return {
        scope: {
            toggle: '=mWindowClickToggle'
        },
        link: function (scope, element) {

            scope.toggle = false;

            $document.bind('click', function(event){
                var isClickedElementChildOfPopup = element
                    .find(event.target)
                    .length > 0;

                if (isClickedElementChildOfPopup)
                    return;

                safeApply(scope, function () {
                    scope.toggle = false;
                });
            });

        }
    };

}]);