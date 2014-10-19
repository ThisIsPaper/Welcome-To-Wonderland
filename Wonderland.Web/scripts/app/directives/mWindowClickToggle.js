wonderlandApp.directive('mWindowClickToggle', ['$document', '$timeout', function ($document, $timeout) {

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

                $timeout(function () {
                    scope.toggle = false;
                });
            });

        }
    };

}]);