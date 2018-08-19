/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />

var labreport = {};

$(document).ready(function () {

});

labreport.openModal = function () {
    $(".modal").modal("show");
}

labreport.cancel = function (row) {
    $(row).parent().parent().parent().remove();
}


