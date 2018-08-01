$(document).ready(function() {
    appList();
});
var appList = function () {
    utility.ajax.helper(app.urls.appointmentGetPatientAppointmentList, function (data) {
        var visitTable = $('#tblevisit tbody');
        var appTable = $('#tbleappointment tbody');
        $(visitTable).empty();
        $(appTable).empty();
        var srno1 = 0;
        var srno2 = 0;
        var tr = '';
        $(data).each(function (ind, ele) {
            var fromtime = new Date(parseInt(ele.AppointmentDateFrom.substr(6)));
            var date = new Date();
            if (!ele.IsCancelled && date.compareDate(fromtime, new Date()).isDateLess) {
                srno1++;
                tr += '<tr>';
                tr += '<td class="text-center">' + srno1 + '</td>';
                tr += '<td class="text-center">' + new Date(parseInt(ele.AppointmentDateFrom.substr(6))).toDateString().trim() + '</td>';
                tr += '<td class="text-center">' + ele.DepartmentName + '</td>';
                tr += '<td class="text-center">' + ele.DoctorName + '</td>';
                tr += '</tr>';
                $(visitTable).append(tr);
            }
            else {
                srno2++;
                tr += '<tr>';
                tr += '<td class="text-center">' + srno2 + '</td>';
                tr += '<td class="text-center">' + new Date(parseInt(ele.AppointmentDateFrom.substr(6))).toDateString().trim() + '</td>';
                tr += '<td class="text-center">' + ele.DepartmentName + '</td>';
                tr += '<td class="text-center">' + ele.DoctorName + '</td>';
                tr += '<td class="text-center">' + new Date(parseInt(ele.AppointmentDateFrom.substr(6))).toTimeString().substr(0, 5) + ' - ' + new Date(parseInt(ele.AppointmentDateTo.substr(6))).toTimeString().substr(0, 5) + '</td>';
                tr += '<td class="text-center">' + (ele.IsCancelled ? "Cancelled" : "Booked") + '</td>';
                if (!ele.IsCancelled) {
                    tr += '<td class="text-center"><button id="btnCancel_' + srno2 + '" class="btn btn-danger" data-data="' + ele.AppointmentId + '" style="padding: 2px 12px !important;">Cancel</button></td>';
                }
                else
                {
                    tr += '<td style="padding: 20px 0;"></td>';
                }

                tr += '</tr>';
                $(appTable).append(tr);
            }
            tr = '';
        });
    }, undefined, "GET");
}
$(document).on('click', '[id*="btnCancel_"]', function () {
    var appId = $(this).data('data');
    utility.ajax.helperWithData(app.urls.appointmentCancelAppointment, { 'appointmentId': appId, 'CancelReason': '' }, function (data) {
        if(data[0].Key==103)
        {
            utility.alert.setAlert( utility.alert.alertType.success,data[0].Value);
            appList();
        }
        if (data[0].Key == 102) {
            utility.alert.setAlert(utility.alert.alertType.error,data[0].Value);
        }
    });
});