/*

this will generate a random class based on the format:
radius-v1-small

m-radius-picker: attr
leave empty for a choice of small|medium|large
or specify to filter for only those sizes


 */

wonderlandApp.directive('mRadiusPicker', [function () {

    var NUM_OF_CLASS_VARIETIES = 3;
    var CLASS_SIZES = ['small', 'medium', 'large'];

    return {
        scope: false,
        link: function (scope, element, attrs) {


            var usedSizes = angular.copy(CLASS_SIZES);
            if (attrs.mRadiusPicker) {
                usedSizes = [];
                for (var i=0; i<CLASS_SIZES.length; i++) {
                    if (attrs.mRadiusPicker.indexOf(CLASS_SIZES[i]) >= 0) {
                        usedSizes.push(CLASS_SIZES[i]);
                    }
                }
            }

            if (!usedSizes.length) {
                return;
            }


            var version = Math.floor(Math.random()*(NUM_OF_CLASS_VARIETIES)) + 1;
            var size = usedSizes[ Math.floor(Math.random()*usedSizes.length) ];

            element.addClass( "radius-v" + version + "-" + size );

        }
    };

}]);