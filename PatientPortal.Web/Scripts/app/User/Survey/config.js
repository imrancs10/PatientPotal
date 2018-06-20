/*jslint browser: true*/
/*global requirejs,require*/
requirejs.config({
    paths: {
        common: "../../../utils/common", 
        questionmodel: '../../Model/QuestionModel',
        rolemodel: '../../Model/RoleModel',
        usermodel: '../../Model/UserModel',

    },
    shim: {
    }
});

require(['questionmodel', 'usermodel'], function (questionmodel, usermodel) {
    'use strict';
    require(['main'], function (main) {
        main.init();
    });
});