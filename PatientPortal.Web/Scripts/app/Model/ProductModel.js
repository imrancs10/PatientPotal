//************************************** Product Model : knockout view model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko, languageCode*/
define(['common'], function (Common) {
    'use strict';
    function ProductModel() {
        var self = this;
        var common = new Common();
        ko.validation.rules['url'] = {
            validator: function (val, required) {
                if (!val) {
                    return !required
                }
                val = val.replace(/^\s+|\s+$/, '');
                return val.match(/^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.‌​\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[‌​6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1‌​,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00‌​a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u‌​00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i);
            },
        };

        ko.validation.registerExtenders();
        //flag to enable/disable validation
        self.ValidationEnabled = ko.observable(true);

        self.Id = ko.observable();
        self.Name = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled } });

        self.Description = ko.observable();
        self.ProductURL = ko.observable().extend({ url: { message: common.Messages[languageCode].InvalidURL, onlyIf: self.ValidationEnabled } });
        self.MainId = ko.observable();
        self.MainName = ko.observable();
        self.MainDescription = ko.observable();
        self.MainProductURL = ko.observable();
        self.IsMapped = ko.observable();
        self.LanguageId = ko.observable().extend({ required: { message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled } });
        self.ProductImagesDTOList = ko.observable([]);

        self.SetData = function (model) {
            self.Id(model.Id);
            self.Name(model.Name);
            self.Description(model.Description);
            self.MainId(model.Id);
            self.MainName(model.Name);
            self.MainDescription(model.Description);
            self.MainProductURL(model.ProductURL);
            self.IsMapped(model.IsMapped);
            self.ProductURL(model.ProductURL);
            self.LanguageId(model.LanguageId);
            self.ProductImagesDTOList(model.ProductImagesDTOList);
        };

        self.ClearData = function () {
            self.Id(0);
            self.Name('');
            self.Description('');
            self.ProductURL('');
            self.LanguageId('');
        };

        self.Errors = ko.validation.group(self);
        self.isValid = ko.computed(function () {
            return self.Errors().length === 0;
        });
    }
    return ProductModel;
});

