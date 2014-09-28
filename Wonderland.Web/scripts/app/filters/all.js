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


wonderlandApp.filter('percentageRounder', function () {
    return function (current, total) {
        var p = 0;

        if (angular.isNumber(current) && angular.isNumber(total)) {
            p = Math.floor((current/total)*100);
            p = p > 100 ? 100 : p;
            p = p < 0 ? 0 : p;
        }
        return p;
    };
});


wonderlandApp.filter('mCurrency', ["$filter", function ($filter) {
    return function(amount, currencySymbol){
        var currency = $filter('currency');

        if(amount < 0){
            return currency(amount, currencySymbol).replace("(", "-").replace(")", "");
        }

        return currency(amount, currencySymbol);
    };
}]);