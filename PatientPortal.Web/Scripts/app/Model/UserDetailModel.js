//************************************** UserDetail Model : knockout view model **************************************//
define(['common'], function (Common) {
    function UserDetailModel() {
        var self = this;
        var common = new Common();

        ko.validation.rules['lengthCheckPassword'] = {
            validator: function (val, number) {
                if (val && val != "")
                    return val.length >= 8;
            }
        };

        ko.validation.rules['lengthCheckZip'] = {
            validator: function (val, number) {
                if (val != undefined)
                    return val.length <= 10;
            }
        };

        ko.validation.rules['lengthCheckPhone'] = {
            validator: function (val, number) {
                if (val != undefined)
                    return val.length <= 14;
            }
        };

        ko.validation.rules['number'] = {
            validator: function (val, number) {
                if (val != undefined)
                    return !isNaN(val.replace('-', ''));
            }
        };

        ko.validation.rules['comparePassword'] = {
            validator: function (val, password) {
                var passwordField = ko.isObservable(self.Password) ? self.Password() : self.Password;
                return val == passwordField;
            }
        };
        ko.validation.registerExtenders();
   
        //flag to enable/disable validation
        self.ValidationEnabled = ko.observable(true);
        self.ValidationEnabledLogin = ko.observable(true);

        self.Id = ko.observable();

        self.UserId = ko.observable();

        self.BusinessName = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled } });

        self.FirstName = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled } });

        self.LastName = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled } });

        self.StateId = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled } });

        self.ZipCode = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled }, lengthCheckZip: { message: common.Messages[languageCode].ValidZipCode, onlyIf: self.ValidationEnabled }, number: { message: common.Messages[languageCode].NumberMust, onlyIf: self.ValidationEnabled } });

        self.PhoneNumber = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled }, lengthCheckPhone: { message: common.Messages[languageCode].PhoneNumberLength, onlyIf: self.ValidationEnabled } });

        self.EmailId = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled }, email: { message: common.Messages[languageCode].InvalidEmailId, onlyIf: self.ValidationEnabled } });

        self.Password = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabledLogin }, lengthCheckPassword: { message: common.Messages[languageCode].PasswordLength, onlyIf: self.ValidationEnabledLogin }, });

        self.ConfirmPassword = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabledLogin }, comparePassword: { message: common.Messages[languageCode].ConfirmPasswordEqual, onlyIf: self.ValidationEnabledLogin } });

        self.IsReqForPromotions = ko.observable();

        self.IsAdvisorToContact = ko.observable();

        self.IsPDFDownload = ko.observable();
        self.IsMailSend = ko.observable();
        self.TerritoryEmails = ko.observableArray();
        self.StateName = ko.observable();
        self.PDFFileName = ko.observable();
        self.SetData = function (model) {
            self.Id(model.Id);
            self.UserId(model.UserId);
            self.BusinessName(model.BusinessName);
            self.FirstName(model.FirstName);
            self.LastName(model.LastName);
            self.StateId(model.StateId);
            self.ZipCode(model.ZipCode);
            self.PhoneNumber(model.PhoneNumber);
            self.EmailId(model.EmailId);
            self.Password(model.Password);
            self.ConfirmPassword(model.Password);
            self.StateName(model.StateName);
            self.PDFFileName(model.PDFFileName);
            self.IsReqForPromotions(model.IsReqForPromotions);
            self.IsAdvisorToContact(model.IsAdvisorToContact);
            self.TerritoryEmails(model.TerritoryEmails);
        };

        self.Errors = ko.validation.group(self);
        self.isValid = ko.computed(function () {
            return self.Errors().length == 0;
        });
    }

    return UserDetailModel;
});

