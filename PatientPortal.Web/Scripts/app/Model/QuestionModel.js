//************************************** Question Model : knockout view model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko, languageCode*/
define(['common'], function (Common) {
    'use strict';
    function QuestionModel() {
        var self = this;
        var common = new Common();

        self.QuestionTypeDTOList = ko.observableArray([]);
        self.LanguageDTOList = ko.observableArray([]);
        self.AnswerDTOList = ko.observableArray([]);

        //flag to enable/disable validation
        self.ValidationEnabled = ko.observable(true);

        self.Id = ko.observable();
        self.Title = ko.observable().extend({required:{message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled}});
        self.IsMandatary = ko.observable();
        self.IsActive = ko.observable();
        self.QuestionTypeId = ko.observable().extend({required: {message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled}});
        self.TextInputTypeId = ko.observable().extend({required: {message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled}});
        self.LanguageId = ko.observable().extend({required: {message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled}});
        self.IsMapped = ko.observable();

        self.SetData = function (model) {
            self.Id(model.Id);
            self.Title(model.Title);
            self.IsMandatary(model.IsMandatary);
            self.IsActive(model.IsActive);
            self.QuestionTypeId(model.QuestionTypeId);
            self.TextInputTypeId(model.TextInputTypeId);
            self.LanguageId(model.LanguageId);
            self.IsMapped(model.IsMapped);
        };

        self.ClearData = function () {
            self.Id(0);
            self.Title("");
            self.IsMandatary("");
            self.IsActive("");
            self.QuestionTypeId("");
            self.LanguageId("");
            self.TextInputTypeId("");
        };

        self.Errors = ko.validation.group(self);
        self.isValid = ko.computed(function () {
            return self.Errors().length === 0;
        });

    }
    return QuestionModel;
});