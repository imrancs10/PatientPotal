/// <reference path="../../jquery-1.10.2.js" />
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
    fillCountry();
    function fillCountry() {
        let dropdown = $('#country');
        dropdown.empty();
        dropdown.append('<option selected="true" disabled>Choose Country</option>');
        dropdown.prop('selectedIndex', 0);
        const url = utility.baseUrl + 'Json/countries.json';
        // Populate dropdown with list of provinces
        $.getJSON(url, function (data) {
            $.each(data.countries, function (key, entry) {
                if (entry.name == 'India')
                    dropdown.append($('<option selected="true"></option>').attr('value', entry.id).text(entry.name));
                else
                    dropdown.append($('<option></option>').attr('value', entry.id).text(entry.name));
            })
        });
    }

    $('#country').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        fillState(valueSelected)
    });
    fillState(101);
    function fillState(countryId) {
        let dropdown = $('#state');
        dropdown.empty();
        dropdown.append('<option selected="true" disabled>Choose State</option>');
        dropdown.prop('selectedIndex', 0);
        const url = utility.baseUrl + 'Json/states.json';
        // Populate dropdown with list of provinces
        $.getJSON(url, function (data) {
            var states = data.states.filter(function (i, n) {
                return i.country_id == countryId;
            });
            $.each(states, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.id).text(entry.name));
            })
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
        dropdown.append('<option selected="true" disabled>Choose City</option>');
        dropdown.prop('selectedIndex', 0);
        const url = utility.baseUrl + 'Json/cities.json';
        // Populate dropdown with list of provinces
        $.getJSON(url, function (data) {
            var cities = data.cities.filter(function (i, n) {
                return i.state_id == stateId;
            });
            $.each(cities, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.id).text(entry.name));
            })
        });
    }
});