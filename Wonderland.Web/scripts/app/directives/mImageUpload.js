wonderlandApp.directive('mImageUpload', ['fileUpload', '$rootScope', function(fileUpload, $rootScope) {
    return {
        scope: true,
        link: function(scope, element, attrs) {

            var onStartEvent = attrs.mOnStartEvent,
                onSuccessEvent = attrs.mOnSuccessEvent,
                resetInputValue = function () {
                    console.log('resetInputValue::form input', element.find('input[type=file]'));
                    element.find('input[type=file]').val(null);
                };

            scope.submitBlank = function () {
                resetInputValue();
                scope.submit();
            };

            scope.submit = function() {
                // TODO: Check file extension
                scope.uploading = true;


                // Fire off the start event
                if (onStartEvent) {
                    $rootScope.$emit(onStartEvent);
                }

                fileUpload.backgroundUpload(element.find('form')).then(function(response) {

                    scope.uploading = false;

                    // Fire off the success event
                    if (onSuccessEvent) {
                        $rootScope.$emit(onSuccessEvent, response);
                    }

                    // Clone and replace the element to clear the input value, so attempt
                    // to re-upload the same file works
                    resetInputValue();
                });
            };
        }
    };

}]);
