/*jslint browser: true*/
/*global requirejs,require*/
requirejs.config({
    paths: {
        common: "../../../utils/common",
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