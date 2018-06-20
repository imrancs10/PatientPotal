//**************************************Past Survey View Model : knockout view model **************************************//
define(['../../Model/QuestionModel', '../../../utils/PatientPortalGrid', 'common'], function (QuestionModel, PatientPortalGrid, Common) {
    function PastSurveyViewModel() {
        var self = this;
        self.PastSurveyGrid = new PatientPortalGrid();
        var common = new Common();
        self.RecommendationList = ko.observableArray([]);
        self.Question = ko.observableArray([]);

        //get past survey list for the current user
        self.PastSurveysList = function () {
            common.GetAJAXCall(common.APIUrls.GetPastSurveyList, {}, self.PastSurveysListSuccess);
        }

        //past survey list success method
        self.PastSurveysListSuccess = function (data) {
            if (data != null && data.length > 0) {
                var uniqueStandards = self.arrUniqueSurvey(data);
                self.PastSurveyGrid.DataRows(uniqueStandards);
            }
            else {
                $('#lblNoRecord').css('display', '');
            }
            common.HideLoader();
        };

        //get survey attempt/filled data
        self.GetSurveyAttempt = function (data) {
            common.ShowLoader();
            $('#divSurveyDetail').css('display', '');
            $('#divSurveyList').css('display', 'none');
            var guid = data.Guid;
            var url = common.APIUrls.GetPastSurveyQuestionList.format([guid]);
            common.GetAJAXCall(url, {}, self.GetSurveyAttemptSuccess);
        }

        //survey attempt success method
        self.GetSurveyAttemptSuccess = function (data) {
            var uniqueStandards = self.arrUnique(data);
            self.Question(uniqueStandards);
            self.GetRecommendationList(uniqueStandards[0].Guid);
            //set slider configuration
            self.NumberSliderConfig();
            //resize clousal when answer option is more than 4
            self.resizeCrousalInneronAnswer();
        };

        //configuration for Number slider
        self.NumberSliderConfig = function () {
            $("div[id*=master]").each(function () {
                var minValue = parseInt($(this).find('span').filter(".pull-left").text());
                var maxValue = parseInt($(this).find('span').filter(".pull-right").text());
                $(this).slider({
                    orientation: "horizontal",
                    range: "min",
                    animate: true,
                    value: $(this).find('span').filter("#spanSliderText").text(),
                    min: minValue,
                    max: maxValue,
                    slide: function (event, ui) {
                        return false;
                    }
                });
                $(this).find('span').filter(".ui-slider-handle").text($(this).find('span').filter("#spanSliderText").text())
            });
        };

        self.resizeCrousalInneronAnswer = function () {
            var newClass, optionDIV;
            for (var i = 0; i < self.Question().length; i++) {
                if (self.Question()[i].IQuestionDTO.IAnswerDTOList.length > 4) {
                    $('div[id*=QuestionSlider]').filter('[surveyQuestId=' + self.Question()[i].Id + ']').find('div').filter("[data-selector='DivAnswers']").each(function () {
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

        // To get recommendation list
        self.GetRecommendationList = function (Guid) {
            var url = common.APIUrls.GetRecommendationListByGuid.format([Guid]);
            common.GetAJAXCall(url, {}, self.GetRecommendationListSuccess);
        };

        //recommendation list success method
        self.GetRecommendationListSuccess = function (data) {
            if (data != null) {
                var uniqueItems = self.arrUniqueProduct(data);
                self.RecommendationList(uniqueItems);
                if (data.length > 0) {
                    $('#divRecommendedProduct').css('display', '');
                    $('#divNoResultFound').css('display', 'none');
                }
                else {
                    $('#divRecommendedProduct').css('display', 'none');
                    $('#divNoResultFound').css('display', '');
                }
            }
            common.HideLoader();
        };

        //Set Link Survey
        self.SurveyLink = function (data) {
            var surveyLink = common.RedirctPage.SurveyUser + data.EncryptSurveyIdLanguageId;
            window.location = surveyLink;
        };

        //downlaod recommendation recap pdf
        self.DownloadPDF = function (data) {
            var url = common.APIUrls.GetRecommendationListByGuid.format([data.Guid]);
            common.GetAJAXCall(url, {}, self.DownloadPDFSuccess);
        };

        //downlaod recommendation recap pdf success method
        self.DownloadPDFSuccess = function (data) {
            if (window.innerWidth > 1024) {
                window.location = common.APIUrls.DownloadPDFPastSurvey;
            } else {
                window.open(common.APIUrls.DownloadPDFPastSurvey, '_blank');
            }
        };

        //back to past survey list
        self.BackToSuveyList = function () {
            $('#divSurveyDetail').css('display', 'none');
            $('#divSurveyList').css('display', '');
            $('[id*=QuestionSlider]').each(function () {
                $(this).removeClass('active');
            });
        };

        //set current menu to active 
        self.setMenuActive = function () {
            $('#ulMenuItem li').removeClass('active');
            $('#liPastSurvey').addClass('active');
            $('#liRecommendation a').addClass('link-disabled');
            $('#liSurvey a').addClass('link-disabled');
            $('#liAdditionalInfo a').addClass('link-disabled');
            //$('#liRecommendation a').attr('href', common.RedirctPage.Recommendation);
        };

        //Get Answer value from answerIds csv
        self.getAnswerValue = function (textInputs, answerIds, id) {
            if (textInputs.toString().indexOf(',') > -1) {
                var index = 0;
                if (answerIds.toString().indexOf(',') > -1) {
                    var answers = answerIds.split(",");
                    for (var i = 0; i < answers.length; i++) {
                        if (answers[i] == id)
                            index = i;
                    }
                }
                var texts = textInputs.split(",");
                return texts[index];
            }
            else {
                return textInputs;
            }
        };

        //check for answerId match
        self.isMatchedAnswer = function (answerIds, id) {
            var isMatch = false;
            if (answerIds != null && answerIds.toString().indexOf(',') > -1) {
                var answers = answerIds.split(",");
                answers.forEach(function (item) {
                    if (item == id)
                        isMatch = true;
                });
            }
            else {
                if (answerIds == id)
                    isMatch = true;
            }

            return isMatch;
        };

        //get unique array
        self.arrUnique = function (arr) {
            var cleaned = [];
            var ansIds;
            var ansvalues;
            var questId;
            arr.forEach(function (itm) {
                var unique = true;
                cleaned.forEach(function (itm2) {
                    if (_.isEqual(itm.QuestionId, itm2.QuestionId)) {
                        unique = false;
                        questId = itm.QuestionId;
                        ansIds = itm.AnswerId;
                        ansvalues = itm.TextInput;
                        ansIds = ansIds + ',' + itm2.AnswerId;
                        if (ansvalues != "")
                            ansvalues = ansvalues + ',' + itm2.TextInput;
                    }
                });
                if (!unique) {
                    cleaned.forEach(function (item) {
                        if (_.isEqual(item.QuestionId, questId)) {
                            item.AnswerId = ansIds;
                            item.TextInput = ansvalues;
                        }
                    });
                }
                if (unique)
                    cleaned.push(itm);
            });
            return cleaned;
        };

        self.ShowText = function (title, answerId) {
            var optionDIV, textLength = 55;
            for (var i = 0; i < self.Question().length; i++) {
                if (self.Question()[i].AnswerId == answerId) {
                    if (self.Question()[i].IQuestionDTO.IAnswerDTOList.length > 4) {
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
            var divCurrentQuestion = $('div[id*=QuestionSlider]').filter('.active');
            var anchor = divCurrentQuestion.find('a').filter('[answerid=' + data.Id + ']');
            if (anchor.text() == common.Messages[languageCode].MoreText) {
                divCurrentQuestion.find('span').filter('[answerid=' + data.Id + ']').text(data.Title);
                anchor.text(common.Messages[languageCode].LessText);
            }
            else {
                for (var i = 0; i < self.Question().length; i++) {
                    if (self.Question()[i].AnswerId == data.Id) {
                        $('div[id*=QuestionSlider]').filter('[surveyQuestId=' + self.Question()[i].Id + ']').find('div').filter("[data-selector='DivAnswers']").each(function () {
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

        //get unique suvey
        self.arrUniqueSurvey = function (arr) {
            var cleaned = [];
            arr.forEach(function (itm) {
                var unique = true;
                cleaned.forEach(function (itm2) {
                    if (_.isEqual(itm.SurveyTakenDateDisplay, itm2.SurveyTakenDateDisplay)) {
                        unique = false;
                    }
                });
                if (unique)
                    cleaned.push(itm);
            });
            return cleaned;
        };
    }

    var viewmodel = new PastSurveyViewModel();
    viewmodel.PastSurveysList();
    viewmodel.setMenuActive();
    ko.applyBindings(viewmodel, document.getElementById('divPastSurvey'));
});
