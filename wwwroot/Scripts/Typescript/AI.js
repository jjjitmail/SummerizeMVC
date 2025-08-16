///<reference path="../typings/jquery/jquery.d.ts" />
var TranscribeModel = /** @class */ (function () {
    function TranscribeModel() {
    }
    return TranscribeModel;
}());
var AI = /** @class */ (function () {
    function AI() {
    }
    AI.prototype.InvokeAjax = function (object, url, CallBack) {
        var postObject = null;
        if (object != null) {
            postObject = JSON.stringify(object);
        }
        $.ajax({
            url: url,
            data: postObject,
            contentType: 'application/json',
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (CallBack != null) {
                    if (object != null) {
                        CallBack(data);
                    }
                }
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                    //alert('Not connect.\n Verify Network.');
                }
                else if (jqXHR.status == 404) {
                    //alert('Requested page not found. [404]');
                }
                else if (jqXHR.status == 500) {
                    //alert('Internal Server Error [500].');
                }
                else if (exception === 'parsererror') {
                    //alert('Requested JSON parse failed.');
                }
                else if (exception === 'timeout') {
                    //alert('Time out error.');
                }
                else if (exception === 'abort') {
                    //alert('Ajax request aborted.');
                }
                else {
                    //alert('Uncaught Error.\n' + jqXHR.responseText);
                }
            }
            //error: function () { alert('error'); }
        });
    };
    return AI;
}());
var _ai = new AI();
//# sourceMappingURL=AI.js.map