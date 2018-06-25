var utility = {};

utility.ajax = {};
utility.table = {};
utility.alert = {};
utility.ajax.errorCall = function (x,y,z) {

}
utility.ajax.options = {
    url: '',
    method: "POST",
    contentType: 'application/json',
    error: utility.ajax.errorCall(),
    success:''
};

utility.ajax.helper = function (url, success, error) {
    if (typeof success === 'function') {
        utility.ajax.options.success = success;
    }
    else
        throw new Error('success should be a function');

    if (typeof error !== undefined && typeof error === 'function')
    {
        utility.ajax.options.error = error;
    }

    utility.ajax.options.url = url;    

    $.ajax(utility.ajax.options);
}
utility.ajax.helperWithData = function (url,data, success, error) {
    if (typeof success === 'function') {
        utility.ajax.options.success = success;
    }
    else
        throw new Error('success should be a function');

    if (typeof error !== undefined && typeof error === 'function') {
        utility.ajax.options.error = error;
    }

    utility.ajax.options.url = url;
    utility.ajax.options.data = JSON.stringify(data);
    utility.ajax.options.dataType = 'json';
    $.ajax(utility.ajax.options);
}

utility.alert.setAlert = function () {
   
}