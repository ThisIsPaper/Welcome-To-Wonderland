wonderlandApp.directive('mDateTimePicker', ['$timeout', function ($timeout) {

    return {

        scope: {
            'theDate': '=mDateTimePicker'
        },

        link: function (scope, element, attrs) {

            var hasSetInitialValue = false,
                pikaday = null;

            scope.$watch('theDate', function (newVal, oldVal) {
                if (hasSetInitialValue) {
                    return;
                }

                hasSetInitialValue = true;

                var initialDate = null;

                if (moment(newVal).isValid()) {
                    initialDate = newVal;
                }

                if (!pikaday) {
                    createPicker(initialDate);
                }

            });






            var createPicker = function (initialDate) {

                var basePicka = {

                    field: element[0],
                    firstDay: 1,
                    format: "dddd Do MMMM, YYYY",
                    minDate: moment().toDate(),
                    maxDate: new Date('2015-12-31'),
                    yearRange: [2014,2015],

                    onSelect: function (date) {
                        scope.theDate = moment(date);
                    }
                };

                if (initialDate) {
                    angular.extend(basePicka, {
                        defaultDate: initialDate.toDate(),
                        setDefaultDate: true
                    });
                }

                pikaday = new Pikaday(basePicka);

            };

        }

    };

}]);