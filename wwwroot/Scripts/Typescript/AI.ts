///<reference path="../typings/jquery/jquery.d.ts" />
class TranscribeModel {
    modelId: string;
    prompt: string;
}

class AI {
    constructor() {

    }
    InvokeAjax(object, url, CallBack) {
        var postObject = null;
        if (object != null) {
            postObject = JSON.stringify(object);
        }
        $.ajax(
            {
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
                    } else if (jqXHR.status == 404) {
                        //alert('Requested page not found. [404]');
                    } else if (jqXHR.status == 500) {
                        //alert('Internal Server Error [500].');
                    } else if (exception === 'parsererror') {
                        //alert('Requested JSON parse failed.');
                    } else if (exception === 'timeout') {
                        //alert('Time out error.');
                    } else if (exception === 'abort') {
                        //alert('Ajax request aborted.');
                    } else {
                        //alert('Uncaught Error.\n' + jqXHR.responseText);
                    }
                }
                //error: function () { alert('error'); }
            });
    }
}
var _ai = new AI();