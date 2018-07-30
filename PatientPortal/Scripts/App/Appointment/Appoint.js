﻿/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />

var appointment = {};

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.commonDepartmentList, 'ddlDepartments', 'DeparmentName', 'DepartmentId');
    //appointment.bindCalendar();
    var date = new Date();
    $('#lblmonthyear').text(utility.global.getMonthArray[date.getMonth()] + ', ' + date.getFullYear());
});
//appointment.getDoctors = function (deptId) {
//    utility.ajax.helperWithData(app.urls.appointmentdeptWiseDoctorScheduleList, { deptId: deptId },function (data) {
//        var newData = data;
//    });
//    //utility.bindDdlByAjaxWithParam(app.urls.appointmentdeptWiseDoctorScheduleList, 'ddlDoctors', { deptId: deptId }, 'DoctorName', 'DoctorId', undefined, );
//}

$(document).on('change', '#ddlDepartments', function () {
    var deptId = $(this).find(':selected').val();
    if (deptId != '') {
        appointment.bindCalendar();
        $('.step1').hide();
        $('.step2').show();
        $('.step3').hide();
        $('#spanDeptName').text($(this).find(':selected').html().trim());
    }
    else {
        utility.alert.setAlert(utility.alert.alertType.warning, 'Please select department');
        $('.step2').hide();
        $('.step3').hide();
    }
});

$(document).on('click', '#nextmonth', function () {
    var currentMonth = parseInt($(this).data('currentmonth'));
    currentMonth = isNaN(currentMonth) ? 0 : currentMonth;
    $('#nextmonth').data('currentmonth', (currentMonth + 1));
    var newDate = new Date();
    newDate.setMonth((newDate.getMonth() + currentMonth + 1));
    appointment.bindCalendar(newDate.getFullYear(), newDate.getMonth());
    $('#lblmonthyear').text(utility.global.getMonthArray[newDate.getMonth()] + ', ' + newDate.getFullYear());
});

$(document).on('click', '#btnToday', function () {
    var newDate = new Date();
    appointment.bindCalendar(newDate.getFullYear(), newDate.getMonth());
    $('#lblmonthyear').text(utility.global.getMonthArray[newDate.getMonth()] + ', ' + newDate.getFullYear());
});


$(document).on('click', '#premonth', function () {
    var currentMonth = parseInt($('#nextmonth').data('currentmonth'));
    currentMonth = isNaN(currentMonth) ? 0 : currentMonth;
    $('#nextmonth').data('currentmonth', (currentMonth - 1));
    var newDate = new Date();
    newDate.setMonth((newDate.getMonth() + currentMonth - 1));
    appointment.bindCalendar(newDate.getFullYear(), newDate.getMonth());
    $('#lblmonthyear').text(utility.global.getMonthArray[newDate.getMonth()] + ', ' + newDate.getFullYear());
});

appointment.bindCalendar = function (year, month) {
    var table = $('#appointTable');
    var tbody = $(table).find('tbody');
    var date = new Date();
    var currentDate = date.getDate();
    var inpuYear = year === undefined ? date.getFullYear() : year;
    var inpuMonth = month === undefined ? date.getMonth() : month;
    var dateObj = date.getCustomDetails(inpuYear, inpuMonth);
    var tr = '';
    var index = 0;
    var day = 0;
    var rowLength = 5;
    var deptId = $('#ddlDepartments').find(':selected').val();

    //empty table 

    $(tbody).find('tr:gt(0)').remove();

    rowLength = dateObj.firstDayIndex == 6 && dateObj.totalDays > 29 ? rowLength + 1 : rowLength;
    utility.ajax.helperWithData(app.urls.appointmentdeptWiseDoctorScheduleList, { deptId: deptId }, function (data) {
        var totalAvailable = [];
        $('#btnStep2').data('data', data);
        $(data).each(function (ind, ele) {
            if (ele.length > 0) {
                totalAvailable[utility.global.getFullDaysArray.indexOf(ele[0].DayName)] = ele.length;
            }
        });
        for (var i = 0; i < rowLength; i++) {
            tr += '<tr>';
            for (var j = 0; j < 7; j++) {
                var availableDoctor = totalAvailable[j] === undefined ? 0 : totalAvailable[j];
                if ((index == 0 && j >= dateObj.firstDayIndex) || (index > 0 && day < dateObj.totalDays)) {
                    day += 1;
                    if ((dateObj.currentMonth >= dateObj.todayMonth && dateObj.currentYear >= dateObj.todayYear && day >= dateObj.todayDate) || (dateObj.currentMonth > dateObj.todayMonth && dateObj.currentYear >= dateObj.todayYear)) {
                        if (day == currentDate && inpuYear == date.getFullYear() && inpuMonth == date.getMonth()) {
                            if (availableDoctor > 0)
                                tr += '<td data-day="' + utility.global.getFullDaysArray[j] + '" data-date="' + (dateObj.currentYear + '-' + dateObj.currentMonth + '-' + day) + '" class="btn-info getApp"><div class="cal-date">' + day + '</div><div class="cal-available">Available : ' + availableDoctor + '</div></td>';
                            else {
                                tr += '<td data-day="' + utility.global.getFullDaysArray[j] + '" class="btn-info getApp-disable"><div class="cal-date">' + day + '</div><div class="cal-not-available">Available : ' + availableDoctor + '</div></td>';
                            }
                        }
                        else {
                            if (availableDoctor > 0)
                                tr += '<td data-day="' + utility.global.getFullDaysArray[j] + '" data-date="' + (dateObj.currentYear + '-' + dateObj.currentMonth + '-' + day) + '" class="getApp"><div class="cal-date">' + day + '</div><div class="cal-available">Available : ' + availableDoctor + '</div></td>';
                            else
                                tr += '<td data-day="' + utility.global.getFullDaysArray[j] + '" class="getApp-disable"><div class="cal-date">' + day + '</div><div class="cal-not-available">Available : ' + availableDoctor + '</div></td>';
                        }
                    }
                    else {
                        tr += '<td style="background:#a2a2a285" title="Date already passed"><div class="cal-date">' + day + '</div><div class="cal-not-available">Available : ' + 0 + '</div></td>';
                    }
                }
                else if ((index == 0 && j < dateObj.firstDayIndex) || day >= dateObj.totalDays)
                    tr += '<td></td>';
            }
            tr += "</tr>"
            index += 1;
        }

        $(tbody).append(tr);
    });


}

$(document).on('click', '.getApp', function () {
    $('.step1').hide();
    $('.step2').hide();
    $('.step3').show();
    $('#spanDepartment').text('Department : ' + $('#ddlDepartments').find(':selected').text());
    $('#spanDate').text('Date : ' + new Date($(this).data('date')).toDateString());
    appointment.binddoctor($(this).data('day'));
});
$(document).on('click', '#btnStep2', function () {
    $('.step2').show();
    $('.step1').hide();
    $('.step3').hide();
});

$(document).on('click', '#btnStep1', function () {
    $('.step2').hide();
    $('.step1').show();
    $('.step3').hide();
});

$(document).on('click', '.timelabel', function () {
    $(this).parent().parent().parent().find('.timelabelActive').removeClass('timelabelActive');
    $(this).addClass('timelabelActive');
    var param = {};
    param.AppointmentDateFrom = ($('#spanDate').text().trim() + ' ' + ($(this).text().trim().split(' - ')[0]).split(' ')[0]).replace('Date : ', '');
    param.AppointmentDateTo = ($('#spanDate').text().trim() + ' ' + ($(this).text().trim().split(' - ')[1]).split(' ')[0]).replace('Date : ', '');
    param.doctorname = $(this).parent().parent().find('td:eq(1)').text().trim();
    param.DoctorId = $(this).parent().parent().data('doctorid');
    param.deptname = $('#spanDepartment').text().split(' : ')[1];
    $('#btnGetAppointment').data('data', param);
    $('#selectAppointmant').text('You have selected appointment for ' + $(this).parent().parent().find('td:eq(1)').text().trim() + ' on ' + $('#spanDate').text().trim() + ' ' + $(this).text().trim());
});

$(document).on('click', '#btnGetAppointment', function () {
    if ($(this).data('data') != '') {
        utility.ajax.helperWithData(app.urls.appointmentSaveAppointment, $(this).data('data'), function (data) {
            if (data = 'Data has been saved') {
                utility.alert.setAlert(utility.alert.alertType.success, 'Your appointment has been booked');
                $('.timelabelActive').addClass('Bookedtimelabel').removeClass('timelabel').removeClass('timelabelActive');
                $('#selectAppointmant').text();
                $('#btnGetAppointment').data('data','')
            }
        });
    }
    else
    {
        utility.alert.setAlert(utility.alert.alertType.warning, 'Please select the appointment time');
    }
});

appointment.binddoctor = function (day) {
    var data = $('#btnStep2').data('data');
    var deptId = $('#ddlDepartments').find(':selected').val();
    var doctorList = [];
    utility.ajax.helperWithData(app.urls.appointmentdayWiseDoctorScheduleList, { deptId: deptId, day: day }, function (doctorListdata) {
        utility.ajax.helperWithData(app.urls.appointmentDateWiseDoctorAppointmentList, { date: new Date($('#spanDate').text().trim().split(' : ')[1]) }, function (AppointmentListdata) { 
        doctorList = doctorListdata;
        var table = $('#appointDoctorTable tbody');
        var srno = 1;
        var tr = '';
        $(table).empty();

        $(doctorList).each(function (ind, ele) {
            var appList = $(AppointmentListdata).filter(function(ind,listele) {
                if (listele[0].DoctorId == ele[0].DoctorID)
                    return listele;
            });
            tr += '<tr data-doctorid="' + ele[0].DoctorID + '">';
            tr += '<td style="padding: 3%;">' + srno + '</td>';
            tr += '<td style="padding: 3%;">' + ele[0].DoctorName + '</td>';
            tr += '<td>' + Timelist(ele, appList) + '</td>';
            tr += '</tr>';
            srno += 1;
        });

        $(table).append(tr);
        });
    });
}

function Timelist(ele, appList) {
    appList = appList.length > 0 ? appList : [{'a':0}];
    var html = '';
    $(ele).each(function (ind1, ele1) {       
            var tList = utility.global.timeSplitter(ele1.TimeFrom, ele1.TimeTo, 30);
            for (var i = 1; i < tList.length - 1; i++) {              
                    var availableAppTime = tList[i - 1] + ' - ' + tList[i];
                    html += '<div class="timelabel" title="' + availableAppTime + ' slot available for booking">' + availableAppTime + '</div>';
            }
       
    });
    $(appList[0]).each(function (ind, ele) {
        if (ele.AppointmentDateFrom) {
            var fromtime = new Date(parseInt(ele.AppointmentDateFrom.substr(6))).toTimeString().substr(0, 5);
            var totime = new Date(parseInt(ele.AppointmentDateTo.substr(6))).toTimeString().substr(0, 5);
            var bookedAppTime = (parseInt(fromtime.substr(0, 2)) > 11 ? fromtime + ' PM' : fromtime + ' AM') + ' - ' + (parseInt(totime.substr(0, 2)) > 11 ? totime + ' PM' : totime + ' AM');
            var oldString = '<div class="timelabel" title="' + bookedAppTime + ' slot available for booking">' + bookedAppTime + '</div>';
            var newString = '<div class="Bookedtimelabel" title="' + bookedAppTime + ' slot already booked">' + bookedAppTime + '</div>';
            if (html.indexOf(oldString) > -1) {
                html = html.replace(oldString, newString);
            }
        }
    });

    return html;

}