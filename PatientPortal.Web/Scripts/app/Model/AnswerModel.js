//************************************** Answer Model : knockout view model **************************************//
/*jslint browser: true*/
/*global define, ko, languageCode */
define(['common'], function (Common) {
    'use strict';
    function AnswerModel() {
        var self = this;
        var common = new Common();
        self.Id = ko.observable();
        self.QuestionId = ko.observable();
        self.ValidationEnabled = ko.observable(true);
        self.Title = ko.observable().extend({required: {message: common.Messages[languageCode].RequiredField, onlyIf: self.ValidationEnabled}});
        self.ToolTip = ko.observable();
        self.ImagePath = ko.observable();

        self.SetData = function (model) {
            self.Id(model.Id);
            self.QuestionId(model.QuestionId);
            self.Title(model.Title);
            self.ToolTip(model.ToolTip);
            self.ImagePath(model.ImagePath);
        };

        self.ClearData = function () {
            self.Id(0);
            self.QuestionId(0);
            self.Title('');
            self.ToolTip('');
        };

        self.Errors = ko.validation.group(self);
        self.isValid = ko.computed(function () {
            return self.Errors().length === 0;
        });
    }
    return AnswerModel;
});