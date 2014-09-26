wonderlandApp.filter('datePreviewFormat', [function () {

    return function (dateString) {

        var newFormattedDate = "";

        if (dateString) {

            newFormattedDate = moment(dateString).format('dddd D MMM, YYYY');

        }

        return newFormattedDate;

    };

}]);


wonderlandApp.filter('dateSuffix', function() {

    var suffixes = ['st', 'nd', 'rd'];

    return function(num) {
        var suffix = '';
        if (angular.isNumber(num) && num>0) {
            var ind = [1, 2, 3].indexOf(num % 10);
            var lastTwoDigits = num % 100;

            if (ind >=0 && !(lastTwoDigits>10 && lastTwoDigits<20)) {
                suffix = suffixes[ind];
            } else {
                suffix = 'th';
            }
        }

        return num+suffix;
    };
});

wonderlandApp.filter('escape', function () {
    return function (url) {
        return encodeURIComponent(url);
    };
});