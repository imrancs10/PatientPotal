//************************************** Answer Tree Model : knockout view model **************************************//
/*jslint browser: true*/
/*global define*/
define([''], function () {
    'use strict';
    function AnswerTreeModel() {
        var self = this;
        self.AnswerId = 0;
        self.text = "";
        self.expanded = true;
        self.spriteCssClass = 'answerFolder';
        self.NodeType = "AnswerType";
        self.items = [];
    }
    return AnswerTreeModel;
});