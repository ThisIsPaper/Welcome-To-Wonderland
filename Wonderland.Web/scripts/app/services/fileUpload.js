wonderlandApp.factory('fileUpload', ['$document', '$q', 'uniqueId',
                         function ( $document,   $q,   uniqueId) {
    return {
        /* Submit a form with one or more upload fields in the background
         *
         * The form should not have any kind of on submit handler. The
         * endpoint the form hits should return a json document with a
         * content type of text/html.
         * Returns a promise which resolves when the submit is complete
         */
        backgroundUpload: function(form) {
            var deferred = $q.defer();

            // Create a hidden iframe and configure the form to submit to
            // it. This allows us to do something that looks like an ajax
            // form submit, but able to upload files in older browsers.
            var formTarget = 'frame_form_target_' + uniqueId();
            var targetIframe = $('<iframe/>', {name: formTarget, id: formTarget}).appendTo($document.find('body'));
            form.attr('target', formTarget);

            console.log('fileUpload::targetIframe', formTarget);

            form.submit();

            targetIframe.load(function() {

                // TODO - handle form closure

                form.scope().$apply(function() {

                    var response = targetIframe[0].contentWindow.document.body.innerHTML;
                    response = response.replace(/<\/?[^>]+>/gi, '');

                    console.log('fileUpload::loaded', response);

                    targetIframe.remove();
                    deferred.resolve(angular.fromJson(response));
                });
            });

            return deferred.promise;
        }
    };
}]);
