﻿/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

$(function () {
    $("#dialog").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });
});

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.commonDepartmentList, 'department', 'DeparmentName', 'DepartmentId', function () {
        //
    });
    var jsonData = jsonPatient;

    //fillCountry();
    //function fillCountry() {
    //    let dropdown = $('#country');
    //    dropdown.empty();
    //    dropdown.append('<option selected="true" disabled>Choose Country</option>');
    //    dropdown.prop('selectedIndex', 0);
    //    const url = utility.baseUrl + 'Json/countries.json';
    //    // Populate dropdown with list of provinces
    //    $.getJSON(url, function (data) {
    //        $.each(data.countries, function (key, entry) {
    //            if (entry.name == 'India')
    //                dropdown.append($('<option selected="true"></option>').attr('value', entry.id).text(entry.name));
    //            else
    //                dropdown.append($('<option></option>').attr('value', entry.id).text(entry.name));
    //        })
    //    });
    //}

    //$('#country').on('change', function (e) {
    //    var optionSelected = $("option:selected", this);
    //    var valueSelected = this.value;
    //    fillState(valueSelected)
    //});
    fillState(); //101 is the country id of India
    function fillState() {
        let dropdown = $('#state');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Home/GetSates',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.StateId).text(entry.StateName));
                })
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    $('#state').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        fillCity(valueSelected)
    });

    function fillCity(stateId) {
        let dropdown = $('#city');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Home/GetCities',
            data: '{stateId: "' + stateId + '" }',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.CityId).text(entry.CityName));
                })
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    if (jsonData != null) {
        fillCountryStateCity();
    }
    function fillCountryStateCity() {
        //Get CIty
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Home/GetCitieByCItyId',
            data: '{citiId: "' + jsonData.City + '" }',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#cityLabel").html(data.CityName);
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
        //get State
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Home/GetStateByStateId',
            data: '{stateId: "' + jsonData.State + '" }',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#stateLabel").html(data.StateName);
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
});