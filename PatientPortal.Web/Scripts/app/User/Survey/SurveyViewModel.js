//************************************** Survey View Model : knockout view model **************************************//
/*jslint browser: true,for: true,this: true*/
/*global $,baseUrl,define,languageCode,window,location,ko,document,self,FormData*/
define(['questionmodel', 'common', 'usermodel'], function (QuestionModel, Common, UserModel) {
    'use strict';
    function SurveyViewModel() {
        var self = this, common;
        common = new Common();
        self.model = new QuestionModel();
        self.FilledQuestAnswerId = ko.observableArray();
        self.QuestionList = ko.observableArray([]);
        self.Question = ko.observableArray([]);
        self.SurveyQuestionIdList = ko.observableArray();
        self.userModel = new UserModel();
        self.index = ko.observable(-1);
        self.QuestAnswerArray = [];

        //show forget password model
        self.ForgetPassword = function () {
            $("#divSignin").hide();
            $('#divForgetPassword').fadeIn();
            $('#divForgetPassword').show();
        };

        //send mail to user raised for forget password
        self.SendForgetPassword = function () {
            common.SendForgetPassword(self.userModel);
        };

        //back to login model from forget password window
        self.BackToLogin = function () {
            common.BackToLogin();
        };

        //login user and reload to current windwo
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

        //Get Survey Question List by Survey Id
        self.GetQuestionList = function () {
            var url, surveyId;
            surveyId = $('#lblSurveyIdLanguageId').html();
            url = common.APIUrls.UserSurveyGetSurveyQuestionList.format([surveyId]);
            common.GetAJAXCall(url, {}, self.GetQuestionListSuccess);
        };

        //Survey Question List success callback function
        self.GetQuestionListSuccess = function (data) {
            if (data.length > 0 && data[0].ISurveyDTO.IsActive == true) {
                self.QuestionList(data);
                self.Question(self.QuestionList());
                ko.applyBindings(self, document.getElementById('divLogin'));
                self.SetPlaceHolder();
                if ($('#spanIsLogin').text() == 'false') {
                    $('#modelLogin').modal('show');
                }

                if (data[0].IQuestionDTO.IsMandatary == false || data[0].IQuestionDTO.IQuestionTypeDTO.Type == 'Number-Slider') {
                    self.setNextButtonColor(true);
                }
               
                //set slider configuration
                self.NumberSliderConfig();

                //resize clousal when answer option is more than 4
                self.resizeCrousalInneronAnswer();
            } else {
                $('#divSurveyActive').addClass('hidden');
                $('#divSurveyInActive').removeClass('hidden');
            }
            common.HideLoader();
        };

        self.setNextButtonColor = function (flag) {
            if (flag) {
                $('.glyphicon.glyphicon-menu-right').css('color', '#ffcc00');
            }
            else {
                $('.glyphicon.glyphicon-menu-right').css('color', '#000');
            }
        };

        self.resizeCrousalInneronAnswer = function () {
            var newClass, optionDIV;
            for (var i = 0; i < self.QuestionList().length; i++) {
                if (self.QuestionList()[i].IQuestionDTO.IAnswerDTOList.length > 4) {
                    $('div[id*=QuestionSlider]').filter('[surveyQuestId=' + self.QuestionList()[i].Id + ']').find('div').filter("[data-selector='DivAnswers']").each(function () {
                        newClass = $(this).attr('class').replaceAll('3', '2');
                        $(this).attr('class', '"col-md-2 col-sm-2 col-xs-12"');
                        optionDIV = $(this).find(".option");
                        optionDIV.css('margin-bottom', '50px');
                        optionDIV.css('padding-bottom', '90px');
                        optionDIV.find('.prod-detail').css('top', '200px');
                        optionDIV.find('.prod-img').css('padding', '10px');
                        optionDIV.find('.prod-img').css('height', '140px');
                        if (optionDIV.find('.prod-detail').find('#SpanTextBox').text().length > 30) {
                            optionDIV.find('.prod-detail').find('.marginL15').css('display', '');
                        }
                        else {
                            optionDIV.find('.prod-detail').find('.marginL15').css('display', 'none');
                        }
                    });
                }
            }
        };

        //set placeholder in login page for IE9
        self.SetPlaceHolder = function () {
            common.setPlaceHolderForIE9();
        };

        //Range slider configuration method
        self.NumberSliderConfig = function () {
            $("div[id*=master]").each(function () {
                var minValue = parseInt($(this).find('span').filter(".pull-left").text());
                var maxValue = parseInt($(this).find('span').filter(".pull-right").text());
                var sliderElement = $(this).slider({
                    orientation: "horizontal",
                    range: "min",
                    animate: true,
                    value: 0,
                    min: minValue,
                    max: maxValue,
                    step: 1,
                    slide: function (event, ui) {
                        $(this).find('span').filter(".ui-slider-handle").text(ui.value)
                    }
                });
                $(this).find('span').filter(".ui-slider-handle").text(minValue);

                // Increase the value of number slider
                var incrementElement = sliderElement.closest(".rangeSlider").find('a').filter('.increaseval');
                $(incrementElement).click(function () {
                    var sliderValue = parseInt(sliderElement.find('span').filter(".ui-slider-handle").text());
                    var maxValue = sliderElement.slider("option", "max");
                    if ((sliderValue + 1) <= maxValue) {
                        sliderElement.slider('value', sliderElement.slider('value') + sliderElement.slider("option", "step"));
                        sliderElement.find('span').filter(".ui-slider-handle").text(sliderValue + 1);
                    }
                });

                // Decrease the value of number slider
                var decreaseElement = sliderElement.closest(".rangeSlider").find('a').filter('.decreseval');
                $(decreaseElement).click(function () {
                    var sliderValue = parseInt(sliderElement.find('span').closest('.ui-slider-handle').text());
                    var minValue = sliderElement.slider("option", "min");
                    if (sliderValue >= (minValue + 1)) {
                        sliderElement.slider('value', sliderElement.slider('value') - sliderElement.slider("option", "step"));
                        sliderElement.find('span').closest(".ui-slider-handle").text(sliderValue - 1);
                    }
                });
            });
        };

        // Get introduction by language Id method
        self.GetIntroductionByLanguageId = function () {
            var id, url;
            id = $('#lblSurveyIdLanguageId').html();
            url = common.APIUrls.UserSurveyGetIntroductionByLanguageId.format([id]);
            common.GetAJAXCall(url, {}, self.GetIntroductionByLanguageIdSuccess);
        };

        //Get introduction by language Id Seccess callback method
        self.GetIntroductionByLanguageIdSuccess = function (data) {
            if (data != null) {
                $("#pIntroductionDescription").html(data.Description);
                //$("#imgIntroduction").attr("src", (common.ImagePath.IntroductionImage + data.ImagePath));
            }
            common.HideLoader();
        };

        //set previous question
        self.PreviousQuestion = function () {
            self.enableNextButton();
            self.FilledQuestAnswerId().pop();
            self.SurveyQuestionIdList.pop();
            var indexOfPreQuestion = self.SurveyQuestionIdList()[self.index()];
            self.setActiveTab(indexOfPreQuestion);
            var i, questionId = self.getQuestionIdByMapId(indexOfPreQuestion);

            for (i = 0; i < self.QuestAnswerArray.length; i++) {
                if (self.QuestAnswerArray[i].QuestionId == questionId) {
                    self.QuestAnswerArray.splice(i, 1);
                }
            }
            self.index(self.index() - 1);
            if (self.index() == -1) {
                self.FilledQuestAnswerId().pop();
                self.SurveyQuestionIdList.pop();
            }
        };

        //set next question
        self.NextQuestion = function () {
            var currentQuestionId, childQuestionId = null, indexOfNextQuestion = null, currentQuestionType, currentQuestionAnswer;
            var divCurrentQuestion = self.getActiveSlider();
            //var divAnswers = divCurrentQuestion.filter("[data-selector='DivAnswers'");
            currentQuestionType = divCurrentQuestion.find('span').filter("[data-selector='QuestionType']").text();
            currentQuestionId = divCurrentQuestion.find('span').filter("[data-selector='QuestionId']").text();
            self.setAnswerSelected(currentQuestionType);
            if (self.IsMendatory(currentQuestionId)) {
                return;
            }

            self.InsertFirstQuestionToArray();

            currentQuestionAnswer = self.QuestAnswerArray.pop();

            self.fillQuestionAnswerId(currentQuestionAnswer);

            childQuestionId = self.GetChildQuestionIdForCurrentAnswer(currentQuestionAnswer);

            indexOfNextQuestion = self.GetSurveyQuestionMapIdforCurrentAnswer(childQuestionId);

            if (indexOfNextQuestion != null) {
                self.ShowNextQuestion(indexOfNextQuestion);
            }
            else if (self.QuestAnswerArray.length > 0) {
                self.ShowNextQuestionRecursive(self.QuestAnswerArray);
            } else {
                self.showLastQuestion();
            }
        };

        //get questionId by surveyQuestionMapId
        self.getQuestionIdByMapId = function (surveyQuestionMapId) {
            for (var i = 0; i < self.QuestionList().length; i++) {
                if (self.QuestionList()[i].Id == surveyQuestionMapId) {
                    return self.QuestionList()[i].QuestionId;
                }
            }
        }

        //enable next button in slider when move previous from last question
        self.enableNextButton = function () {
            var isLastSlide = self.getActiveSlider().attr('surveyQuestId');
            if (isLastSlide == "finalSubmit") {
                $('#RightButton').removeClass('disabledAnchor');
                $('#RightSpan').css('color', '#000');
            }
        };

        //get current slider
        self.getActiveSlider = function () {
            return $('div[id*=QuestionSlider]').filter('.active');
        };

        //show final slide
        self.showLastQuestion = function () {
            self.setActiveTab('finalSubmit');
            self.index(self.index() + 1);
            self.setProgressBar(100);
            $('#RightButton').addClass('disabledAnchor');
            $('#RightSpan').css('color', '#babfc0');
            self.SurveyQuestionIdList.push('finalSubmit');
        };

        //set Active tab
        self.setActiveTab = function (Id) {
            var indexSurvey = null, divSlider;
            $('div[id*=QuestionSlider]').each(function (index) {
                divSlider = $(this);
                if (divSlider.attr('surveyQuestId') == Id) {
                    indexSurvey = index;
                    return false;
                }
            });
            if (indexSurvey != null) {
                if (Id != 'finalSubmit') {
                    var isMandatory = self.Question()[indexSurvey].IQuestionDTO.IsMandatary;
                    if (isMandatory == false) {
                        self.setNextButtonColor(true);
                    }
                    else {
                        self.setNextButtonColor(false);
                        self.checkAnswerSelected(divSlider);
                    }
                    if (self.Question()[indexSurvey].IQuestionDTO.IQuestionTypeDTO.Type == 'Number-Slider') {
                        self.setNextButtonColor(true);
                    }
                }

                $("#SurveyCarousel").carousel(indexSurvey);
            }
        };

        //total question count
        self.totalQuestionCount = ko.computed(function () {
            return arrUnique(self.QuestionList()).length;
        }, self);

        //push item to question answer array
        self.PushItemQuestionAnswer = function (array) {
            var i, item, index, length = array.length;
            for (i = 0; i < length; i++) {
                item = array.pop();
                index = self.lookupQestionAnswer(self.QuestAnswerArray, item.QuestionId, item.AnswerId);
                if (index == -1) {
                    self.QuestAnswerArray.push({
                        QuestionId: item.QuestionId,
                        AnswerId: item.AnswerId,
                        Value: item.Value
                    });
                } else {
                    self.QuestAnswerArray[index] = { QuestionId: item.QuestionId, AnswerId: item.AnswerId, Value: item.Value };
                }
            }
        };

        //get answer selected and fill to array
        self.setAnswerSelected = function (questionType) {
            if (questionType == common.SurveyQuestionType.SingleSelect) {
                self.RadioSelected();
            } else if (questionType == common.SurveyQuestionType.MultiSelect) {
                self.CheckboxSelected();
            } else if (questionType == common.SurveyQuestionType.TextBox) {
                self.TextboxEntered();
            } else if (questionType == common.SurveyQuestionType.TextArea) {
                self.TextAreaEntered();
            } else if (questionType == common.SurveyQuestionType.NumberSlider) {
                self.NumberSliderSelected();
            }
        };

        //check answer is filled or now
        self.checkAnswerSelected = function (divSlider) {
            var questionType = divSlider.find('span').filter("[data-selector='QuestionType']").text();
            if (questionType == common.SurveyQuestionType.SingleSelect) {
                self.checkRadioSelected(divSlider);
            } else if (questionType == common.SurveyQuestionType.MultiSelect) {
                self.checkCheckboxSelected(divSlider);
            }
            else if (questionType == common.SurveyQuestionType.TextBox) {
                self.checkTextboxEntered(divSlider);
            } else if (questionType == common.SurveyQuestionType.TextArea) {
                self.checkTextAreaEntered(divSlider);
            }
            //else if (questionType == common.SurveyQuestionType.NumberSlider) {
            //    self.NumberSliderSelected();
            //}
        };

        //check radio selected and change color of next button
        self.checkRadioSelected = function (divSlider) {
            divSlider.find('input').filter('[name=SingleSelect]').each(function () {
                if ($(this).parent('span').attr('class').indexOf('active') > -1) {
                    self.setNextButtonColor(true);
                    return false;
                }
            });
        };

        //checkbox selected and change color of next button
        self.checkCheckboxSelected = function (divSlider) {
            divSlider.find('input').filter('[name=MultiSelect]').each(function () {
                if ($(this).parent('span').attr('class').indexOf('active') > -1) {
                    self.setNextButtonColor(true);
                    return false;
                }
            });
        };

        //check textbox entered and change color of next button
        self.checkTextboxEntered = function (divSlider) {
            divSlider.find('input').filter('[id=TextboxAnswer]').each(function () {
                if ($(this).val().trim().length > 0) {
                    self.setNextButtonColor(true);
                    return false;
                }
            });
        };

        //check textarea entered and change color of next button
        self.checkTextAreaEntered = function (divSlider) {
            divSlider.find('input').filter('[id=TextArea]').each(function () {
                if ($(this).val().trim().length > 0) {
                    self.setNextButtonColor(true);
                    return false;
                }
            });
        };

        //radio selected
        self.RadioSelected = function () {
            var questionId, answerId, arrayAnswer, divCurrentQuestion;
            divCurrentQuestion = self.getActiveSlider();
            questionId = self.getQuestionId();
            arrayAnswer = [];
            divCurrentQuestion.find('input').filter('[name=SingleSelect]').each(function () {
                if ($(this).parent('span').attr('class').indexOf('active') > -1) {
                    answerId = $(this).val();
                    arrayAnswer.push({ QuestionId: questionId, AnswerId: answerId, Value: '' });
                    return false;
                }
            });
            self.PushItemQuestionAnswer(arrayAnswer);
        };

        //checkbox selected
        self.CheckboxSelected = function () {
            var questionId, answerId, arrayAnswer, divCurrentQuestion;
            divCurrentQuestion = self.getActiveSlider();
            questionId = self.getQuestionId();
            arrayAnswer = [];
            divCurrentQuestion.find('input').filter('[name=MultiSelect]').each(function () {
                if ($(this).parent('span').attr('class').indexOf('active') > -1) {
                    answerId = $(this).val();
                    arrayAnswer.push({ QuestionId: questionId, AnswerId: answerId, Value: '' });
                }
            });
            self.PushItemQuestionAnswer(arrayAnswer);
        };

        //textbox entered
        self.TextboxEntered = function () {
            var divCurrentQuestion, questionId, answerId, arrayAnswer;
            divCurrentQuestion = self.getActiveSlider();
            questionId = self.getQuestionId();
            arrayAnswer = [];
            divCurrentQuestion.find('input').filter('[id=TextboxAnswer]').each(function () {
                if ($(this).val().trim().length > 0) {
                    answerId = $(this).attr('answerId');
                    arrayAnswer.push({ QuestionId: questionId, AnswerId: answerId, Value: $(this).val() });
                }
            });
            self.PushItemQuestionAnswer(arrayAnswer);
        };

        //textarea entered
        self.TextAreaEntered = function () {
            var questionId, answerId, arrayAnswer, divCurrentQuestion;
            divCurrentQuestion = self.getActiveSlider();
            questionId = self.getQuestionId();
            arrayAnswer = [];
            divCurrentQuestion.find('textarea').filter('[id=TextArea]').each(function () {
                if ($(this).val().trim().length > 0) {
                    answerId = $(this).attr('answerId');
                    arrayAnswer.push({ QuestionId: questionId, AnswerId: answerId, Value: $(this).val() });
                }
            });
            self.PushItemQuestionAnswer(arrayAnswer);
        };

        //number slider selected
        self.NumberSliderSelected = function () {
            var questionId, answerId, arrayAnswer, divCurrentQuestion, value
            divCurrentQuestion = self.getActiveSlider();
            questionId = self.getQuestionId();
            answerId = divCurrentQuestion.find('div').filter('[id=master]').attr('answerId');
            arrayAnswer = [];
            value = divCurrentQuestion.find('div').filter('[id=master]').find('span').filter('.ui-slider-handle').text();
            arrayAnswer.push({ QuestionId: questionId, AnswerId: answerId, Value: value });
            self.PushItemQuestionAnswer(arrayAnswer);
        };

        //Get Current Question Id
        self.getQuestionId = function () {
            var questionId = self.getActiveSlider().find('span').filter("[data-selector='QuestionId']").text();
            return questionId;
        };

        //check question mandatory and return true when not answered
        self.IsMendatory = function (currentQuestionId) {
            var index, isMandatoryQuestion = self.getActiveSlider().find('span').filter("[data-selector='QuestionMandatory']").text();
            if (isMandatoryQuestion == 'true') {
                index = self.lookup(self.QuestAnswerArray, currentQuestionId);
                if (index == -1) {
                    common.AlertBox(common.Messages[languageCode].QuestionMendatory);
                    return true;
                }
            }
            else {
                var questionId, answerId, arrayAnswer;
                index = self.lookup(self.QuestAnswerArray, currentQuestionId);
                if (index == -1) {
                    questionId = self.getQuestionId();
                    arrayAnswer = [];
                    answerId = null;
                    arrayAnswer.push({ QuestionId: questionId, AnswerId: answerId, Value: '' });
                    self.PushItemQuestionAnswer(arrayAnswer);
                }
                return false;
            }
        };

        //insert item to survey list when first question occure.
        self.InsertFirstQuestionToArray = function () {
            if (self.index() == -1) {
                var activeDIV = self.getActiveSlider();
                self.SurveyQuestionIdList.push(activeDIV.attr('surveyquestid'));
            }
        };

        //fill question answer and value to array
        self.fillQuestionAnswerId = function (currentQuestionAnswer) {
            if (self.lookupQestionAnswer(self.FilledQuestAnswerId(), currentQuestionAnswer.QuestionId, currentQuestionAnswer.AnswerId) == -1) {
                self.FilledQuestAnswerId.push({ QuestionId: currentQuestionAnswer.QuestionId, AnswerId: currentQuestionAnswer.AnswerId, Value: currentQuestionAnswer.Value });
            }
        };

        //Get child question Id
        self.GetChildQuestionIdForCurrentAnswer = function (questAnswer) {
            var i, childQuestionId = null;
            if (questAnswer != undefined) {
                for (i = 0; i < self.QuestionList().length; i++) {
                    if (self.QuestionList()[i].AnswerId == questAnswer.AnswerId && self.QuestionList()[i].QuestionId == questAnswer.QuestionId) {
                        childQuestionId = self.QuestionList()[i].ChildQuestionId;
                        break;
                    }
                }
            }
            return childQuestionId;
        };

        //get survey question map Id for current answer and question
        self.GetSurveyQuestionMapIdforCurrentAnswer = function (childQuestionId) {
            var i, indexofQuestion, indexOfNextQuestion = null;
            if (childQuestionId == 0 || childQuestionId == null) {
                for (i = 0; i < self.QuestionList().length; i++) {
                    indexofQuestion = self.lookupQuestion(self.FilledQuestAnswerId(), self.QuestionList()[i].QuestionId);
                    if (indexofQuestion == -1 && (self.QuestionList()[i].SurveyQuestionMapId == 0 || self.QuestionList()[i].SurveyQuestionMapId == null)) {
                        indexOfNextQuestion = self.QuestionList()[i].Id;
                        break;
                    }
                }
            } else {
                for (i = 0; i < self.QuestionList().length; i++) {
                    if (self.QuestionList()[i].QuestionId == childQuestionId) {
                        indexOfNextQuestion = self.QuestionList()[i].Id;
                        break;
                    }
                }
            }
            return indexOfNextQuestion;
        };

        //Show Next Question to show
        self.ShowNextQuestion = function (indexOfNextQuestion) {
            self.setActiveTab(indexOfNextQuestion);
            self.index(self.index() + 1);
            self.setProgressBar();
            self.SurveyQuestionIdList.push(indexOfNextQuestion);
        };

        //set progress bar
        self.setProgressBar = function (value) {
            var percentage = null;
            if (value != undefined || value != null) {
                percentage = value;
            } else {
                var total = self.totalQuestionCount();
                var questAttempt = self.index() + 1;
                percentage = Math.floor((questAttempt / total) * 100);
            }
            if ($('#SurveyProgressBar').html() != "100%") {
                $('#SurveyProgressBar').css('width', percentage + '%');
                $('#SurveyProgressBar').html(percentage + '%');
            }
        };

        //show next question in recursive
        self.ShowNextQuestionRecursive = function () {
            if (self.QuestAnswerArray.length > 0) {
                var currentQuestionAnswer, childQuestionId, indexOfNextQuestion;
                currentQuestionAnswer = self.QuestAnswerArray.pop();
                self.fillQuestionAnswerId(currentQuestionAnswer);
                childQuestionId = self.GetChildQuestionIdForCurrentAnswer(currentQuestionAnswer);
                indexOfNextQuestion = self.GetSurveyQuestionMapIdforCurrentAnswer(childQuestionId);
                if (indexOfNextQuestion != null) {
                    self.ShowNextQuestion(indexOfNextQuestion);
                }
                else {
                    self.ShowNextQuestionRecursive();
                }
            }
            else {
                self.showLastQuestion();
            }
        };

        //set Radio selected
        self.SetRadioSelected = function (data, target) {
            var radio = target.currentTarget;
            if (radio.checked) {
                $('input[name*=SingleSelect]').filter('[questionid=' + data.QuestionId + ']').parent('span').removeClass('active');
                radio.parentElement.className = radio.parentElement.className + ' active';
            } else {
                radio.parentElement.className = radio.parentElement.className.replace('active', '');
            }
            self.setNextButtonColor(true);
        };

        //set checkbox selected
        self.SetCheckBoxSelected = function (data, target) {
            var checkbox = target.currentTarget;
            if (checkbox.checked) {
                checkbox.parentElement.className = checkbox.parentElement.className + ' active';

            } else {
                checkbox.parentElement.className = checkbox.parentElement.className.replace('active', '');
            }
            var isMandatory = self.getActiveSlider().find('span').filter("[data-selector='QuestionMandatory']").text();
            var ischecked = false;
            if (isMandatory == 'true') {
                self.getActiveSlider().find('input[name*=MultiSelect]').each(function () {
                    if ($(this).parent().attr('class').indexOf('active') > -1) {
                        ischecked = true
                        return false;
                    }
                });
                if (ischecked) {
                    self.setNextButtonColor(true);
                }
                else {
                    self.setNextButtonColor(false);
                }
            }
        };

        //set TextBox Input for validation
        self.setTextBoxInputNextButton = function (data, target) {
            var isMandatory = self.getActiveSlider().find('span').filter("[data-selector='QuestionMandatory']").text();
            var ischecked = false;
            if (isMandatory == 'true') {
                self.getActiveSlider().find('input[id*=TextboxAnswer]').each(function () {
                    if ($(this).val().trim().length > 0) {
                        ischecked = true
                        return false;
                    }

                });
                if (ischecked) {
                    self.setNextButtonColor(true);
                }
                else {
                    self.setNextButtonColor(false);
                }
            }
        };

        //set TextBox Input for validation
        self.setTextAreaInputNextButton = function (data, target) {
            var isMandatory = self.getActiveSlider().find('span').filter("[data-selector='QuestionMandatory']").text();
            var ischecked = false;
            if (isMandatory == 'true') {
                self.getActiveSlider().find('input[id*=TextArea]').each(function () {
                    if ($(this).val().trim().length > 0) {
                        ischecked = true
                        return false;
                    }

                });
                if (ischecked) {
                    self.setNextButtonColor(true);
                }
                else {
                    self.setNextButtonColor(false);
                }
            }
        };

        //set TextBox Input for validation
        self.setTextBoxInput = function (data, target) {
            var textInputTypeId = self.getActiveSlider().find('span').filter("[data-selector='TextInputTypeId']").text();
            var isNumberValidate = false;
            if (textInputTypeId == 1) {
                self.getActiveSlider().find('[id*=TextboxAnswer]').each(function () {
                    if (isNaN(this.value)) {
                        isNumberValidate = true;
                        return false;
                    } else {
                        isNumberValidate = false;
                    }
                });
                self.setErrorOnNumberValidation(isNumberValidate);
            }
        };

        //set TextArea for validation
        self.setTextAreaInput = function (data, target) {
            var textInputTypeId = self.getActiveSlider().find('span').filter("[data-selector='TextInputTypeId']").text();
            var isNumberValidate = false;
            if (textInputTypeId == 1) {
                self.getActiveSlider().find('[id*=TextArea]').each(function () {
                    if (isNaN(this.value)) {
                        isNumberValidate = true;
                        return false;
                    } else {
                        isNumberValidate = false;
                    }
                });
                self.setErrorOnNumberValidation(isNumberValidate);
            }
        };

        //final submit the Answer and Question selected by user
        self.SubmitSurvey = function () {
            var i, k, SurveyQuestionAnswer = [], matchedLists = [];
            for (i = 0; i < self.FilledQuestAnswerId().length; i++) {
                //if (self.lookupQuestion(self.QuestionIdList(), self.FilledQuestAnswerId[i].key) > -1) {
                matchedLists = getListByQuestionIdAnswerId(self.Question(), self.FilledQuestAnswerId()[i].QuestionId, self.FilledQuestAnswerId()[i].AnswerId);
                for (k = 0; k < matchedLists.length; k++) {
                    SurveyQuestionAnswer.push({ SurveyId: self.Question()[0].SurveyId, QuestionId: self.FilledQuestAnswerId()[i].QuestionId, AnswerId: self.FilledQuestAnswerId()[i].AnswerId, TextInput: self.FilledQuestAnswerId()[i].Value, ProductId: matchedLists[k].ProductId });
                }
                //}
            }
            var list = JSON.stringify(ko.mapping.toJS(SurveyQuestionAnswer));
            common.PostAJAXCall(common.APIUrls.SaveSurveyAttempt, list, self.SubmitSurveySuccess);
        };

        //submit answer success method
        self.SubmitSurveySuccess = function (data) {
            var recommendationURL = baseUrl + "recommendation/";
            window.location = recommendationURL;
        };

        //Show/hide error message when invalid value is entered
        self.setErrorOnNumberValidation = function (isNumberValidate) {
            if (isNumberValidate == true) {
                $('#RightButton').addClass('disabledAnchor');
                $('#RightSpan').css('color', '#babfc0');
                $('.error').removeClass('hidden');
                $('#LeftButton').addClass('disabledAnchor');
                $('#LeftSpan').css('color', '#babfc0');
                return;
            } else {
                $('#RightButton').removeClass('disabledAnchor');
                $('#RightSpan').css('color', '#000');
                $('#LeftButton').removeClass('disabledAnchor');
                $('#LeftSpan').css('color', '#000');
                $('.error').addClass('hidden');
            }
        };

        self.ShowText = function (title, answerId) {
            var optionDIV, textLength = 55;
            for (var i = 0; i < self.QuestionList().length; i++) {
                if (self.QuestionList()[i].AnswerId == answerId) {
                    if (self.QuestionList()[i].IQuestionDTO.IAnswerDTOList.length > 4) {
                        textLength = 30;
                    }
                    break;
                }
            }

            if (title.length > textLength)
                return title.substring(0, textLength) + "...";
            else
                return title;
        };
        self.ShowMoreText = function (data) {
            var optionDIV, title, textLength = 55;
            var divCurrentQuestion = self.getActiveSlider();
            var anchor = divCurrentQuestion.find('a').filter('[answerid=' + data.Id + ']');
            if (anchor.text() == common.Messages[languageCode].MoreText) {
                divCurrentQuestion.find('span').filter('[answerid=' + data.Id + ']').text(data.Title);
                anchor.text(common.Messages[languageCode].LessText);
            }
            else {
                for (var i = 0; i < self.QuestionList().length; i++) {
                    if (self.QuestionList()[i].AnswerId == data.Id) {
                        $('div[id*=QuestionSlider]').filter('[surveyQuestId=' + self.QuestionList()[i].Id + ']').find('div').filter("[data-selector='DivAnswers']").each(function () {
                            optionDIV = $(this).find(".option");
                            if (optionDIV.css('margin-bottom') == "50px") {
                                textLength = 30;
                            }
                        });
                        break;
                    }
                }
                if (data.Title.length > textLength)
                    title = data.Title.substring(0, textLength) + "...";
                else
                    title = data.Title;
                divCurrentQuestion.find('span').filter('[answerid=' + data.Id + ']').text(title);
                anchor.text(common.Messages[languageCode].MoreText);
            }
        };

        $(document).ready(function () {
            //introduction panel click function to toggle the panel
            $('.popover-detail').hide();
            $(".small-popover").mouseenter(function () {
                $('.popover-detail').slideToggle();
            });

            $(".small-popover").mouseleave(function () {
                $('.popover-detail').slideToggle();
            });
        });
        //set current menu active
        self.setMenuActive = function () {
            $('#ulMenuItem li').removeClass('active');
            $('#liSurvey').addClass('active');
            $('#liRecommendation a').addClass('link-disabled');
            $('#liAdditionalInfo a').addClass('link-disabled');
        };

        //check name in object array, if exists then return index   =====
        self.lookup = function (arr, name) {
            var i, index = -1, len = arr.length;
            for (i = 0; i < len; i++) {
                if (arr[i].QuestionId == name) {
                    index = i;
                    break;
                }
            }
            return index;
        };

        //return index of question and answer in array  =====
        self.lookupQestionAnswer = function (arr, question, answer) {
            var i, index = -1, len = arr.length;
            for (i = 0; i < len; i++) {
                if (arr[i].QuestionId == question && arr[i].AnswerId == answer) {
                    index = i;
                }
            }
            return index;
        };

        //return index of name in 1D array  =====
        self.lookupQuestion = function (arr, name) {
            var i, index = -1, len = arr.length;
            for (i = 0; i < len; i++) {
                if (arr[i].QuestionId == name) {
                    index = i;
                }
            }
            return index;
        };

        //remove duplicate record from array =====
        function arrUnique(arr) {
            var unique, cleaned = [];
            arr.forEach(function (itm) {
                unique = true;
                cleaned.forEach(function (itm2) {
                    if (_.isEqual(itm.QuestionId, itm2.QuestionId)) {
                        unique = false;
                    }
                });
                if (unique) {
                    cleaned.push(itm);
                }
            });
            return cleaned;
        };

        function getListByQuestionIdAnswerId(arr, questionId, answerId) {
            var unique, cleaned = [];
            arr.forEach(function (itm) {
                if (_.isEqual(itm.QuestionId, parseInt(questionId)) && _.isEqual(itm.AnswerId, parseInt(answerId))) {
                    cleaned.push(itm);
                }
            });
            return cleaned;
        };



        String.prototype.replaceAll = function (target, replacement) {
            return this.split(target).join(replacement);
        };

    }

    var viewmodel = new SurveyViewModel();
    viewmodel.GetQuestionList();
    viewmodel.setMenuActive();
    viewmodel.GetIntroductionByLanguageId();
    ko.applyBindings(viewmodel, document.getElementById('divSurvey'));
});

