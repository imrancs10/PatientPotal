//**************************************Additonal Information View Model : knockout view model **************************************//

define(['../../Model/UserDetailModel', 'common', 'usermodel'], function (UserDetailModel, Common, UserModel) {
    function AdditionalInfoViewModel() {
        var self = this;
        self.States = ko.observableArray([]);
        var common = new Common();
        self.userDetailModel = new UserDetailModel();
        self.CurrentAction = null;
        self.userModel = new UserModel();

        //show forget password popup 
        self.ForgetPassword = function () {
            $("#divSignin").hide();
            $('#divForgetPassword').fadeIn();
            $('#divForgetPassword').show();

        };

        //send mail for forget password
        self.SendForgetPassword = function () {
            common.SendForgetPassword(self.userModel);
        };

        //back to login page
        self.BackToLogin = function () {
            common.BackToLogin();
        };

        //login action occure
        self.LoginUser = function () {
            common.LoginUser(self.userModel);
        };

        //cancel button event of login popup
        self.LoginCancel = function () {
            self.userModel.UserName('');
            self.userModel.Password('');
            self.userModel.ConfirmPassword(self.userModel.Password());
            self.userModel.ValidationEnabledForget(false);
            self.userModel.ValidationEnabledConfirmPassword(false);
            self.userModel.ValidationEnabledSignIn(false);
            $('#modelLogin').modal('hide');
        };

        //get state list
        self.GetStatesDropdownlist = function () {
            self.userDetailModel.ValidationEnabled(false);
            common.GetAJAXCall(common.APIUrls.GetStateList, {}, self.GetStatesDropdownlistSuccess);
        };

        //state list success method
        self.GetStatesDropdownlistSuccess = function (data) {
            var states = [];
            $.each(data, function () {
                states.push({ Name: this.Name, Id: this.Id });
            });
            //self.TerritoryEmails 
            self.States(states);
            common.HideLoader();
            ko.applyBindings(self, document.getElementById('divLogin'));
            self.SetPlaceHolder();
            //if ($('#spanIsLogin').text() == 'false')
            //    $('#modelLogin').modal('show');
        };

        //get additional information
        self.getAdditionalInfo = function () {
            self.userDetailModel.ValidationEnabled(false);
            common.GetAJAXCall(common.APIUrls.GetAdditionalInformation, {}, self.getAdditionalInfoSuccess);
        };

        //additional information success method
        self.getAdditionalInfoSuccess = function (data) {
            if (data != null && data.Id > 0) {
                //$.each(data.TerritoryEmails, function (emailId) {
                //    self.userDetailModel.te
                //});
                self.userDetailModel.SetData(data);
                $('#chkRegisterUser').parent().css('display', 'none');
                self.setControlDisabled();
            }
        };

        //disable control after login
        self.setControlDisabled = function () {
            $('#BusinessNameInput').attr('disabled', 'disabled');
            $('#FirstNameInput').attr('disabled', 'disabled');
            $('#LastNameInput').attr('disabled', 'disabled');
            $('#StateInput').attr('disabled', 'disabled');
            $('#ZipCodeInput').attr('disabled', 'disabled');
            $('#PhoneNumberInput').attr('disabled', 'disabled');
            $('#EmailIdInput').attr('disabled', 'disabled');
        };

        //Submit Addition information
        self.SaveAdditionalInfo = function () {
            var currentdate = new Date(), fileName;
            fileName = "Yale Multi-Family Housing Product Recommendation- " + (currentdate.getMonth() + 1) + common.Sepretor.Hypen + (currentdate.getDate()) + common.Sepretor.Hypen + currentdate.getFullYear() + common.Sepretor.Hypen + currentdate.getMinutes() + common.Sepretor.Hypen + currentdate.getSeconds() + common.FileType.PDF;

            var chkPromotional = $('#chkPromotional').prop('class').indexOf('active') > -1;
            if (chkPromotional == true) {
                self.userDetailModel.IsReqForPromotions(true);
            }
            else {
                self.userDetailModel.IsReqForPromotions(false);
            }
            self.userDetailModel.IsAdvisorToContact(false);
            self.userDetailModel.PDFFileName(fileName);
            self.userDetailModel.ValidationEnabled(true);
            self.userDetailModel.ValidationEnabledLogin(false);
            if (!self.userDetailModel.isValid()) {
                self.userDetailModel.Errors.showAllMessages();
                return;
            }
            var isAnychecked = false;
            $('[name*=MultiSelect]').each(function () {
                if ($(this).parent().prop('class').indexOf('active') > -1) {
                    isAnychecked = true;
                    return false;
                }

            })
            if (isAnychecked == false) {
                common.AlertBox(common.Messages[languageCode].CheckboxCheckedMessage);
                return;
            }
            else {
                var chkRegisterUser = false;
                var chkAdvisorContact = $('#chkAdvisorContact').prop('class').indexOf('active') > -1;
                var chkPDFDownload = $('#chkPDFDownload').prop('class').indexOf('active') > -1;
                if ($('#chkRegisterUser').length > 0)
                    chkRegisterUser = $('#chkRegisterUser').prop('class').indexOf('active') > -1;

                if (chkAdvisorContact == true && chkPDFDownload == true && chkRegisterUser == true) {
                    //save information, pdf downlaod and open register popup
                    self.userDetailModel.IsPDFDownload(true);
                    self.userDetailModel.IsAdvisorToContact(true);
                    self.userDetailModel.IsMailSend(true);
                    self.CurrentAction = 123;
                    self.userDetailModel.Password('');
                    if (self.userDetailModel.ConfirmPassword() != null || self.userDetailModel.ConfirmPassword() != undefined)
                        self.userDetailModel.ConfirmPassword('');
                    
                    //$('#loginSuccessDiv').addClass('hidden');
                    //$('#loginDiv').removeClass('hidden');
                    $('#LoginModel').modal('show');
                }
                else if (chkAdvisorContact == true && chkPDFDownload == true) {
                    //save information and downlaod pdf
                    self.userDetailModel.IsPDFDownload(true);
                    self.userDetailModel.IsMailSend(true);
                    self.userDetailModel.IsAdvisorToContact(true);
                    self.CurrentAction = 12;
                    self.userDetailModel.Password('');
                    self.saveData();
                    //code for pdf download
                }
                else if (chkAdvisorContact == true && chkRegisterUser == true) {
                    //save indformation and open login popup
                    self.CurrentAction = 13;
                    self.userDetailModel.IsPDFDownload(false);
                    self.userDetailModel.IsMailSend(true);
                    self.userDetailModel.IsAdvisorToContact(true);
                    self.userDetailModel.Password('');
                    if (self.userDetailModel.ConfirmPassword() != null || self.userDetailModel.ConfirmPassword() != undefined)
                        self.userDetailModel.ConfirmPassword('');
                    $('#LoginModel').modal('show');
                }
                else if (chkPDFDownload == true && chkRegisterUser == true) {
                    //save information,downaload pdf and open login popup
                    self.userDetailModel.IsPDFDownload(true);
                    self.userDetailModel.IsMailSend(true);
                    self.CurrentAction = 23;
                    self.userDetailModel.Password('');
                    if (self.userDetailModel.ConfirmPassword() != null || self.userDetailModel.ConfirmPassword() != undefined)
                        self.userDetailModel.ConfirmPassword('');
                    $('#LoginModel').modal('show');
                }
                else if (chkAdvisorContact == true) {
                    //save information and show thanku message
                    self.userDetailModel.IsPDFDownload(false);
                    self.userDetailModel.IsMailSend(true);
                    self.userDetailModel.IsAdvisorToContact(true);
                    self.CurrentAction = 1;
                    self.userDetailModel.Password('');
                    self.saveData();
                }
                else if (chkPDFDownload == true) {
                    //downlaod pdf only
                    self.CurrentAction = 2;
                    self.userDetailModel.IsPDFDownload(true);
                    self.userDetailModel.IsMailSend(true);
                    self.userDetailModel.Password('');
                    self.saveData();
                    //code for pdf download
                }
                else if (chkRegisterUser == true) {
                    //open login popup
                    self.userDetailModel.IsPDFDownload(false);
                    self.userDetailModel.IsMailSend(true);
                    self.CurrentAction = 3;
                    self.userDetailModel.Password('');
                    if (self.userDetailModel.ConfirmPassword() != null || self.userDetailModel.ConfirmPassword() != undefined)
                        self.userDetailModel.ConfirmPassword('');
                    $('#LoginModel').modal('show');
                }

                if (chkAdvisorContact == false && chkPDFDownload == false && chkRegisterUser == false && chkPromotional == true) {
                    self.CurrentAction = 4;
                    self.saveData();
                }

                // File attached and downloaded
                if (chkAdvisorContact == true && chkPDFDownload == true && chkRegisterUser == false) {
                    $.ajax({
                        url: common.APIUrls.DownloadPDFAndAttachedPDF + fileName,
                        async: false,
                        success: function (data) {
                            self.CreateAnchorAndDownloadPDF(baseUrl + "Images/PDFFiles/" + fileName);
                            self.sendMail();
                            self.ErrorMessage(data);
                        }
                    });
                }

                // File attached and not downloaded
                if (chkAdvisorContact == true && chkPDFDownload == false && chkRegisterUser == false) {
                    $.ajax({
                        url: common.APIUrls.PDFAttached + fileName,
                        async: false,
                        success: function (data) {
                            self.sendMail();
                            self.ErrorMessage(data);
                        } 
                    });
                }

                // File not attached but downloaded
                if (chkAdvisorContact == false && chkPDFDownload == true && chkRegisterUser == false) {
                    $.ajax({
                        url: common.APIUrls.DownloadPDF + fileName,
                        async: false,
                        success: function (data) {
                            self.CreateAnchorAndDownloadPDF("Images/PDFFiles/" + fileName);
                            self.sendMail();
                            self.ErrorMessage(data);
                        }
                    });
                }

                // file not attached, not downloaded, only survey result attached
                if (chkAdvisorContact == false && chkPDFDownload == false && chkRegisterUser == false) {
                    $.ajax({
                        url: common.APIUrls.DownloadUserSurveyResult + fileName,
                        async: false,
                        success: function (data) {
                            self.sendMail();
                        }
                    });
                }
                self.SetPlaceHolder();
            }
        }

        self.ErrorMessage = function (data) {
            if (data != null && parseInt(data) > 0)
            {
                if (data == "1")
                {
                    common.AlertBox(common.Messages[languageCode].FailedToDownloadPDF);
                } else if (data == "2")
                {
                    common.AlertBox(common.Messages[languageCode].ContactToAdvisor);
                } else if (data == "3") {
                    common.AlertBox(common.Messages[languageCode].FailedContactToAdvisorAndDownloadPDF);
                }
            }
        },

        // Create Anchor Tag and download file
        self.CreateAnchorAndDownloadPDF = function (filePath) {
            var lastIndexOfBackSlash = filePath.lastIndexOf('/')
            $("#aFileDownload").attr("href", filePath);
            $("#aFileDownload").attr("download", filePath.substring(lastIndexOfBackSlash + 1));
            $("#aFileDownload")[0].click()
        };

        //save additional information
        self.SaveLoginAdditionalInfo = function () {
            self.userDetailModel.ValidationEnabled(false);
            self.userDetailModel.ValidationEnabledLogin(true);
            //var isValid = self.isValidLogin();
            if (!self.userDetailModel.isValid()) {
                self.userDetailModel.Errors.showAllMessages();
                return;
            }
            else {
                var currentdate = new Date(), fileName;
                fileName = "Yale Multi-Family Housing Product Recommendation- " + (currentdate.getMonth() + 1) + common.Sepretor.Hypen + (currentdate.getDate()) + common.Sepretor.Hypen + currentdate.getFullYear() + common.Sepretor.Hypen + currentdate.getMinutes() + common.Sepretor.Hypen + currentdate.getSeconds() + common.FileType.PDF;

                self.userDetailModel.PDFFileName(fileName);
                self.saveData();
                
                var chkAdvisorContact = $('#chkAdvisorContact').prop('class').indexOf('active') > -1;
                var chkPDFDownload = $('#chkPDFDownload').prop('class').indexOf('active') > -1;
                var chkRegisterUser = $('#chkRegisterUser').prop('class').indexOf('active') > -1;
                var chkPromotional = $('#chkPromotional').prop('class').indexOf('active') > -1;

                // File attached and downloaded
                if (chkAdvisorContact == true && chkPDFDownload == true) {
                    $.ajax({
                        url: common.APIUrls.DownloadPDFAndAttachedPDF + fileName,
                        async: false,
                        success: function (data) {
                            self.CreateAnchorAndDownloadPDF(baseUrl + "Images/PDFFiles/" + fileName);
                            self.sendMail();
                            self.ErrorMessage(data);
                        }
                    });
                }

                // File attached and not downloaded
                if (chkAdvisorContact == true && chkPDFDownload == false) {
                    $.ajax({
                        url: common.APIUrls.PDFAttached + fileName,
                        async: false,
                        success: function (data) {
                            self.sendMail();
                            self.ErrorMessage(data);
                        }
                    });
                }

                // File not attached but downloaded
                if (chkAdvisorContact == false && chkPDFDownload == true) {

                    $.ajax({
                        url: common.APIUrls.DownloadPDF + fileName,
                        async: false,
                        success: function (data) {
                            self.CreateAnchorAndDownloadPDF(baseUrl + "Images/PDFFiles/" + fileName);
                            self.sendMail();
                            self.ErrorMessage(data);
                        }
                    });
                }

                // file not attached, not downloaded, only survey result attached
                if (chkAdvisorContact == false && chkPDFDownload == false) {
                    $.ajax({
                        url: common.APIUrls.DownloadUserSurveyResult + fileName,
                        async: false,
                        success: function (data) {
                            self.sendMail();
                        }
                    });

                }
            }
            //self.userDetailModel.EmailId('');
            //$("#EmailIdInput").val('');
        };

        //save data for addional info
        self.saveData = function () {
            var data = self.userDetailModel;
            if (data.Id() > 0) {
                self.ShowMessages();
            }
            else {
                data = JSON.stringify(ko.mapping.toJS(data));
                common.PostAJAXCall(common.APIUrls.SaveAdditionalInformation, data, self.saveDataSuccess, true);
            }
        };

        self.saveDataSuccess = function (data) {
            if (data == null || data.Id == 0) {
                self.ShowMessages();
            }
            else {
                $('#loginSuccessDiv').removeClass('hidden');
                $('#loginDiv').addClass('hidden');
            }
        };

        // Display messages after save additional informations
        self.ShowMessages = function () {
            $('#divThankYouMessage').css('display', '');
            $('#divAdditionalInfo').css('display', 'none');

            if (self.CurrentAction == 1) {
                $("#divThankYouMessage").html("<p>" + common.Messages[languageCode].ThankYouSecurityAdvisor + "</p>");
            }
            else if (self.CurrentAction == 2) {
                $("#divThankYouMessage").html("<p>" + common.Messages[languageCode].ThankYouPDF + "</p>");
            }
            else if (self.CurrentAction == 12) {
                $("#divThankYouMessage").html("<p>" + common.Messages[languageCode].ThankYouSecurityAdvisorAndPDF + "</p>");
            }
            else if (self.CurrentAction == 4) {
                $("#divThankYouMessage").html("<p>" + common.Messages[languageCode].ThankYouPromotional + "</p>");
            }
        };

        //get territory email on state selected
        self.GetTerritoriesEmail = function () {
            var stateId = $('#StateInput').val();
            if (parseInt(stateId) > 0) {
                if (stateId != 'Select a state') {
                    var url = common.APIUrls.GetTerritoryEmails.format([stateId]);
                    common.GetAJAXCall(url, {}, self.GetTerritoriesEmailSuccess);
                }
            }
        };

        //territory email Success method
        self.GetTerritoriesEmailSuccess = function (data) {
            var emails = [];
            $.each(data, function () {
                emails.push(this.EmailId);
            });
            self.userDetailModel.TerritoryEmails(emails);
            common.HideLoader();
        };

        //downlaod pdf action
        self.PDFAttached = function (fileName) {
            common.PostAJAXCall(common.APIUrls.PDFAttached + fileName, self.SuccessFileAttached, false);
        };

        self.SuccessFileAttached = function () {
        },


        //downlaod pdf action
        self.DownloadFile = function (URL) {
            if (window.innerWidth > 1024) {
                window.location = URL;
            } else {
                window.open(URL, '_blank');
            }
        };

        //downlaod pdf action
        self.DownloadPDF = function (fileName) {
            if (window.innerWidth > 1024) {
                window.location = common.APIUrls.DownloadPDF + fileName;
            } else {
                window.open(common.APIUrls.DownloadPDF + fileName, '_blank');
            }
        };


        //send mail to territory
        self.sendMail = function () {
            self.userDetailModel.StateName($('#StateInput :selected').text());
            var data = self.userDetailModel;
            data = JSON.stringify(ko.mapping.toJS(data));
            common.PostAJAXCall(common.APIUrls.SendMailAdvisor, data, self.sendMailSuccess);
        };

        self.sendMailSuccess = function (data) {

        };

        //check login validaion
        self.isValidLogin = function () {
            return self.userDetailModel.Password.isValid() &&
                self.userDetailModel.ConfirmPassword.isValid();
        };

        //check email id entered
        self.CheckUserEmail = function () {
            if (self.userDetailModel.EmailId() != "" || self.userDetailModel.EmailId() != null) {
                var url = common.APIUrls.CheckUserEmail.format([self.userDetailModel.EmailId()]);
                common.GetAJAXCall(url, {}, self.CheckUserEmailSuccess);
            }
        };
        //check email id success method
        self.CheckUserEmailSuccess = function (data) {
            if (data != null) {
                self.userDetailModel.EmailId('');
                common.AlertBox(common.Messages[languageCode].EmailExists);
            }
        };

        // Load once DOM created
        $(document).ready(function () {
            $('input.radio').change(function () {
                if ($(this).is(':checked')) {
                    $('input.radio').parent('span').removeClass('active');
                    $(this).parent('span').addClass('active');
                }
                else {
                    $(this).parent('span').removeClass('active');
                }
            });

            $('input.checkbox').change(function () {
                if ($(this).prop('checked')) {
                    $(this).parent('span').addClass('active');
                }
                else {
                    $(this).parent('span').removeClass('active');
                }
            });
            $("#ZipCodeInput").mask("99999-9999");
            $("#PhoneNumberInput").mask("(999)999-9999");
        });

        //set current menu to active
        self.setMenuActive = function () {
            $('#ulMenuItem li').removeClass('active');
            $('#liAdditionalInfo').addClass('active');
            $('#liSurvey a').addClass('link-disabled');
            $('#liRecommendation a').addClass('link-disabled');
        };

        self.SetPlaceHolder = function () {
            common.setPlaceHolderForIE9();
        };
    }

    var viewmodel = new AdditionalInfoViewModel();
    viewmodel.GetStatesDropdownlist();
    viewmodel.getAdditionalInfo();
    viewmodel.setMenuActive();
    ko.applyBindings(viewmodel, document.getElementById('divAditionalInfo'));
    viewmodel.SetPlaceHolder();
});
