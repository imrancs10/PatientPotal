/*jslint browser: true*/
/*global requirejs,require*/
requirejs.config({
    paths: {
        common: "../../../utils/common",
        jqueryMigrate: "../../../lib/jquery-migrate-1.3.0",
        yaleNexGrid: '../../../utils/PatientPortalGrid',
        surveymodel: '../../Model/SurveyModel',
        questiontreemodel: '../../Model/QuestionTreeModel',
        answertreemodel: '../../Model/AnswerTreeModel',
        producttreemodel: '../../Model/ProductTreeModel',
        surveyquestionmapmodel: '../../Model/SurveyQuestionMapModel'
    },
    shim: {
    }
});

require(['common'], function (common) {
    'use strict';
    require(['main'], function (main) {
        main.init();
    });
});