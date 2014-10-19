wonderlandApp.directive('mPostOnSubmit', ['safeApply', 'mHttp', 'uniqueId', '$q', '$parse', '$rootScope', '$timeout', function (safeApply, mHttp, uniqueId, $q, $parse, $rootScope, $timeout) {


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
                var $targetIframe = null,
                    $postForm = null,
                    postDeferred;

                function postTo() {
                    postDeferred = $q.defer();

                    if (!$postForm) {
                        var target = 'form_target_iframe_' + uniqueId();

                        $targetIframe = $('<iframe/>', { name: target, id: target, frameborder: '0' }).insertAfter(element).css({ width: 0, height: 0 }).load(function () {
                            var iframe = this;

                            scope.$apply(function () {
                                var response = iframe.contentWindow.document.body.innerHTML;
                                postDeferred.resolve(JSON.parse(response));
                            });
                        });

                        // TODO: who knows if this will work in IE!?
                        $postForm = $('<form></form>', { target: target, method: 'post', enctype: 'multipart/form-data' }).insertAfter(element);
//                        $postForm.append('<input type="hidden" name="csrfmiddlewaretoken" value="' + mHttp.getCsrfCookie() + '">');
                        element.appendTo($postForm);
                    }

                    $postForm.attr('action', attrs.action);
                    $postForm.submit();

                    return postDeferred.promise;
                }

                postTo();

                return false;
            });


            element.on('esubmit', function () {

                safeApply(scope, function () {
                    if (serialized && mHttp.isInProgress(formSubmitRequest)) {
                        return;
                    }

                    prepareForSubmit();
                    setProgressState('in-progress');

                    var originalData = dataVar(scope) || {},
                        sendData = angular.copy(originalData);

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
                    formSubmitRequest = mHttp.post(attrs.action + '?t='+date.getTime(), {
                        formData: sendData,
                        dataType: 'json'
                    });

                    formSubmitRequest.then(function (response) {

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
                    }, function () {
                        setProgressState('ready');
                    });

                });
                return false;
            });

            return;

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