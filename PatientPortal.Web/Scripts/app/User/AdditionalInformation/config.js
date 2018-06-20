/*jslint browser: true*/
/*global requirejs,require*/
requirejs.config({
    paths: {
        common: "../../../utils/common",
        rolemodel: '../../Model/RoleModel',
        usermodel: '../../Model/UserModel',
    },
    shim: {
    }
});

require(['common', 'usermodel'], function (common, usermodel) {
    'use strict';
    require(['main'], function (main) {
        main.init();
    });
});