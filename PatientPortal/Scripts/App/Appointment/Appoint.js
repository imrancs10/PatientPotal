/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />

var appointment = {};

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.commonDepartmentList, 'ddlDepartments', 'DeparmentName', 'DepartmentId', function () {
        
    });

});
appointment.getDoctors = function (deptId) {
    utility.bindDdlByAjaxWithParam(app.urls.appointmentdeptWiseDoctorScheduleList, 'ddlDoctors', { deptId: deptId }, 'DoctorName', 'DoctorId', undefined, function () {

    });
}

$(document).on('change', '#ddlDepartments', function () {
    var deptId = $(this).find(':selected').val();
    appointment.getDoctors(deptId);
});