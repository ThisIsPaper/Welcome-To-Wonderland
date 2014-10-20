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
    return function (current, total, capAt100) {
        var p = 0;
        capAt100 = capAt100 === null ? true : !!capAt100;

        current = Number(current);
        total = Number(total);

        if (!isNaN(current) && !isNaN(total) && total > 0) {
            p = Math.floor((current/total)*100);
            if (capAt100) {
                p = p > 100 ? 100 : p;
            }
        }

        p = p < 0 ? 0 : p;
        p = isNaN(p) ? 0 : p;

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
                if (angular.isString(originalParts[i]) && originalParts[i].length>0 && originalParts[i] !== "null") {
                    addressParts.push(originalParts[i]);
                }
            }
        }

        var finalAddress = addressParts.join(", ");
        if (finalAddress.length<=0) {
            finalAddress = "An address hasn't been set yet";
        }

        return finalAddress;
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
                if (col[i].complete===true) {
                    complete++;
                }
            }
        }

        return "(" + complete + " of " + total + ")";
    };

}]);

wonderlandApp.filter('mLimitTo', ['$filter', function ($filter) {

    return function (str, limit, suffix) {
        return $filter('limitTo')(str, limit) + (str.length>limit && suffix ? suffix : '');
    };

}]);

wonderlandApp.filter('mQuality', [function () {

    return function (str, quality) {
        var newStr = str;
        if (str && !isNaN(quality)) {
            newStr = str.replace(/quality=[0-9]+/i,"quality=" + quality);
        }
        return newStr;
    };

}]);

wonderlandApp.filter('mWidth', [function () {

    return function (str, width) {
        var newStr = str;
        if (str && !isNaN(width)) {
            newStr = str.replace(/width=\d+/i, "width=" + width);
        }
        return newStr;
    };

}]);

wonderlandApp.filter('forceNullString', [function () {

    return function (str) {
        return str || 'null';
    };

}]);