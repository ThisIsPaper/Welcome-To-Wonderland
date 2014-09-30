wonderlandApp.filter('datePreviewFormat', [function () {

    return function (dateString) {

        var newFormattedDate = "";

        if (dateString) {

            newFormattedDate = moment(dateString).format('dddd D MMM, YYYY') + " at " + moment(dateString).format('HH.mm');

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

        current = Number(current);
        total = Number(total);

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

        var formattedCurrency = currency(amount, currencySymbol);
        formattedCurrency = formattedCurrency.replace("(", "-").replace(")", "").replace(".00", "");

        return formattedCurrency;
    };
}]);


wonderlandApp.filter('addressJoiner', [function () {
    return function(addressString, separator){

        var addressParts = [];

        if (angular.isString(addressString)) {
            var originalParts = addressString.split(separator);

            for (var i=0; i<originalParts.length; i++) {
                if (angular.isString(originalParts[i]) && originalParts[i].length>0) {
                    addressParts.push(originalParts[i]);
                }
            }
        }

        return addressParts.join(", ");
    };
}]);

wonderlandApp.filter('striptags', [function () {

    return function (htmlString) {

        var ret = htmlString;

        if (angular.isString(htmlString)) {
            ret = htmlString.replace(/<[^>]+>/gi,"");
        }

        return ret;
    };

}]);

wonderlandApp.filter('formatAmountComplete', [function () {

    return function (col) {

        var complete = 0,
            total = col.length || 0;

        if (angular.isArray(col)) {
            for (var i=0; i<total; i++) {
                if (col[i]["complete"]===true) {
                    complete++;
                }
            }
        }

        return "(" + complete + " of " + total + ")";
    };

}]);