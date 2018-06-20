//************************************** Question Tree Model : knockout view model **************************************//
/*jslint browser: true, this: true*/
/*global define, ko*/
define([''], function () {
    'use strict';
    function QuestionTreeModel() {
        var self = this;
        self.QuestionId = 0;
        self.text = "";
        self.expanded = false;
        self.index = true;
        self.spriteCssClass = 'questionFolder';
        self.NodeType = "QuestionType";
        self.items = [];
    }
    return QuestionTreeModel;
});