wonderlandApp.directive('mImageUpload', ['fileUpload', '$rootScope', function(fileUpload, $rootScope) {
    return {
        scope: true,
        link: function(scope, element, attrs) {

            var onSuccessEvent = attrs.mOnSuccessEvent;

            scope.submit = function() {
                // TODO: Check file extension
                scope.uploading = true;

                fileUpload.backgroundUpload(element.find('form')).then(function(response) {

                    scope.uploading = false;

                    // Fire off the success event
                    if (onSuccessEvent) {
                        $rootScope.$emit(onSuccessEvent, response);
                    }

                    // Clone and replace the element to clear the input value, so attempt
                    // to re-upload the same file works
                    element.find('input[type=file]').val(null);
                });
            };
        }
    };

}]);
