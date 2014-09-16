wonderlandApp.directive('postOnSubmit', ['safeApply', '$http', '$parse', function (safeApply, $http, $parse) {


    return {
        scope: true,
        link: function(scope, element, attrs) {

            var progressState,
                progressVar = attrs.mProgressVar && $parse(attrs.mProgressVar),
                dataVar = $parse(attrs.postOnSubmit),
                formSubmitRequest = null,
                savedConfirmTimer = null,
                serialized = attrs.hasOwnProperty('wSerialized'),
                onSuccessfulPost = attrs.mOnSuccessfulPost && $parse(attrs.mOnSuccessfulPost);

                var hiddenFields = {
                    'ufprt': element.find("[name='ufprt']")[0].value
//                    '__RequestVerificationToken': element.find("[name='__RequestVerificationToken']")[0].value
                };


//            setProgressState('ready');

            element.on('submit', function() {
                safeApply(scope, function() {
//                    if (serialized && mHttp.isInProgress(formSubmitRequest)) {
//                        return;
//                    }
//                    prepareForSubmit();
//                    setProgressState('in-progress');
//
                    var data = dataVar(scope) || {};
//                    if (data.MAX_NUM_FORMS) {  // formset
//                        data = formset.fromJsonForm(data);
//                    }
                    console.log('submit1', data);

                    angular.extend(data, hiddenFields);

                    console.log('submit2', data);

//

                    formSubmitRequest = $http.post(attrs.action, {
                        data: data,
                        dataType: 'json',
                        headers: {'Content-Type': 'application/x-www-form-urlencoded'}
                    });

                    formSubmitRequest.then(function (response) {

                        console.log('response', response);

                    });



//                    formSubmitRequest = mHttp.post(attrs.action, {
//                        data: data,
//                        dataType: 'json'
//                    });
//                    formSubmitRequest.then(function(response) {
//                        if (response && response.data) {
//                            dataVar.assign(scope.$parent, response.data);
//                        }
//                        if (response && response.success === false) {
//                            setProgressState('ready');
//                        } else {
//                            setProgressState('saved');
//                            savedConfirmTimer = $timeout(function() {
//                                setProgressState('ready');
//                                savedConfirmTimer = null;
//                            }, SAVED_CONFIRMATION_PERIOD);
//                            if (onSuccessfulPost) {
//                                onSuccessfulPost(scope, {'response': response});
//                            }
//                        }
//                    }, function() {
//                        setProgressState('ready');
//                    });
                });
                return false;
            });

            return;

//            function setProgressState(state) {
//                progressState = state;
//                if (progressVar) {
//                    progressVar.assign(scope.$parent, state);
//                }
//            }
//
            function prepareForSubmit() {
                console.log('prepareForSubmit');
//                mHttp.cancel(formSubmitRequest);
//                if(savedConfirmTimer) {
//                    $timeout.cancel(savedConfirmTimer);
//                    savedConfirmTimer = null;
//                }
//                if(progressState === 'saved') {
//                    setProgressState('in-progress');
//                }
            }

        }
    };

}]);