//************************************** Product Tree Model : knockout view model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko, languageCode*/
define([''], function () {
    'use strict';
    function ProductTreeModel() {
        var self = this;
        self.ProductId = 0;
        self.text = "";
        self.spriteCssClass = 'productFolder';
        self.expanded = true;
        self.NodeType = "ProductType";
    }
    return ProductTreeModel;
});