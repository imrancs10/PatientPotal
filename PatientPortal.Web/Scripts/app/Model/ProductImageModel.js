//************************************** Admin View Model : knockout view model **************************************//
/*jslint browser: true*/
/*global define, ko*/
define([''], function () {
    'use strict';
    function ProductImageModel() {
        var self = this;
        self.Id = ko.observable();
        self.ProductId = ko.observable();
        self.ImagePath = ko.observable();
        self.ThumbnailPath = ko.observable();
        self.IsPrimary = ko.observable();
    }
    return ProductImageModel;
});