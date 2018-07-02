/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="App.js" />

var app = {};

app.urls = {};

app.urls.commonDepartmentList = '/common/GetDepartments';

app.urls.departmentList = '/masters/GetDepartments';
app.urls.departmentSave = '/masters/SaveDepartment';
app.urls.departmentEdit = '/masters/EditDepartment';
app.urls.departmentDelete = '/masters/DeleteDepartment';

app.urls.doctorList = '/masters/GetDoctors';
app.urls.doctorSave = '/masters/SaveDoctor';
app.urls.doctorEdit = '/masters/EditDoctor';
app.urls.doctorDelete = '/masters/DeleteDoctor';