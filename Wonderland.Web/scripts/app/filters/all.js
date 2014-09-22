wonderlandApp.filter('datePreviewFormat', [function () {

    return function (dateString) {

        var newFormattedDate = "";

        if (dateString) {

            newFormattedDate = moment(dateString).format('dddd D MMM, YYYY');

        }

        return newFormattedDate;

    };

}]);



wonderlandApp.filter('charsRemaining', [function () {

    return function (count, total) {

        var remaining = 0;

        if (angular.isNumber(count) && angular.isNumber(total)) {

            remaining = total - count;
            remaining = remaining < 0 ? 0 : remaining;

        }

        return remaining;

    };

}]);