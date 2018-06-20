(function userViewModel(viewModel) {
    ko.observable(viewModel);
    var self = viewModel;

    self.EnterAdmin = function (data) {
        var userDTO = JSON.stringify(ko.mapping.toJS(data));
        $.ajax({
            url: baseUrl + 'api/User/CheckAuthenticUser',
            data: userDTO,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {

                alert("Record inserted sucessfully.");
            },
            error: function (err) {

            }
        });

    };

})(userViewModel);