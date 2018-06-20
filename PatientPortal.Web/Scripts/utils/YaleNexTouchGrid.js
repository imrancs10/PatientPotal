//************************************** PatientPortal Grid Model : knockout view model **************************************//
define([''], function () {
    function PatientPortalGrid() {
        var self = this;
        //Observable Array
        self.DataRows = ko.observableArray([]);
    }
    return PatientPortalGrid;
});


