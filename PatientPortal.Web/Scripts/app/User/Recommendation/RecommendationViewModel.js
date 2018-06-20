//************************************** Recommendation View Model : knockout view model **************************************//
/*jslint browser: true,for: true,this: true*/
/*global $,baseUrl,define,languageCode,window,location,ko,document,self,FormData*/

define(['../../Model/QuestionModel', 'yaleNexGrid', 'common', 'usermodel'], function (QuestionModel, PatientPortalGrid, Common, UserModel) {
    'use strict';
    function RecommendationViewModel() {
        var self = this;
        self.model = new QuestionModel();
        self.SurveyName = ko.observable();
        self.RecommendationList = ko.observableArray([]);
        self.RecommendedGrid = new PatientPortalGrid();
        var common = new Common();
        self.userModel = new UserModel();

        //function to show forget password panel
        self.ForgetPassword = function () {
            $("#divSignin").hide();
            $('#divForgetPassword').fadeIn();
            $('#divForgetPassword').show();
        };

        //send mail to user when forget password requested
        self.SendForgetPassword = function () {
            common.SendForgetPassword(self.userModel);

        };

        //back button to redirect to login page
        self.BackToLogin = function () {
            common.BackToLogin();
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

        //function to login a user
        self.LoginUser = function () {
            common.LoginUser(self.userModel);
        };

        // To get recommendation list
        self.GetRecommendationList = function () {
            common.GetAJAXCall(common.APIUrls.GetRecommendationList, {}, self.GetRecommendationListSuccess);
        };

        // get recommendation list success method
        self.GetRecommendationListSuccess = function (data) {
            if (data != null) {
                var uniqueItems = self.arrUniqueProduct(data);
                self.RecommendationList(uniqueItems);
                if (data.length > 0) {
                    self.SurveyName(data[0].SurveyName);
                    self.RecommendedGrid.DataRows([]);
                    var uniqueProduct = self.arrUnique(data);
                    self.RecommendedGrid.DataRows(uniqueProduct);
                }
                if (data.length < 3) {
                    $('#DivCustomScrollbar').removeClass('mCustomScrollbar custom-scroll-common');
                    $('#DivCustomScrollbar').addClass('recommend-list-item');
                } else {
                    $('#DivCustomScrollbar').removeClass('recommend-list-item');
                    $('#DivCustomScrollbar').addClass('mCustomScrollbar custom-scroll-common');
                }
            } else {
                $('#divRecommendedProduct').css('display', 'none');
                $('#divNoResultFound').css('display', '');
            }
            ko.applyBindings(self, document.getElementById('divLogin'));
            self.SetPlaceHolder();
            common.HideLoader();
        };

        //set placeholder in login page for IE9
        self.SetPlaceHolder = function () {
            common.setPlaceHolderForIE9();
        };

        // To call additional information page
        self.AdditionalInformation = function () {
            window.location = common.RedirctPage.AdditionalInformation;
        };

        self.isNotNumberSlider = function (number) {
            return isNaN(number);
        };

        self.GetProductConstraint = function (firstAnswer, ProductConstraint) {
            var firstNumber = 0;
            var products = firstAnswer.split(':');
            if (!isNaN(parseInt(products[1]))) {
                //firstNumber=parseInt(products[1]);
                ProductConstraint = firstAnswer + (parseInt(products[1]) > 1 ? ' units' : (products[1] == "" ? "" : ' unit')) + ";" + ProductConstraint;
            }
            else
                ProductConstraint = products[0] + ";" + ProductConstraint;

            var stringOne, stringTwo, stringThree = undefined
            stringOne = ProductConstraint.split(';');
            for (var i = 0; i < stringOne.length; i++) {
                stringTwo = stringOne[i].split(':');
                if (stringTwo[1] != undefined)
                    stringThree = stringTwo[1].replace('units', '');
                if (stringThree != undefined)
                    stringThree = stringThree.replace('unit', '');
                if (!isNaN(parseInt(stringThree))) {
                    firstNumber = firstNumber + parseInt(stringThree);
                }
            }
            ProductConstraint = ProductConstraint.replaceAll(';', '<br />')
            if (firstNumber != 0) {
                ProductConstraint = ProductConstraint + "<hr style='margin-top:0px;margin-bottom:0px'/><b>" + common.Messages[languageCode].Total + ":</b> " + firstNumber + " units";
            }
            return ProductConstraint;
        };

        String.prototype.replaceAll = function (target, replacement) {
            return this.split(target).join(replacement);
        };

        self.IsNumberMoreThanOne = function (RecommendedCount) {
            if (!isNaN(parseInt(RecommendedCount))) {
                if (parseInt(RecommendedCount) > 1)
                    return true;
            }
        };

        //get unique Item for Product
        self.arrUniqueProduct = function (arr) {
            var cleaned = [];
            var productConstraint = "";
            var productId;
            arr.forEach(function (itm) {
                var unique = true;
                cleaned.forEach(function (itm2) {
                    if (_.isEqual(itm.ProductId, itm2.ProductId)) {
                        unique = false;
                        productId = itm.ProductId;
                        productConstraint = itm.AnswerTitle;
                        if (!isNaN(parseInt(itm.AnswerTitle)))
                            productConstraint = "";
                        if (!isNaN(parseInt(itm.RecommendedCount)) && productConstraint != "")
                            productConstraint += ": " + (itm.RecommendedCount + (parseInt(itm.RecommendedCount) > 1 ? ' units' : (itm.RecommendedCount == "" ? "" : ' unit')));
                        else if (productConstraint == "")  //for Slider
                            productConstraint += (itm.RecommendedCount + (parseInt(itm.RecommendedCount) > 1 ? ' units' : (itm.RecommendedCount == "" ? "" : ' unit')));
                        if (itm2.DuplicateProductConstraint != "" && itm2.DuplicateProductConstraint != null)
                            productConstraint = productConstraint + ";" + itm2.DuplicateProductConstraint
                    }
                });
                if (!unique) {
                    cleaned.forEach(function (item) {
                        if (_.isEqual(item.ProductId, productId)) {
                            item.DuplicateProductConstraint = productConstraint;
                        }
                    });
                }
                if (unique)
                    cleaned.push(itm);
            });
            return cleaned;
        };

        //get unique array
        self.arrUnique = function (arr) {
            var cleaned = [];
            var totalQuantity = 0;
            var productId;
            arr.forEach(function (itm) {
                var unique = true;
                cleaned.forEach(function (itm2) {
                    if (_.isEqual(itm.ProductId, itm2.ProductId)) {
                        unique = false;
                        productId = itm.ProductId;
                        if (!isNaN(parseInt(itm.RecommendedCount)))
                            totalQuantity = parseInt(itm.RecommendedCount);
                        if (!isNaN(parseInt(itm2.RecommendedCount)))
                            totalQuantity += parseInt(itm2.RecommendedCount);
                    }
                });
                if (!unique) {
                    cleaned.forEach(function (item) {
                        if (_.isEqual(item.ProductId, productId)) {
                            item.RecommendedCount = totalQuantity;
                        }
                    });
                }
                if (unique)
                    cleaned.push(itm);
            });
            return cleaned;
        };

        //set recommendation page menu to active
        self.setMenuActive = function () {
            $('#ulMenuItem li').removeClass('active');
            $('#liRecommendation').addClass('active');
            $('#liSurvey a').addClass('link-disabled');
            $('#liAdditionalInfo a').addClass('link-disabled');
            //$('#liAdditionalInfo a').attr('href', common.RedirctPage.AdditionalInformation);
        };
    }
    var viewmodel = new RecommendationViewModel();
    viewmodel.GetRecommendationList();
    viewmodel.setMenuActive();
    ko.applyBindings(viewmodel, document.getElementById('divRecommendation'));
});

