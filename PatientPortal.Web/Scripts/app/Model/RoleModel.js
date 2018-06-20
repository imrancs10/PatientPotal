//************************************** Admin View Model : knockout view model **************************************//
/*jslint browser: true*/
/*global define, ko*/
define([''], function () {
    'use strict';
    function RoleModel() {
        var self = this;
        self.Id = ko.observable();
        self.Type = ko.observable();
        self.SetData = function (model) {
            self.Id(model.Id);
            self.Type(model.Type);
        };
    }
    return RoleModel;
});