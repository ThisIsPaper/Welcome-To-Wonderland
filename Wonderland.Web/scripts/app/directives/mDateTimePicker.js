wonderlandApp.directive('mDateTimePicker', ['safeApply', function (safeApply) {

    return {

        scope: {
            'theDate': '=mDateTimePicker'
        },

        link: function (scope, element) {

            var pikaday = new Pikaday({

                field: element[0],
                firstDay: 1,
                format: "dddd Do MMMM, YYYY",
//                defaultDate: moment().toDate(),
//                setDefaultDate: true,
                minDate: moment().toDate(),
                maxDate: new Date('2015-12-31'),
                yearRange: [2014,2015],

                onSelect: function (date) {

                    safeApply(scope, function () {
                        scope.theDate = moment(date);
                    });

                }
            });

//            scope.$watch('theDate', function (newVal, oldVal) {
//
//                console.log('theDate change', newVal, oldVal);
//
//                if (newVal.isValid()) {
//                    pikaday.setDate(newVal.toDate());
//                }
//
//            }, true);

        }

    };

}]);