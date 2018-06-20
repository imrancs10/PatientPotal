//************************************** Survey Model : knockout view model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko, languageCode*/
define(['common'], function (Common) {
    'use strict';
    function SurveyModel() {
        var self = this;
        var common = new Common();
        self.ValidationEnabled = ko.observable(true);
        self.Id = ko.observable();
        self.Name = ko.observable().extend({required: {message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled}});
        self.IsActive = ko.observable();
        self.LanguageId = ko.observable().extend({required: {message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled}});
        //self.SurveyQuestionMapList = ko.observableArray([]);
        self.SurveyQuestionMapList = ko.observableArray([]);
        self.EncryptSurveyIdLanguageId = ko.observable();
        self.IsMapped = ko.observable();

        self.SetData = function (model) {
            self.Id(model.Id);
            self.Name(model.Name);
            self.IsActive(model.IsActive);
            self.LanguageId(model.LanguageId);
            self.IsMapped(model.IsMapped);
            self.EncryptSurveyIdLanguageId(model.EncryptSurveyIdLanguageId);
            self.SurveyQuestionMapList(model.SurveyQuestionMapList);
        };

        self.ClearData = function () {
            self.Id(0);
            self.Name('');
            self.IsActive(false);
            self.EncryptSurveyIdLanguageId('');
            //self.LanguageId("");
            self.SurveyQuestionMapList([]);
        };

        self.Errors = ko.validation.group(self);
        self.isValid = ko.computed(function () {
            return self.Errors().length === 0;
        });

    }
    return SurveyModel;
});