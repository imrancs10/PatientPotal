/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />

$(document).ready(function () {
    var jsonData = jsonPatient;

    utility.bindDdlByAjax(app.urls.commonDepartmentList, 'department', 'DeparmentName', 'DepartmentId', function () {
        $("#department").val(jsonData.Department);
    });

    if (jsonData.PinCode == 0) {
        $("#pincode").val("");
    }

    if (jsonData.DOB == null) {
        $("#DOB").val("");
    }

    setSelectedGender();
    function setSelectedGender() {
        $("#Gender").val(jsonData.Gender);
    }

    setSelectedReligion();
    function setSelectedReligion() {
        $("#religion").val(jsonData.Religion);
    }
    fillState();
    function fillState() {
        let dropdown = $('#state');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Home/GetSates',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.StateId).text(entry.StateName));
                });
                dropdown.val(jsonData.State);
                fillCity();
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    function fillCity() {
        var stateId = jsonData.State;
        let dropdown = $('#city');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Home/GetCities',
            data: '{stateId: "' + stateId + '" }',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.CityId).text(entry.CityName));
                });
                dropdown.val(jsonData.City);
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
        fillCityByStateId(valueSelected);
    });

    function fillCityByStateId(stateId) {
        //var stateId = jsonData.State;
        let dropdown = $('#city');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
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

    $("input:file").change(function () {
        var fileName = $(this).val();
        fileName = fileName.substring(fileName.indexOf('fakepath') + 9, fileName.length);
        $("#filename").html(fileName);
    });
});

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