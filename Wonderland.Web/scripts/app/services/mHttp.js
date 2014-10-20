/*
 Simple wrapper for $.ajax - useful for mocking HTTP requests in testing
 */
wonderlandApp.factory('mHttpBackend', function () {
    return function (opts) {
        return $.ajax(opts);
    };
});


/*
 To be used instead of angular's $http. Reasons:
 - this has mHttp.cancel [this may be added to angular later]
 - this adds the mobile device id/access token/platform headers to each request
 - this handles the CSRF token [$http can be configured to do that if required]

 Usage:

 var promise = mHttp(opts);

 opts (and defaults):
 - type: 'GET'
 - async: true
 - headers: {}
 - data
 - dataType (N.B. Zepto doesn't do dataType inference)
 - timeout
 - url
 - onSuccess: called before the promise is resolved (used by mHttp.load)
 - requestProgress: called with progress updates 0-1

 Shortcut functions:

 mHttp.get(url, opts)
 mHttp.post(url, opts)
 mHttp.del(url, opts) // Note: not delete as that is a reserved word
 mHttp.load(element, scope, url, opts) // loads the response HTML into element and compiles it

 Other:

 mHttp.cancel(promise) // aborts the ajax request

 */
wonderlandApp.factory('mHttp', ['$compile', '$document', '$q', '$rootScope', '$window', 'mHttpBackend', '$timeout', 'uniqueId',
    function ($compile, $document, $q, $rootScope, $window, mHttpBackend, $timeout, uniqueId) {
        var cancelFns = {};

        var mHttp = function (opts) {
            var optsWithDefaults = angular.extend({
                                                      method: 'GET',
                                                      async: true,
                                                      headers: {},
                                                      invokeApply: true
                                                  }, opts);

            var xhrOpts = {
                url: optsWithDefaults.url,
                type: optsWithDefaults.method,
                timeout: optsWithDefaults.timeout,
                data: optsWithDefaults.data,
                dataType: optsWithDefaults.dataType,
                async: optsWithDefaults.async,
                headers: optsWithDefaults.headers,
                success: function () {
                    if (optsWithDefaults.onSuccess) {
                        optsWithDefaults.onSuccess.apply(mHttp, Array.prototype.slice.call(arguments));
                    }
                    onResponse(arguments, true);
                },
                error: function () {
                    onResponse(arguments, false);
                }
            };

            if (optsWithDefaults.requestProgress) {
                xhrOpts.xhr = function () {
                    var xhr = $.ajaxSettings.xhr();

                    var progress = 0,
                        downloadProgress = 0;

                    xhr.addEventListener('progress', function (event) {
                        if (event.lengthComputable) {
                            downloadProgress = event.loaded / event.total;
                            setProgress();
                        }
                    }, false);

                    xhr.addEventListener('readystatechange', setProgress);

                    function setProgress() {
                        progress = Math.min((xhr.readyState - 1) / 3 + downloadProgress / 3, 1);
                        optsWithDefaults.requestProgress(progress);

                        // safeApply required because xhr.abort() happens in sync and calls the error handler - abort could be called by something already in a digest
                        if (optsWithDefaults.invokeApply) {
                            $timeout(function () {
                                $rootScope.$apply();
                            });
                        }
                    }

                    return xhr;
                };
            }

            if (optsWithDefaults.formData && !optsWithDefaults.data) {
                var formData = new $window.FormData();

                angular.forEach(optsWithDefaults.formData, function (val, key) {
                    formData.append(key, val);
                });

                xhrOpts.data = formData;
                xhrOpts.processData = false;
                xhrOpts.contentType = false;
            }

            var deferred = $q.defer(),
                promise = deferred.promise;

            promise._requestId = uniqueId();
            cancelFns[promise._requestId] = function () {
                xhr.abort();
            };

            promise.then(cleanup, cleanup);

            var xhr = mHttpBackend(xhrOpts);

            return promise;


            function onResponse(args, success) {
                deferred[success ? 'resolve' : 'reject'].apply(deferred, Array.prototype.slice.call(args));

                // safeApply required because xhr.abort() happens in sync and calls the error handler - abort could be called by something already in a digest
                if (optsWithDefaults.invokeApply) {
                    $timeout(function () {
                        $rootScope.$apply();
                    });
                }
            }

            function cleanup() {
                delete cancelFns[promise._requestId];
            }
        };

        mHttp.isInProgress = function (promise) {
            return promise && promise._requestId in cancelFns;
        };

        mHttp.cancel = function (promise) {
            if (mHttp.isInProgress(promise)) {
                cancelFns[promise._requestId]();
            }
        };


        // Shortcut methods
        mHttp.get = function (url, opts) {
            return mHttp(angular.extend({
                                            method: 'GET',
                                            url: url
                                        }, opts));
        };

        mHttp.post = function (url, opts) {
            return mHttp(angular.extend({
                                            method: 'POST',
                                            url: url
                                        }, opts));
        };

        mHttp.del = function (url, opts) {
            return mHttp(angular.extend({
                                            method: 'DELETE',
                                            url: url
                                        }, opts));
        };

        mHttp.load = function (element, scope, url, opts) {
            return mHttp(angular.extend({
                                            method: 'GET',
                                            url: url,
                                            element: element,
                                            onSuccess: function (html) {
                                                element.html(html);
                                                $compile(element.contents())(scope);
                                            }
                                        }, opts));
        };


        return mHttp;

    }]);
