Better Unobtrusive Ajax (not only for ASP.NET MVC)
==================================================
version 0.2.0 (2013-11-06)  
(c) 2012 Dusan Hlavaty, WorkInField s.r.o.  
freely distributable under The MIT License (MIT)  
https://github.com/dhlavaty/winf.unobtrusiveAjax.js


Purpose:
--------

Purpose of this library is to be 100% compatible with 'Microsofts Unobtrusive Ajax support library for jQuery'
found in ASP.NET MVC3 and MVC4 but without bugs, with new features, clean and well documented code and compatible
with latest jQuery. Library is also fully usable without ASP.NET MVC.

Usage:
------

If you use ASP.NET MVC, just replace original `jquery.unobtrusive-ajax.js` or `jquery.unobtrusive-ajax.min.js`
with our `winf.unobtrusive-ajax.js` and you are all set. All other requirements are the same as with original script
from Microsoft, see tutorial at [http://goo.gl/3DTJY](http://goo.gl/3DTJY)

Changelog:
----------

* 2013-11-06 ver 0.2.0
   - ADD: After DOM insertion, we focus on inserted element with autofocus attribute
* 2013-07-17 ver 0.1.9
   - ADD: new `data-ajax-disable-onclick="true"` introduced
* 2012-11-23 ver 0.1.8
   - ADD: new `data-ajax="false"` support
* 2012-08-28 ver 0.1.7
   - ADD: new `data-ajax-mode="beforeelement"` and `data-ajax-mode="afterelement"` introduced
* 2012-08-03 ver 0.1.6
   - ADD: new `data-ajax-update-closest=""` attribute introduced
   - ADD: automatically ajax-submit 'select' element, if it has `data-ajax="true"` attribute
* 2012-06-06 ver 0.1.5
   - FIX: unobtrusive Ajax always expects 'html' code to be returned from server, so we set `dataType: "html"`. Now MVC controller can return `null` and this library behaves correctly.
* 2012-06-05 ver 0.1.4
   - FIX: error handler was commented out by mistake
   - CHANGE: some minor documentation changes
* 2012-05-31 ver 0.1.3
   - ADD: new `data-ajax-mode="realreplace"` introduced
* 2012-05-29 ver 0.1.2
   - ADD: some improvements inspired by Brant Burnett (http://btburnett.com/2011/01/mvc-3-unobtrusive-ajax-improvements.html)
* 2012-05-25 ver 0.1.1
   - FIX: ajaxError event handler was not fired correctly
   - ADD: you can use `<form data-ajax='true'><input type='image'>` or `<form><input type='image' data-ajax='true'>` also
* 2012-05-25 ver 0.1
   - All code comments are in English, no code changes
* 2012-05-24 ver 0.1
   - Initial version


Docs:
-----

All ajax data attributes are 100% compatible with Microsoft ASP.NET MVC 3 - [http://www.asp.net/mvc/mvc3](http://www.asp.net/mvc/mvc3)


### data-ajax="true"

attribute activates Unobtrusive Ajax library. It can be used on `form`, `a`, `input[type="image"]` and/or `select` element.

    Example 1:
    <form data-ajax="true" ... ><!-- Performs AJAX on entire form -->
    ...</form>

    Example 2:
    <a data-ajax="true" ... >Link text - performs AJAX only on this anchor</a>

    Example 3:
    <input type="image" data-ajax="true" ... />

    Example 4:
    <select name="selectName" data-ajax="true" data-ajax-url="http://example-url/" ... >
      <option value="1">Option 1</option>
    </select>


### data-ajax="false"

attribute explicitly deactivates Unobtrusive Ajax library. It can be used on `button`, `a`, `input` and/or `select` element.

    Example:
    <form data-ajax="true" ... ><!-- Performs AJAX on entire form except where data-ajax=='false' -->
      <button data-ajax="false">THIS WILL NOT USE AJAX</button>
      <button>THIS WILL USE AJAX</button>
    ...</form>



### data-ajax-loading

attribute which contains element selector to element showing "LOADING IN PROGRESS" text


### data-ajax-loading-duration

how long should show/hide animation of "LOADING IN PROGRESS" text take. Animation duration is given in milliseconds. Default is 0.

    Example:
    <div id="loading">AJAX IS IN PROGRESS</div>
    <a data-ajax="true" data-ajax-loading="#loading" data-ajax-loading-duration="1000" ... />


### data-ajax-confirm

attribute contains a confirmation question. If user hit CANCEL button on a confirmation dialog, AJAX call will NOT be fired.

    Example:
    <a data-ajax-confirm="Are you sure to delete entry ?" ... />


### data-ajax-disable-onclick="true"

Disable element after user clicks on it, to prevent multiple postbacks to server. It can be used on `button`, `input` or `a` element.

    Example:
      <button data-ajax="true" data-ajax-disable-onclick="true">Disables after click</button>


### data-ajax-method

The type of request to make (`POST` or `GET`). *jQuery documentation note: Other HTTP request methods, such as `PUT` and `DELETE`, can also be used here, but they are not supported by all browsers.*
This library uses `X-HTTP-Method-Override` HTTP header command to support `PUT` and `DELETE` in these browsers too.

    Example:
    <a data-ajax-method="post" ... />


### data-ajax-mode

Mode of displaying ajax response sent from server. Can be `BEFORE`, `AFTER`, `REPLACE` or `REALREPLACE`

+ **"BEFORE"**  - response data is prepended with [jQuery.prepend()](http://api.jquery.com/prepend/) as a first child of target (`data-ajax-update`) element. Note: Target element is NOT emptied before inserting.
+ **"AFTER"** - response data is appended with [jQuery.append()](http://api.jquery.com/append/) as a last child of target (`data-ajax-update`) element. Note: Target element is NOT emptied before inserting.
+ **"REPLACE"** - response data replaces with [jQuery.empty()](http://api.jquery.com/empty/).[html()](http://api.jquery.com/html/) inner content of target (`data-ajax-update`) element and NOT an element itseft. So every attribute (e.g. `<div id="someId">`) of target element will stay intact. Note: Target element IS emptied before inserting.
+ **"REALREPLACE"** - will replace (`data-ajax-update`) element itself with all its content. Uses [jQuery.after()](http://api.jquery.com/after/) and [jQuery.remove()](http://api.jquery.com/remove/) combo.
+ **"BEFOREELEMENT"** - response data is inserted before (`data-ajax-update`) element. Uses [jQuery.before()](http://api.jquery.com/before/)
+ **"AFTERELEMENT"** - response data is inserted after (`data-ajax-update`) element. Uses [jQuery.after()](http://api.jquery.com/after/)


    Example:
    < a data-ajax-mode="replace" ... >Some link< /a>


### data-ajax-update

attribute which contains element selector to element in which ajax response will be shown.

    Example:
    <div id="ajaxTarget">...this will be replaced with ajax response...</div>
    <a data-ajax-update="#ajaxTarget" data-ajax-mode="replace" ... />


### data-ajax-update-closest

attribute which contains closest element selector from current element in which ajax response will be shown.
It uses .closest(selector) jQuery method, so look for documentation at http://api.jquery.com/closest/

    Example:
    <div>...this will be replaced with ajax response...
    <a data-ajax-update-closest="div" data-ajax-mode="replace" ... /></div>


### data-ajax-url

URL of ajax request to perform. If this attribute is not defined in <a /> element, the `href` attribute will be used.

    Example:
    <a data-ajax-url="http://www.acme.com/ajax" href="http://www.acme.com/nonajax" ... />


### data-ajax-begin

event callback, which is called before ajax request itself is fired.
Returning `false` from this callback will cancel the request.
If attribute contains an function name, this function is called with signature:

`function beforeAjaxRequest(jqXHR)`  
Returns: boolean  
Params:  
**jqXHR** - is an jQuery XHR object (see http://api.jquery.com/Types/#jqXHR
            and http://api.jquery.com/jQuery.ajax/#jqXHR )

    Example 1:
    <script> function BeforeSendFunctionName(jqXHR) { return true; } </script>
    <a data-ajax-begin="BeforeSendFunctionName" ... />

    Example 2:
    <a data-ajax-begin="alert('We stop AJAX at all costs.');return false;" ... />


### data-ajax-complete

event callback, which is called when ajax request itself was completed.
A function will be called after 'success' and 'error' callbacks are executed.
If attribute contains an function name, this function is called with signature:

`function completeAjaxRequest(jqXHR, textStatus)`  
Returns: undefined  
Params:  
**jqXHR** - is an jQuery XHR object (see http://api.jquery.com/Types/#jqXHR
            and http://api.jquery.com/jQuery.ajax/#jqXHR )

**textStatus** - can be `success`, `notmodified`, `error`, `timeout`, `abort` or `parsererror`
                 (see paragraph `complete(jqXHR, textStatus)` at http://api.jquery.com/jQuery.ajax/)

    Example 1:
    <script> function CompleteSendFunctionName(jqXHR, textStatus) { alert('All done'); } </script>
    <a data-ajax-complete="CompleteSendFunctionName" ... />

    Example 2:
    <a data-ajax-complete="alert('All done');" ... />


### data-ajax-success
event callback, which is called if ajax request succeeds.
If attribute contains an function name, this function is called with signature:

`function successAjaxRequest(data, textStatus, jqXHR)`  
Returns: undefined  
Params:  
**data** - the data returned from the server

**textStatus** - a string describing the status
                 (see paragraph `success(data, textStatus, jqXHR)` at http://api.jquery.com/jQuery.ajax/)

**jqXHR** - is an jQuery XHR object (see http://api.jquery.com/Types/#jqXHR
            and http://api.jquery.com/jQuery.ajax/#jqXHR )

    Example 1:
    <script> function SuccessSendFunctionName(data, textStatus, jqXHR) { alert('Success'); } </script>
    <a data-ajax-success="SuccessSendFunctionName" ... />

    Example 2:
    <a data-ajax-success="alert('Success');" ... />


### data-ajax-error

event callback, which is called if ajax request fails.
Note: This handler is not called for cross-domain script and JSONP requests.
If attribute contains an function name, this function is called with signature:

`function errorAjaxRequest(jqXHR, textStatus, errorThrown)`  
Returns: undefined  
Params:  
**jqXHR** - is an jQuery XHR object (see http://api.jquery.com/Types/#jqXHR
            and http://api.jquery.com/jQuery.ajax/#jqXHR )

**textStatus** - `timeout`, `error`, `abort`, and `parsererror` or `null`
                 (see paragraph `error(jqXHR, textStatus, errorThrown)` at http://api.jquery.com/jQuery.ajax/)

**errorThrown** - when an HTTP error occurs, errorThrown receives the textual portion
                  of the HTTP status, such as `Not Found` or `Internal Server Error.`

    Example 1:
    <script> function ErrorSendFunctionName(jqXHR, textStatus, errorThrown) { alert('Sorry error !'); } </script>
    <a data-ajax-error="ErrorSendFunctionName" ... />

    Example 2:
    <a data-ajax-error="alert('Sorry error !');" ... />


Better Unobtrusive Form Validation
----------------------------------

This library plays nice with original `jquery.validate.unobtrusive.js` library from MVC3. It is even better.
You can now add any forms after AJAX request, and all validations will still work.


Global AJAX Events
------------------

Using this library rather original from MS, you can attach a global AJAX event handlers (e.g. $(document).ajaxSuccess() ) that specifically handle only
the MVC related AJAX events. In ajaxOptions you have a variable "isMvcAjax: true" and "mvcTagetElement" is your target element as an jQuery object.

    Example:
    jQuery(document).ajaxSuccess(function(event, XMLHttpRequest, ajaxOptions) 
                                 {
                                     if (ajaxOptions.isMvcAjax === true) { 
                                         ajaxOptions.mvcTagetElement.prepend('<p>Prepend something to MVC target.</p>');
                                     }
                                 });


The end.