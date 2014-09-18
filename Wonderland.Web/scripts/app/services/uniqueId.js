/*
A simple service that provides a function that gives a unique ID (per page load)
*/
wonderlandApp.factory('uniqueId', function() {
    var counter = 0;

    return function() {
        counter++;
        return counter;
    };
});
