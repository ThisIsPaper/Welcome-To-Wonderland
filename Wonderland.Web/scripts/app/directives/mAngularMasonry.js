(function() {
    "use strict";

    angular.module('masonry', ['ng']).directive('masonry', function($parse) {
        return {
            restrict: 'AC',
            link: function(scope, elem, attrs) {
                scope.items = [];
                var container = elem[0];
                var options = angular.extend({
                    itemSelector: '.item'
                }, JSON.parse(attrs.masonry));

                attrs.$observe('masonryFilterChange', function (newVal) {

                    if (!angular.isString(newVal)) {
                        return;
                    }
                    scope.update();
                });

                var masonry = scope.masonry = new Masonry(container, options);

                var debounceTimeout = 0;
                scope.update = function() {
                    if (debounceTimeout) {
                        window.clearTimeout(debounceTimeout);
                    }
                    debounceTimeout = window.setTimeout(function() {
                        debounceTimeout = 0;
                        masonry.reloadItems();
                        masonry.layout();
    
                        elem.children(options.itemSelector).css('visibility', 'visible');
                    }, 120);
                };
            }
        };
    }).directive('masonryTile', function() {
        return {
            restrict: 'AC',
            link: function(scope, element, attrs) {
                element.css('visibility', 'hidden');
                var master = element.parent('*[masonry]:first').scope(),
                    update = master.update;

                imagesLoaded(element.find('img')[0], update);
                element.ready(update);
            }
        };
    });
})();