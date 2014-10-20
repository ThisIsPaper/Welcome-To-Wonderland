wonderlandApp.directive('mFormTokenDispatcher', [function () {

    return {
        link: function (scope, element) {

            var $token = $(element).find('input[name="__RequestVerificationToken"]');
            if ($token) {
                scope.$emit('formToken', $token[0].value);
            }

        }
    };

}]);