//************************************** Admin View Model : knockout view model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko, languageCode*/
define(['rolemodel', 'common'], function (RoleModel, Common) {
    'use strict';
    function UserModel() {
        var self = this;
        self.roleModel = new RoleModel();
        var common = new Common();

        ko.validation.rules['compare'] = {
            validator: function (val, password) {
                var passwordConfirm = ko.isObservable(self.ResetPassword) ? self.ResetPassword() : self.ResetPassword;
                return val === passwordConfirm;
            },
        };

        ko.validation.rules['passwordLength'] = {
            validator: function (val, number) {
                if (val && val !== "")
                    return val.length >= 8;
            }
        };

        ko.validation.registerExtenders();

        //flag to enable/disable validation
        self.ValidationEnabledSignIn = ko.observable(true);

        //flag to enable/disable validation
        self.ValidationEnabledForget = ko.observable(true);

        //flag to enable/disable validation
        self.ValidationEnabledConfirmPassword = ko.observable(true);

        self.UserName = ko.observable().extend({ required: { message: common.Messages[languageCode].UserName, onlyIf: self.ValidationEnabledSignIn }, email: { message: common.Messages[languageCode].RequiredEmailId, onlyIf: self.ValidationEnabledSignIn } });

        self.Password = ko.observable().extend({ required: { message: common.Messages[languageCode].Password, onlyIf: self.ValidationEnabledSignIn }, passwordLength: { message: common.Messages[languageCode].PasswordLength, onlyIf: self.ValidationEnabledSignIn } });

        self.ResetPassword = ko.observable().extend({ required: { message: common.Messages[languageCode].Password, onlyIf: self.ValidationEnabledConfirmPassword }, passwordLength: { message: common.Messages[languageCode].PasswordLength, onlyIf: self.ValidationEnabledConfirmPassword } });

        self.ConfirmPassword = ko.observable().extend({ required: { message: common.Messages[languageCode].Password, onlyIf: self.ValidationEnabledConfirmPassword }, passwordLength: { message: common.Messages[languageCode].PasswordLength, onlyIf: self.ValidationEnabledConfirmPassword }, compare: { message: common.Messages[languageCode].PasswordEqual, onlyIf: self.ValidationEnabledConfirmPassword } });

        self.ForgetPasswordUserName = ko.observable().extend({ required: { message: common.Messages[languageCode].UserName, onlyIf: self.ValidationEnabledForget }, email: { message: common.Messages[languageCode].RequiredEmailId, onlyIf: self.ValidationEnabledForget } });
        self.IsForgetPassword = ko.observable();

        self.SetData = function (model) {
            self.UserName(model.UserName);
            self.Password(model.Password);
            self.ForgetPasswordUserName(model.UserName);
        };

        self.ClearData = function () {
            self.UserName("");
            self.Password("");
            self.ResetPassword("");
            self.ConfirmPassword("");
            self.ForgetPasswordUserName("");
        };
        self.Errors = ko.validation.group(self);
        self.isValid = ko.computed(function () {
            return self.Errors().length === 0;
        });

    }

    return UserModel;
});

