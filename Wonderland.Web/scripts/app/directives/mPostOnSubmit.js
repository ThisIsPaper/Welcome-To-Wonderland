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
                onSuccessEvent = attrs.mOnSuccessEvent,
                onErrorEvent = attrs.mOnErrorEvent,
                useLegacy = (attrs.mUseLegacy === true || attrs.mUseLegacy === 'true') || false;

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
                if (!useLegacy && window.File && window.FileReader) {

                    $timeout(function () {
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
console.log('SAVE DATA', originalData, sendData);

                        formSubmitRequest = mHttp.post(attrs.action + '?t=' + new Date().getTime(), {
                            formData: sendData,
                            dataType: 'json'
                        });

                        formSubmitRequest.then(function (response) {
                            handleResponse(response, originalData);
                        }, function () {
                            emitError();
                            setProgressState('ready');
                        });
                    });


                    /**
                     * LEGACY (NON-HTML5) AJAX FALLBACK
                     */
                } else {

                    var target = 'form_target_iframe_' + uniqueId();

                    var $targetIframe = $('<iframe name="' + target + '"></iframe>', { id: target, frameborder: '0' }).insertAfter(element).css({ width: 0, height: 0 }).load(function () {
                        $timeout(function () {
                            var response = $targetIframe.contents().find("body").html().replace(/<\/?[^>]+>/gi, ''),
                                parsedJson = null;
                            try {
                                parsedJson = JSON.parse(response);
                            } catch (er) {
                            }

console.log('response', response, parsedJson);
                            handleResponse(parsedJson, originalData);
                        });
                    });

                    var $postForm = $('<form></form>', { target: target, method: 'post', enctype: 'multipart/form-data' }).insertAfter(element);
                    $(element).contents().appendTo($postForm);

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

                if (response && response.Success === true) {
                    setProgressState('saved');
                    savedConfirmTimer = $timeout(function () {
                        setProgressState('ready');
                        savedConfirmTimer = null;
                    }, SAVED_CONFIRMATION_PERIOD);

                    if (onSuccessEvent) {
                        $rootScope.$emit(onSuccessEvent, response, angular.copy(originalData));
                    }
                } else {
                    emitError();
                    setProgressState('ready');
                }

            }

            function emitError() {
                if (onErrorEvent) {
                    $rootScope.$emit(onErrorEvent);
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