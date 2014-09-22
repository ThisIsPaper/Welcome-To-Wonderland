wonderlandApp.filter('datePreviewFormat', [function () {

    return function (dateString) {

        var newFormattedDate = "";

        if (dateString) {

            newFormattedDate = moment(dateString).format('dddd D MMM, YYYY');

        }

        return newFormattedDate;

    };

}]);