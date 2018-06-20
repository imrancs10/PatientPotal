/*jslint browser: true*/
/*global requirejs,require*/
requirejs.config({
    paths: {
        common: "../../../utils/common",
        yaleNexGrid: '../../../utils/PatientPortalGrid',
        questionmodel: '../../Model/QuestionModel',
        answermodel: '../../Model/AnswerModel',
        questiontypemodel: '../../Model/QuestionTypeModel',
        languagemodel: '../../Model/LanguageModel'
    },
    shim: {
    }
});

require(['common', 'yaleNexGrid', 'questionmodel', 'answermodel'], function (common, yaleNexGrid, questionmodel, answermodel) {
    'use strict';
    require(['main'], function (main) {
        main.init();
    });
});