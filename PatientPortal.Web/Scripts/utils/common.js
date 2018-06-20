//************************************** Common Model : knockout view model **************************************//
/*global $,baseUrl,define,languageCode,window,location,ko,document*/
define([''], function () {

    function Common() {
        var self = this;


        // Message box body message
        self.Messages = {
            "en-US": {
                Insert: "Record has been added successfully",
                InsertUser: "User has been added successfully",
                Delete: "Record has been deleted successfully",
                DeleteQuestionConfirm: "Are you sure you want to delete question #",
                DeleteProductConfirm: "Are you sure you want to delete the product",
                DeleteQuestion: "Are you sure you want to delete question #",
                DeleteAnswerConfirm: "Are you sure you want to delete the answer",
                DeleteProductImageConfirm: "Are you sure you want to delete the product's image",
                DeleteSurveyConfirm: "Are you sure you want to delete survey #",
                Update: "Record has been updated successfully",
                ConfirmMessage: "Are you sure you want to delete the record",
                Error: "Error! failed",
                InvalidUser: "Invalid user name or password",
                InvalidDrop: "Node can not be droped",
                InvalidDelete: "Node can not be deleted",
                NoDataInNode: "Please add at least one product",
                PasswordMailSent: "Please check your email, reset password link has been sent to your email-id",
                InvalidEmailId: "Please enter valid email-id",
                MinimumAnswer: "Please add at least one answer",
                ProductPrimaryImage: "Please select a image to make it primary for this product",
                MaximumNumberOfProductImage: "The maximum number of images upload for a product can not exceed over 6",
                ImageFileSize: "File size can not exceed over 500 KB",
                InvalidDragAndDrop: "Invalid drop location",
                AddProductPopupCancel: "Are you sure you want to cancel adding a new product?",
                AddQuestionPopupCancel: "Are you sure you want to cancel adding a new question?",
                AddSurveyPopupCancel: "Are you sure you want to cancel adding a new survey?",
                UpdateProductPopupCancel: "Are you sure you want to cancel update a new product?",
                UpdateQuestionPopupCancel: "Are you sure you want to cancel update a new question?",
                UpdateSurveyPopupCancel: "Are you sure you want to cancel update a new survey?",
                RequiredAnswer: "Please fill answer title",
                RequiredProductImage: "Please select product’s image",
                ImageType: "Please select a valid image",
                PasswordReset: "Password has been reset",
                QuestionMendatory: "This question is mandatory",
                EmailExists: "Email-Id already exists, please enter another email-Id or login to proceed",
                QuestionAlreadyMapped: "This question already mapped with a survey and can not be deleted",
                QuestionAlreadyMappedUpdated: "This question already mapped with a survey and can not be updated",
                SurveyAlreadyMapped: "This survey already filled and can not be deleted",
                NodeAlreadyMapped: "This survey already filled and the node can not be deleted",
                SurveyAlreadyMappedNotUpdated: "This survey already filled and can not be updated",
                AnswerQuestionAlreadyMapped: "The question of this answer is already mapped with a survey and can not be deleted",
                ProductAlreadyMapped: "This product already mapped with a survey and can not be deleted",
                ProductAlreadyMappedUpdated: "This product already mapped with a survey and can not be updated",
                ProductImagesAlreadyMapped: "The product of this image is already mapped with a survey and can not be deleted",
                ProductRequiredImages: "Please add atleast one image to this product",
                RequiredField: "This is a required field",
                InvalidURL: "This field must contain a valid URL",
                UserName: "Please Enter Your User Name",
                Password: "Please Enter Your Password",
                PasswordLength: "Password length must be greater than or equal to 8",
                RequiredEmailId: "Please Enter Valid email-id",
                ThankYouSecurityAdvisor: "Thank you. One of our multi-family housing security advisors will contact you.",
                ThankYouPDF: "Thank you. A PDF with user’s responses and recommendations is generated.",
                ThankYouSecurityAdvisorAndPDF: "Thank you. A PDF with user’s responses and recommendations is generated, One of our multi-family housing security advisors will contact you.",
                ThankYouPromotional: "Thank you for the feedback.",
                PasswordEqual: "Confirm password must be equal to Password",
                PhoneNumberLength: "Length of the Phone Number cannot exceed over 14",
                NumberMust: "This field must be a number",
                ValidZipCode: "Length of the Zip Code cannot exceed over 7",
                ConfirmPasswordEqual: "Confirm password must be equal to Password",
                CheckboxCheckedMessage: "Please select at least one option",
                DeleteAnswerToMakeNumberSlider: "Please delete all answers except one to make it number slider",
                SessionExpire: "Session has been expired. please login again",
                DuplicateProductName: "Copied product name can not be same, Please change product name",
                MoreText: "More",
                LessText: "Less",
                Total: "Total",
                ContactToAdvisor: "Failed to contact Advisor, Please refresh the page and try again",
                FailedToDownloadPDF: "Failed to download PDF summary of your survey, Please refresh the page and try again",
                FailedContactToAdvisorAndDownloadPDF: "Failed to contact Advisor and download PDF summary of your survey, Please refresh the page and try again"
            },
            "fr-FR": {
                UserName: "S'il vous plaît entrez votre nom d'utilisateur",
                RequiredEmailId: "S'il vous plaît Entrez Valable email-id",
                Password: "S'il vous plait entrez votre mot de passe",
                PasswordLength: "La longueur de mot de passe doit être supérieure ou égale à 8",
                PasswordEqual: "Confirmer mot de passe doit être égal à Mot de passe",
                Error: "Erreur! échoué",
                InvalidUser: "Nom d'utilisateur ou mot de passe invalide",
                RequiredField: "ce champ est requis",
                PasswordMailSent: "Por favor, consultar su correo electrónico, enlace de restablecimiento de contraseña ha sido enviada a su correo electrónico-id",
                InvalidEmailId: "S'il vous plaît entrer ID Email valide",
                ThankYouSecurityAdvisor: "Je vous remercie. Un de nos conseillers en sécurité de logements multifamiliaux vous contactera.",
                ThankYouPDF: "Je vous remercie. Un PDF avec les utilisateurs des réponses et recommandations est générée.",
                ThankYouSecurityAdvisorAndPDF: "Je vous remercie. Un PDF avec les utilisateurs des réponses et recommandations est généré, un de nos conseillers en sécurité de logements multifamiliaux vous contacteront.",
                ThankYouPromotional: "Merci pour votre retour.",
                QuestionMendatory: "Cette question est obligatoire",
                PhoneNumberLength: "Longueur du numéro de téléphone ne peut pas dépasser de plus de 14",
                NumberMust: "Ce champ doit être un nombre",
                ValidZipCode: "Longueur du code postal ne peut pas dépasser plus de 7",
                ConfirmPasswordEqual: "Confirmer mot de passe doit être égal à Mot de passe",
                EmailExists: "ID Email existe déjà, s'il vous plaît entrer un autre ID Email ou connectez-vous pour continuer",
                CheckboxCheckedMessage: "S'il vous plaît sélectionner au moins une option",
                SessionExpire: "Session a expiré. s'il vous plaît vous connecter à nouveau",
                MoreText: "Plus",
                LessText: "Moins",
                Total: "Total",
                ContactToAdvisor: "Impossible de communiquer avec le conseiller, S'il vous plaît rafraîchir la page et réessayer",
                FailedToDownloadPDF: "Impossible de télécharger résumé PDF de votre enquête, S'il vous plaît rafraîchir la page et réessayer",
                FailedContactToAdvisorAndDownloadPDF: "Échec Contact Conseiller et télécharger Résumé PDF de votre enquête, S'il vous plaît rafraîchir la page et réessayer"
            },
            "es-ES": {
                UserName: "Por favor, ingrese su nombre de usuario",
                RequiredEmailId: "Por favor, Introduzca Válido email-id",
                Password: "Por favor, introduzca su contraseña",
                PasswordLength: "Longitud de la contraseña debe ser mayor que o igual a 8",
                PasswordEqual: "Confirmar contraseña debe ser igual a la contraseña",
                Error: "¡Error! Falló",
                InvalidUser: "usuario o contraseña invalido",
                RequiredField: "este es un campo requerido",
                PasswordMailSent: "S'il vous plaît vérifier votre e-mail, un lien de réinitialisation de mot a été envoyé à votre adresse e-id",
                InvalidEmailId: "Por favor, introduzca email-id válidas",
                ThankYouSecurityAdvisor: "Gracias. Uno de nuestros asesores de seguridad de viviendas multifamiliares se comunicará con usted.",
                ThankYouPDF: "Gracias. Se genera un PDF con los usuarios respuestas y recomendaciones.",
                ThankYouSecurityAdvisorAndPDF: "Gracias. Se genera un PDF con los usuarios respuestas y recomendaciones, Uno de nuestros asesores de seguridad de viviendas multifamiliares se comunicará con usted.",
                ThankYouPromotional: "Gracias por los comentarios.",
                QuestionMendatory: "Esta pregunta es obligatoria",
                PhoneNumberLength: "Longitud del número de teléfono no puede exceder más de 14",
                NumberMust: "Este campo debe ser un número",
                ValidZipCode: "Longitud del código postal no puede exceder más de 7",
                ConfirmPasswordEqual: "Confirmar contraseña debe ser igual a la contraseña",
                EmailExists: "Correo-Id ya existe, por favor escribe otro email-Id o login para proceder",
                CheckboxCheckedMessage: "Por favor, seleccione al menos una opción",
                SessionExpire: "Sesión ha expirado. Entra en la cuenta de nuevo",
                MoreText: "Más",
                LessText: "Menos",
                Total: "Total",
                ContactToAdvisor: "No se ha podido contactar Asesor, por favor, actualice la página y vuelva a intentarlo",
                FailedToDownloadPDF: "No se ha podido descargar PDF resumen de la encuesta, por favor, actualice la página y vuelva a intentarlo",
                FailedContactToAdvisorAndDownloadPDF: "No se ha podido contactar con Asesor y descarga PDF Resumen de la encuesta, por favor, actualice la página y vuelva a intentarlo"
            }
        };

        // Message box header message
        self.HeaderMessages = {
            ConfirmDelete: "Confirm Delete",
            AlertSuccess: "Alert Success",
            AlertUpdate: "Alert Update"
        };

        //Question Type in Survey 
        self.SurveyQuestionType = {
            SingleSelect: "Single-Select",
            MultiSelect: "Multi-Select",
            TextBox: "TextBox",
            TextArea: "TextArea",
            NumberSlider: "Number-Slider"
        };

        // Message box button text
        self.ButtonText = {
            "en-US": {
                OK: "OK",
                Candel: "Cancel"
            },
            "fr-FR": {
                OK: "D'accord",
                Candel: "Annuler"
            },
            "es-ES": {
                OK: "Okay",
                Candel: "Cancelar"
            }
        };

        // File Type
        self.FileType = {
            PDF: ".pdf"
        };

        // Sepretor
        self.Sepretor = {
            Colon: ":",
            SemiColon: ";",
            ForwardSlash: "/",
            Hypen : "-"
        };

        // Alert Box
        self.AlertBox = function (bodyMessage, url) {
            $.alert({
                title: "",
                content: bodyMessage,
                confirmButton: self.ButtonText[languageCode].OK,
                backgroundDismiss: false,
                confirm: function () {
                    self.HideLoader();
                    if (url) {
                        window.location = url;
                    } else {
                        return true;
                    }
                }
            });
        };

        // Alert Box with refresh the page
        self.AlertBoxRefreshPage = function (bodyMessage) {
            $.alert({
                title: "",
                content: bodyMessage,
                confirmButton: self.ButtonText[languageCode].OK,
                backgroundDismiss: false,
                confirm: function () {
                    location.reload();
                }
            });
        };

        // Type of Textinput type for question
        self.TextInputType = {
            Number: "1",
            AlphaNumeric: "2"
        };

        // Type of Textinput type for question
        self.QuestionType = {
            Single_Select: "1",
            Multi_Select: "2",
            TextBox: "3",
            TextArea: "4",
            Number_Slider: "5"
        };

        // Role Type Array
        self.RoleType = {
            Admin: "Admin",
            SurveyUser: "SurveyUser"
        };

        // Page path
        self.RedirctPage = {
            Login: baseUrl + "Admin/User",
            Question: baseUrl + 'question/',
            Survey: baseUrl + 'survey/',
            Product: baseUrl + 'product/',
            SurveyUser: baseUrl + 'surveyuser/',
            PastSurvey: baseUrl + 'pastsurvey/',
            Recommendation: baseUrl + 'recommendation/',
            AdditionalInformation: baseUrl + 'additionalInformation/'
        };

        // Api URLs
        self.APIUrls = {
            SaveProductImage: baseUrl + 'api/ProductApi/SaveProductImage',
            PostPutProduct: baseUrl + 'api/ProductApi/',
            GetProduct: baseUrl + 'api/ProductApi/',
            GetProductById: baseUrl + 'api/ProductApi/Get/',
            DeleteProduct: baseUrl + 'api/ProductApi/Delete/',
            GetLanguage: baseUrl + 'api/LanguageApi',
            PostQuestion: baseUrl + 'api/QuestionApi/POST',
            PostAnswer: baseUrl + 'api/QuestionApi/SaveAnswerImage',
            GetQuestionType: baseUrl + 'api/QuestionTypeApi',
            GetTextInputType: baseUrl + 'api/TextInputTypeApi',
            GetQuestion: baseUrl + 'api/QuestionApi',
            PutQuestion: baseUrl + 'api/QuestionApi',
            GetQuestionById: baseUrl + 'api/QuestionApi/Get/',
            DeleteQuestion: baseUrl + 'api/QuestionApi/Delete/',
            GetSurvey: baseUrl + 'api/SurveyApi/',
            GetSurveyQuestionList: baseUrl + 'api/QuestionApi/GetSurveyQuestionList/',
            DeleteSurvey: baseUrl + 'api/SurveyApi/Delete/',
            PostSurvey: baseUrl + 'api/SurveyApi/',
            PutSurvey: baseUrl + 'api/SurveyApi/',
            GetSurveyById: baseUrl + 'api/SurveyApi/Get/',
            PostUser: baseUrl + 'api/UserApi/',
            PutUser: baseUrl + 'api/UserApi/',
            PostForgetPassword: baseUrl + 'api/UserApi/',
            //survey user
            SaveSurveyAttempt: baseUrl + 'api/SurveyUserApi/SaveSurveyAttempt',
            UserSurveyGetSurveyQuestionList: baseUrl + 'api/SurveyUserApi/GetSurveyQuestionList/{0}',
            //recommendation
            GetRecommendationList: baseUrl + 'api/RecommendationApi/GetRecommendationList',
            //additional info
            GetAdditionalInformation: baseUrl + 'api/AdditionalInfoApi/GetAdditionalInformation',
            SaveAdditionalInformation: baseUrl + 'api/AdditionalInfoApi/SaveAdditionalInformation',
            CheckUserEmail: baseUrl + 'api/AdditionalInfoApi/CheckUserEmail?emailId={0}',
            //past survey
            GetPastSurveyList: baseUrl + 'api/PastSurveyApi/GetPastSurveyList',
            GetPastSurveyQuestionList: baseUrl + 'api/PastSurveyApi/GetPastSurveyQuestionList/{0}',
            GetRecommendationListByGuid: baseUrl + 'api/PastSurveyApi/GetRecommendationListByGuid?Guid={0}',
            //state and territory emails
            GetStateList: baseUrl + 'api/StateApi/GetStateList',
            GetTerritoryEmails: baseUrl + 'api/TerritoryEmailApi/GetTerritoryEmails?StateId={0}',
            UserSurveyGetIntroductionByLanguageId: baseUrl + 'api/IntroductionApi/GetIntroductionByLanguageId/{0}',
            DownloadPDF: baseUrl + 'SurveyUser/DownloadPDF/',
            PDFAttached: baseUrl + 'SurveyUser/PDFAttached/',
            DownloadPDFAndAttachedPDF: baseUrl + 'SurveyUser/DownloadPDFAndAttachedPDF/',
            DownloadPDFPastSurvey: baseUrl + 'SurveyUser/DownloadPDFPastSurvey/',
            SendMailAdvisor: baseUrl + 'api/AdditionalInfoApi/SendMailAdvisor',
            DownloadUserSurveyResult: baseUrl + 'SurveyUser/DownloadUserSurveyResult/',
        };
        // Api URLs
        self.ImagePath = {
            IntroductionImage: baseUrl + 'Contents/images/'
        }

        // Return a unique guid
        self.GenerateGUID = function () {
            var d = new Date().getTime();
            var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                var r = (d + Math.random() * 16) % 16 | 0;
                d = Math.floor(d / 16);
                return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
            });
            return uuid;
        };

        // Show loader
        self.ShowLoader = function () {
            if ($("#divLoader").hasClass("hidden")) {
                $("#divLoader").removeClass("hidden");
            }
        };

        // Hide loader
        self.HideLoader = function () {
            if (!$("#divLoader").hasClass("hidden")) {
                $("#divLoader").addClass("hidden");
            }
        };

        //Set text length
        self.SetTextLength = function (text) {
            var textValue = text;
            if (text.length > 20) {
                textValue = text.substring(0, 20) + '...';
            }
            return textValue;
        };

        // Functionly of login
        self.LoginUser = function (userModel) {
            userModel.ConfirmPassword(userModel.Password());
            userModel.ValidationEnabledForget(false);
            userModel.ValidationEnabledConfirmPassword(false);
            userModel.ValidationEnabledSignIn(true);
            if (userModel.isValid()) {
                var userDTO = JSON.stringify(ko.mapping.toJS(userModel));
                $.ajax({
                    url: baseUrl + 'api/UserApi/',
                    data: userDTO,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        self.LoginSuccess(data);
                    },
                    error: function () {

                        self.DisplayErrorMessage(errorThrown);
                    }
                });
            } else {
                self.HideLoader();
                userModel.Errors.showAllMessages();
            }
        };

        // Login success
        self.LoginSuccess = function (data) {
            if (data.ErrorCode > 0) {

                self.DisplayErrorMessage(errorThrown);
                return false;
            }

            if (data.Id > 0) {
                if (data.RoleDTO.Type === self.RoleType.SurveyUser) {
                    location.reload();
                } else {
                    self.AlertBox(self.Messages[languageCode].InvalidUser);
                }
            } else {
                self.AlertBox(self.Messages[languageCode].InvalidUser);
            }
        };

        // Send mail for forget password event
        self.SendForgetPassword = function (userModel) {
            userModel.ValidationEnabledSignIn(false);
            userModel.ValidationEnabledForget(true);
            if (userModel.isValid()) {
                userModel.IsForgetPassword = 1;
                userModel.UserName = userModel.ForgetPasswordUserName();
                var userDTO = JSON.stringify(ko.mapping.toJS(userModel));
                $.ajax({
                    url: baseUrl + 'api/UserApi/',
                    data: userDTO,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.Id > 0) {
                            self.AlertBox(self.Messages[languageCode].PasswordMailSent);
                        } else {
                            self.AlertBox(self.Messages[languageCode].InvalidEmailId);
                        }
                    },
                    error: function () {

                        self.DisplayErrorMessage(errorThrown);
                    }
                });
            } else {
                self.HideLoader();
                userModel.Errors.showAllMessages();
                return;
            }
        };

        // Back to login DIV event
        self.BackToLogin = function () {
            $("#divSignin").show();
            $('#divSignin').fadeIn();
            $('#divForgetPassword').hide();
        };

        // Client error type
        self.ClientSideErrorType = {
            Unauthorized: "Unauthorized",
            InternalError: "Internal Server Error"
        };

        // Get Error message on based of error type
        self.DisplayErrorMessage = function (errorThrown) {

            if (errorThrown === self.ClientSideErrorType.Unauthorized) {
                self.AlertBoxRefreshPage(self.Messages[languageCode].SessionExpire);

            }
            else if (errorThrown === self.ClientSideErrorType.InternalError) {
                self.AlertBox(self.Messages[languageCode].Error);
            }
            else {
                self.AlertBox(self.Messages[languageCode].Error);
            }
        },

        // AJAX call for GET request
        self.GetAJAXCall = function (url, parameters, successCallback, isAsync) {
            if (isAsync == null || isAsync == undefined)
                isAsync = true;
            $.ajax({
                url: url,
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: isAsync,
                success: successCallback,
                error: function (xhr, textStatus, errorThrown) {
                    //self.DisplayErrorMessage(errorThrown);
                }
            });
        };

        // AJAX call for POST request
        self.PostAJAXCall = function (url, parameters, successCallback, isAsync) {
            if (isAsync == null || isAsync == undefined) {
                isAsync = true;
            }
            var isSuccess = true;
            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: parameters,
                async: isAsync,
                success: successCallback,
                error: function (xhr, textStatus, errorThrown) {
                    isSuccess = false;
                    self.DisplayErrorMessage(errorThrown);
                }
            });
            return isSuccess;
        };

        // AJAX call for PUT request
        self.PutAJAXCall = function (url, parameters, successCallback, isAsync) {
            if (isAsync == null || isAsync == undefined) {
                isAsync = true;
            }
            var isSuccess = true;
            $.ajax({
                url: url,
                type: 'PUT',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: parameters,
                async: isAsync,
                success: successCallback,
                error: function (xhr, textStatus, errorThrown) {
                    isSuccess = false;
                    self.DisplayErrorMessage(errorThrown);
                }
            });
            return isSuccess;
        };

        // AJAX call for Delete request
        self.DeleteAJAXCall = function (url, successCallback) {
            var isSuccess = true;
            $.ajax({
                url: url,
                type: 'DELETE',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: successCallback,
                error: function (xhr, textStatus, errorThrown) {
                    isSuccess = false;
                    self.DisplayErrorMessage(errorThrown);
                }
            });
            return isSuccess;

        };

        // Extention function for string format
        String.prototype.format = function (args) {
            var str = this;
            return str.replace(String.prototype.format.regex, function (item) {
                var intVal = parseInt(item.substring(1, item.length - 1));
                var replace;
                if (intVal >= 0) {
                    replace = args[intVal];
                } else if (intVal === -1) {
                    replace = "{";
                } else if (intVal === -2) {
                    replace = "}";
                } else {
                    replace = "";
                }
                return replace;
            });
        };
        String.prototype.format.regex = new RegExp("{-?[0-9]+}", "g");

        self.setPlaceHolderForIE9 = function () {
            var pos = window.navigator.userAgent.indexOf("MSIE");
            if (pos > 0) {
                if (window.navigator.userAgent.substring(pos + 5, window.navigator.userAgent.indexOf(".", pos)) < 10) {
                    $("input[placeholder]").each(function () {
                        $(this).val($(this).attr("placeholder"));
                    });

                    $("input[placeholder]").click(function () {
                        if ($(this).val() === $(this).attr("placeholder")) {
                            $(this).val('');
                        }
                    });

                    $('input[placeholder]').blur(function () {

                        if ($.trim($(this).val()).length === 0) {
                            $(this).val($(this).attr("placeholder"));
                        }
                    });
                }
            }
        };

        // Check file extention and size
        $(document).ready(function () {

            $('input[type=file]').change(function (e) {
                var currentFileId, file;
                currentFileId = $(e.target).attr("id");
                file = document.getElementById(currentFileId).files[0];
                if (file !== null && file.size > 0) {
                    if ((Math.round((file.size / 1024) * 100) / 100) > 500) {
                        $("#" + currentFileId).val("");
                        self.AlertBox(self.Messages[languageCode].ImageFileSize);
                    }
                    if (file.name.substring(file.name.length - 4) !== '.png' && file.name.substring(file.name.length - 4) !== '.jpeg' && file.name.substring(file.name.length - 4) !== '.PNG' && file.name.substring(file.name.length - 4) !== '.JPEG' && file.name.substring(file.name.length - 4) !== '.jpg' && file.name.substring(file.name.length - 4) !== '.JPG') {
                        $("#" + currentFileId).val("");
                        self.AlertBox(self.Messages[languageCode].ImageType);
                    }
                }
            });

            // To prevent click on back button of browser
            if (window.location.pathname.indexOf("DownloadPdf") == -1 || window.location.pathname.indexOf("PageNotFound") == -1) {
                window.history.forward(-1);
            }

            // to add place holder on textbox when browser is IE 9
            
        });
    }
    return Common;
});