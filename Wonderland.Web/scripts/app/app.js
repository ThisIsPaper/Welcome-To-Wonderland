var wonderlandApp = angular.module('wonderlandApp', ['oc.modal']);

wonderlandApp.config(function ($httpProvider) {
    $httpProvider.defaults.headers.post['Content-Type'] = 'multipart/form-data';
});