/*jslint browser: true*/
/*global requirejs,require*/
requirejs.config({
    paths: {
        common: '../../../utils/common',
        productimagemodel: '../../Model/ProductImageModel',
        productmodel: '../../Model/ProductModel',
        PatientPortalgrid: '../../../utils/PatientPortalGrid'
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