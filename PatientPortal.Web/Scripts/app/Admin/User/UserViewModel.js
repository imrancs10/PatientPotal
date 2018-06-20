//************************************** User View Model : knockout view model **************************************//
/*jslint browser: true, bitwise: true, devel: true, this : true */
/*global $,baseUrl,define,languageCode,window,location,ko,document,self,FormData*/
define(['usermodel', 'common'], function (UserModel, Common) {
    'use strict';
    function UserViewModel() {
        var self = this, common;
        self.model = new UserModel();
        common = new Common();

        // Functionly of login
        self.Login = function (data) {
         
            self.model.ConfirmPassword(self.model.Password());
            self.model.ValidationEnabledForget(false);
            self.model.ValidationEnabledConfirmPassword(false);
            self.model.ValidationEnabledSignIn(true);
            if (self.model.isValid()) {
                common.ShowLoader();
                var userDTO = JSON.stringify(ko.mapping.toJS(data.model));
                $.ajax({
                    url: common.APIUrls.PostUser,
                    data: userDTO,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        self.LoginSuccess(data);
                    },
                    error: function () {
                        common.AlertBox(common.Messages[languageCode].Error);
                    }
                });
            } else {
                self.model.Errors.showAllMessages();
                return;
            }
        };

        // Insert success
        self.LoginSuccess = function (data) {
            if (data.ErrorCode > 0) {
                common.AlertBox(common.Messages[languageCode].Error);
                return false;
            }
     
            if (data.Id > 0) {
                if (data.RoleDTO.Type === common.RoleType.Admin) {
                    window.location = common.RedirctPage.Product;
                }
                if (data.RoleDTO.Type === common.RoleType.SurveyUser) {
                    window.location = common.RedirctPage.PastSurvey;
                }
            } else {
                common.AlertBox(common.Messages[languageCode].InvalidUser);
            }
        };

        // Reset password event
        self.ResetPassword = function () {
            self.model.ValidationEnabledSignIn(false);
            self.model.ValidationEnabledForget(false);
            self.model.ValidationEnabledConfirmPassword(true);

            if (self.model.isValid()) {
                var userName = $("#lblUserName").text();
                self.model.UserName(userName);
                self.model.Password(self.model.ResetPassword());
                var userDTO = JSON.stringify(ko.mapping.toJS(self.model));
                $.ajax({
                    url: common.APIUrls.PutUser,
                    data: userDTO,
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function () {
                        $("#divSuccessMessage").removeClass("hidden");
                        $("#spnSuccessMessage").text(common.Messages[languageCode].PasswordReset);
                        self.model.ValidationEnabledConfirmPassword(false);
                        self.model.ClearData();
                    },
                    error: function () {
                        common.AlertBox(common.Messages[languageCode].Error);
                    }
                });
            } else {
                self.model.Errors.showAllMessages();
                return;
            }
        };

        // Display login div and hide forget password div
        self.ShowHideLoginArea = function () {
            self.model.ValidationEnabledSignIn(false);
            self.model.ValidationEnabledForget(false);
            self.model.ValidationEnabledConfirmPassword(false);
            $("#divSignin").show();
            $('#divForgetPassword').hide();
        };

        // Display forget password div and hide login div
        self.ForgetPassword = function () {
            self.model.ValidationEnabledSignIn(false);
            self.model.ValidationEnabledForget(false);
            self.model.ValidationEnabledConfirmPassword(false);
            $("#divSignin").hide();
            $('#divForgetPassword').fadeIn();
            $('#divForgetPassword').show();

        };

        // Back to login event from forget password screen
        self.BackToLogin = function () {
            $("#divSignin").show();
            $('#divSignin').fadeIn();
            $('#divForgetPassword').hide();
        };

        // Send forget password to user from forget password screen
        self.SendForgetPassword = function (data) {
            self.model.ValidationEnabledConfirmPassword(false);
            self.model.ValidationEnabledSignIn(false);
            self.model.ValidationEnabledForget(true);
            if (self.model.isValid()) {
                common.ShowLoader();
                data.model.IsForgetPassword = 1;
                data.model.UserName = data.model.ForgetPasswordUserName();
                var userDTO = JSON.stringify(ko.mapping.toJS(data.model));
                $.ajax({
                    url: common.APIUrls.PostForgetPassword,
                    data: userDTO,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.Id > 0) {
                            common.AlertBox(common.Messages[languageCode].PasswordMailSent);
                        } else {
                            common.AlertBox(common.Messages[languageCode].InvalidEmailId);
                        }
                    },
                    error: function () {
                        common.AlertBox(common.Messages[languageCode].Error);
                    }
                });
            } else {
                self.model.Errors.showAllMessages();
                return;
            }
        };

        //set placeholder in login page for IE9
        self.SetPlaceHolder = function () {
            common.setPlaceHolderForIE9();
        };
    }

    var viewModel = new UserViewModel();
    viewModel.ShowHideLoginArea();
    ko.applyBindings(viewModel, document.getElementById('divLogin'));
    viewModel.SetPlaceHolder();
});