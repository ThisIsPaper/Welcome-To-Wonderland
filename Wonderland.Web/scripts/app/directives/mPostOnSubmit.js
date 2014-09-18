wonderlandApp.directive('mPostOnSubmit', ['safeApply', 'mHttp', '$parse', '$rootScope', '$timeout', function (safeApply, mHttp, $parse, $rootScope, $timeout) {


    var SAVED_CONFIRMATION_PERIOD = 2000; // milliseconds


    return {
        scope: true,
        link: function(scope, element, attrs) {

            var progressState,
                progressVar = attrs.mProgressVar && $parse(attrs.mProgressVar),
                dataVar = $parse(attrs.mPostOnSubmit),
                formSubmitRequest = null,
                savedConfirmTimer = null,
                serialized = attrs.hasOwnProperty('mSerialized'),
                onSuccessEvent = attrs.mOnSuccessEvent;

            setProgressState('ready');

            element.on('submit', function() {
                safeApply(scope, function() {
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
                        sendData['ufprt'] = ufprtElement[0].value;
                    }

                    // check for ufprt as hidden value
                    var rvtElement = element.find("[name='__RequestVerificationToken']");
                    if (rvtElement && rvtElement.length) {
                        sendData['__RequestVerificationToken'] = rvtElement[0].value;
                    }


                    console.log('mPostOnSubmit::data', sendData);

                    formSubmitRequest = mHttp.post(attrs.action, {
                        formData: sendData,
                        dataType: 'json'
                    });


                    formSubmitRequest.then(function(response) {

                        if (response && response.data) {
                            dataVar.assign(scope.$parent, response.data);
                        }

                        if (response && response.success === false) {
                            setProgressState('ready');
                        } else {
                            setProgressState('saved');
                            savedConfirmTimer = $timeout(function() {
                                setProgressState('ready');
                                savedConfirmTimer = null;
                            }, SAVED_CONFIRMATION_PERIOD);

                            if (onSuccessEvent) {
                                $rootScope.$emit(onSuccessEvent, response, angular.copy(originalData));
                            }
                        }
                    }, function() {
                        setProgressState('ready');
                    });

                });
                return false;
            });

            return;

            function setProgressState(state) {
                console.log('setProgressState', state);
                progressState = state;
                if (progressVar) {
                    progressVar.assign(scope.$parent, state);
                }
            }

            function prepareForSubmit() {
                mHttp.cancel(formSubmitRequest);
                if(savedConfirmTimer) {
                    $timeout.cancel(savedConfirmTimer);
                    savedConfirmTimer = null;
                }
                if(progressState === 'saved') {
                    setProgressState('in-progress');
                }
            }

        }
    };

}]);