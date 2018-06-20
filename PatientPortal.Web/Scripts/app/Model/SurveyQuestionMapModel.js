//************************************** SurveyQuestionMap Model : knockout model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko*/
define([''], function () {
    'use strict';
    function SurveyQuestionMapModel() {
        var self = this;

        self.Id = ko.observable();
        self.SurveyId = ko.observable();
        self.QuestionId = ko.observable();
        self.AnswerId = ko.observable();
        self.ProductId = ko.observable();
        self.ChildQuestionId = ko.observable();
        self.QuestionOrderNumber = ko.observable();
        self.SurveyQuestionMapId = ko.observable();
        self.IsParent = ko.observable();
        self.AnswerDTOList = ko.observableArray([]);
        self.IsMainNode = ko.observable();
        self.SameNodeNumber = ko.observable();
        self.SameQuestionNumber = ko.observable();

        self.SetData = function (model) {
            self.Id(model.Id);
            self.SurveyId(model.SurveyId);
            self.QuestionId(model.QuestionId);
            self.AnswerId(model.AnswerId);
            self.ProductId(model.ProductId);
            self.ChildQuestionId(model.ChildQuestionId);
            self.QuestionOrderNumber(model.QuestionOrderNumber);
            self.SurveyQuestionMapId.SetData(model.SurveyQuestionMapId);
            self.IsParent.SetData(model.IsParent);
            self.IsMainNode.SetData(model.IsMainNode);
            self.AnswerDTOList(model.AnswerDTOList);
            self.SameQuestionNumber(model.SameQuestionNumber);
        };
    }
    return SurveyQuestionMapModel;
});