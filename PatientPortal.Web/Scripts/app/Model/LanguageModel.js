//************************************** Language Model : knockout model **************************************//
/*jslint browser: true, this : true*/
/*global define, ko*/
define([''], function () {
    'use strict';
    function LanguageModel() {
        var self = this;
        self.Id = ko.observable();
        self.Name = ko.observable();
        self.CultureCode = ko.observable();
        self.SetData = function (model) {
            self.Id(model.Id);
            self.Name(model.Name);
            self.CultureCode(model.CultureCode);
        };
    }
    return LanguageModel;
});