//************************************** Question Type Model : knockout model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko*/
define([''], function () {
    'use strict';
    function QuestionTypeModel() {
        var self = this;
        self.Id = ko.observable();
        self.Type = ko.observable();
        self.SetData = function (model) {
            self.Id(model.Id);
            self.Type(model.Type);
        };
    }
    return QuestionTypeModel;
});