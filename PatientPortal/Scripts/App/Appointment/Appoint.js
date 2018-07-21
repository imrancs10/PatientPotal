/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />

var appointment = {};

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.commonDepartmentList, 'ddlDepartments', 'DeparmentName', 'DepartmentId');
    appointment.bindCalendar();
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
    appointment.bindCalendar();
});

$(document).on('click', '#nextmonth', function () {
    var currentMonth = parseInt($(this).data('currentmonth'));
    currentMonth = isNaN(currentMonth) ? 0 : currentMonth;
    $('#nextmonth').data('currentmonth', (currentMonth + 1));
    var newDate = new Date();
    newDate.setMonth((newDate.getMonth()+currentMonth + 1));
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
    newDate.setMonth((newDate.getMonth() + currentMonth -1));
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
    var deptId = $('#ddlDepartments').find(':selected').val()

    //empty table 

    $(tbody).find('tr:gt(0)').remove();

    rowLength = dateObj.firstDayIndex == 6 && dateObj.totalDays > 29 ? rowLength + 1 : rowLength;
    utility.ajax.helperWithData(app.urls.appointmentdeptWiseDoctorScheduleList, { deptId: deptId }, function (data) {
        var totalAvailable = [];
        $(data).each(function(ind,ele) {
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
                    if (day == currentDate && inpuYear == date.getFullYear() && inpuMonth == date.getMonth())
                        if (availableDoctor>0)
                            tr += '<td class="btn-info"><div class="cal-date">' + day + '</div><div class="cal-available">Available : ' + availableDoctor + '</div></td>';
                        else
                        {
                            tr += '<td class="btn-info"><div class="cal-date">' + day + '</div><div class="cal-not-available">Available : ' + availableDoctor + '</div></td>';
                        }
                    else
                        if (availableDoctor > 0)
                            tr += '<td><div class="cal-date">' + day + '</div><div class="cal-available">Available : ' + availableDoctor + '</div></td>';
                        else
                            tr += '<td><div class="cal-date">' + day + '</div><div class="cal-not-available">Available : ' + availableDoctor + '</div></td>';
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