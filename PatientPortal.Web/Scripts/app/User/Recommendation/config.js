/*jslint browser: true*/
/*global requirejs,require*/
requirejs.config({
    paths: {
        yaleNexGrid: '../../../utils/PatientPortalGrid',
        common: "../../../utils/common",
        rolemodel: '../../Model/RoleModel',
        usermodel: '../../Model/UserModel',
    },
    shim: {
    }
});

require(['yaleNexGrid', 'common', 'usermodel'], function (yaleNexGrid, common, usermodel) {
    'use strict';
    require(['main'], function (main) {
        main.init();
    });
});