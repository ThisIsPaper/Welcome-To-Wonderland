wonderlandApp.directive('mPostOnSubmit', ['mHttp', 'uniqueId', '$parse', '$rootScope', '$timeout', function (mHttp, uniqueId, $parse, $rootScope, $timeout) {


    var SAVED_CONFIRMATION_PERIOD = 2000; // milliseconds


    return {
        scope: true,
        link: function (scope, element, attrs) {

            var progressState,
                progressVar = attrs.mProgressVar && $parse(attrs.mProgressVar),
                dataVar = $parse(attrs.mPostOnSubmit),
                formSubmitRequest = null,
                savedConfirmTimer = null,
                serialized = attrs.hasOwnProperty('mSerialized'),
                onSuccessEvent = attrs.mOnSuccessEvent;

            setProgressState('ready');


            element.on('submit', function () {

                if (serialized && mHttp.isInProgress(formSubmitRequest)) {
                    return;
                }

                prepareForSubmit();
                setProgressState('in-progress');

                var originalData = dataVar(scope) || {},
                    sendData = angular.copy(originalData);

                /**
                 * HTML 5 AJAX
                 */
                if (window.File && window.FileReader) {

                    $timeout(scope.$apply(function () {
                        // check for ufprt as hidden value
                        var ufprtElement = element.find("[name='ufprt']");
                        if (ufprtElement && ufprtElement.length) {
                            sendData.ufprt = ufprtElement[0].value;
                        }

                        // check for ufprt as hidden value
                        var rvtElement = element.find("[name='__RequestVerificationToken']");
                        if (rvtElement && rvtElement.length) {
                            sendData.__RequestVerificationToken = rvtElement[0].value;
                        }
                        formSubmitRequest = mHttp.post(attrs.action + '?t=' + new Date().getTime(), {
                            formData: sendData,
                            dataType: 'json'
                        });

                        formSubmitRequest.then(function (response) {
                            handleResponse(response, originalData);
                        }, function () {
                            setProgressState('ready');
                        });
                    }));


                    /**
                     * LEGACY (NON-HTML5) AJAX FALLBACK
                     */
                } else {


                    var target = 'form_target_iframe_' + uniqueId();

                    var $targetIframe = $('<iframe/>', { name: target, id: target, frameborder: '0' }).insertAfter(element).css({ width: 0, height: 0 }).load(function () {
                        $timeout(scope.$apply(function () {
                            var response = $targetIframe.contents().find("body").html();

                            console.log('RESPONSE', response);

                            handleResponse(JSON.parse(response), originalData);
                        }));
                    });

                    var $postForm = $('<form></form>', { target: target, method: 'post', enctype: 'multipart/form-data' }).insertAfter(element);
                    element.appendTo($postForm);

                    $postForm.attr('action', attrs.action);
                    $postForm.submit();

                }

                return false;
            });


            return;


            function handleResponse(response, originalData) {

                if (response && response.data) {
                    dataVar.assign(scope.$parent, response.data);
                }

                if (response && response.success === false) {
                    setProgressState('ready');
                } else {
                    setProgressState('saved');
                    savedConfirmTimer = $timeout(function () {
                        setProgressState('ready');
                        savedConfirmTimer = null;
                    }, SAVED_CONFIRMATION_PERIOD);

                    if (onSuccessEvent) {
                        $rootScope.$emit(onSuccessEvent, response, angular.copy(originalData));
                    }
                }

            }

            function setProgressState(state) {
                progressState = state;
                if (progressVar) {
                    progressVar.assign(scope.$parent, state);
                }
            }

            function prepareForSubmit() {
                mHttp.cancel(formSubmitRequest);
                if (savedConfirmTimer) {
                    $timeout.cancel(savedConfirmTimer);
                    savedConfirmTimer = null;
                }
                if (progressState === 'saved') {
                    setProgressState('in-progress');
                }
            }

        }
    };

}]);