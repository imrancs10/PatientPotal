//************************************** Question View Model : knockout view model **************************************//
/*jslint browser: true, for: true, this : true*/
/*global $,baseUrl,define,languageCode,window,location,ko,document,self,FormData */
define(['questionmodel', 'answermodel', 'yaleNexGrid', 'common'], function (QuestionModel, AnswerModel, PatientPortalGrid, Common) {
    'use strict';
    function QuestionViewModel() {
        var self = this, clonedTrId = 0, common;
        self.QuestionTypeList = ko.observableArray([]);
        self.TextInputTypeList = ko.observableArray([]);
        self.LanguageList = ko.observableArray([]);
        self.QuestionGrid = new PatientPortalGrid();
        self.AnswerGrid = new PatientPortalGrid();
        self.model = new QuestionModel();
        self.model.AnswerDTO = new AnswerModel();
        common = new Common();

        // Insert questions and answers
        self.InsertQuestion = function (data) {
            common.ShowLoader();
            var formData, questionDTO;
            formData = self.BindAllAnswer();
            questionDTO = JSON.stringify(ko.mapping.toJS(data.model));
            if (!self.SetValidation()) {
                return;
            }
            var isSuccess = false;
            isSuccess = common.PostAJAXCall(common.APIUrls.PostQuestion, questionDTO, self.InsertSuccess, true);
            if (isSuccess) {
                self.SaveAnswerImages(formData);
            }
        };

        // Insert Success
        self.InsertSuccess = function (data) {
            if (data["<ErrorCode>k__BackingField"] > 0) {
                common.AlertBox(common.Messages[languageCode].Error);
            } else {
                common.AlertBoxRefreshPage(common.Messages[languageCode].Insert);
            }
        };

        // Save images
        self.SaveAnswerImages = function (data) {
            $.ajax({
                type: "POST",
                url: common.APIUrls.PostAnswer,
                data: data,
                dataType: 'json',
                contentType: false,
                processData: false,
                error: function () {
                    common.AlertBox(common.Messages[languageCode].Error);
                }
            });
        };

        // Set Validation on Save
        self.SetValidation = function () {
            common.HideLoader();
            var isValid = true;
            self.model.ValidationEnabled(true);
            if (!self.model.isValid()) {
                self.model.Errors.showAllMessages();
                isValid = false;
            } else {
                if (self.model.AnswerDTOList().length <= 0) {
                    self.model.AnswerDTO.ValidationEnabled(true);
                    if (!self.model.AnswerDTO.isValid()) {
                        self.model.AnswerDTO.Errors.showAllMessages();
                        isValid = false;
                    }
                }

                if (self.model.AnswerDTOList().length <= 0) {
                    common.AlertBox(common.Messages[languageCode].MinimumAnswer);
                    isValid = false;
                }
            }
            return isValid;
        };

        // Enable / Disable Dropdown box
        self.EnableDisableControl = function (isTrue) {
            $("#ddlLanguage").attr("disabled", isTrue);
            $("#ddlTextInputType").attr("disabled", isTrue);
            $("#ddlQuestionType").attr("disabled", isTrue);
        },
        // Get QuestionTypeList to fill dropdown box
        self.GetQuestionTypeList = function () {
            $("#btnUpdate").hide();
            common.GetAJAXCall(common.APIUrls.GetQuestionType, {}, self.GetQuestionTypeListSuccess, true);
        };

        // Get GetQuestionTypeList Success event
        self.GetQuestionTypeListSuccess = function (data) {
            var array = [];
            $.each(data, function (index, value) {
                array.push(value);
            });
            self.QuestionTypeList(array);
        },

        // Get TextInputTypeList to fill dropdown box
        self.GetTextInputTypeList = function () {
            common.GetAJAXCall(common.APIUrls.GetTextInputType, {}, self.GetTextInputTypeSuccess, true);
        };

        // Get GetTextInputType Success event
        self.GetTextInputTypeSuccess = function (data) {
            var array = [];
            $.each(data, function (index, value) {
                array.push(value);
            });
            self.TextInputTypeList(array);
        },

        //Function to Load Data using WEB API
        self.LoadData = function () {
            $("#liQuestions").addClass("active");
            common.GetAJAXCall(common.APIUrls.GetQuestion, {}, self.LoadDataSuccess, true);
        };

        // Get LoadData Success event
        self.LoadDataSuccess = function (data) {
            self.QuestionGrid.DataRows([]);
            self.QuestionGrid.DataRows(data);
            self.SortingPaging();
            common.HideLoader();
        },

        // Update question and answer
        self.UpdateQuestion = function () {
            common.ShowLoader();
            var formData, questionDTO;
            formData = self.BindAllAnswer();
            questionDTO = JSON.stringify(ko.mapping.toJS(self.model));
            if (!self.SetValidation()) {
                return;
            }
            var isSuccess = false;
            isSuccess = common.PutAJAXCall(common.APIUrls.PutQuestion, questionDTO, self.UpdateSuccess, true);
            if (isSuccess) {
                self.SaveAnswerImages(formData);
            }
        };

        // Update Success
        self.UpdateSuccess = function (data) {
            if (data["<ErrorCode>k__BackingField"] > 0) {
                common.AlertBox(common.Messages[languageCode].Error);
            } else {
                common.AlertBoxRefreshPage(common.Messages[languageCode].Update);
            }
        };

        // Get question by id
        self.GetQuestionById = function (data) {
            common.ShowLoader();
            $("#btnUpdate").show();
            $("#btnSave").hide();
            common.GetAJAXCall(common.APIUrls.GetQuestionById + data.Id, {}, self.GetQuestionByIdSuccess, true);
        };

        // Get GetQuestionById Success event
        self.GetQuestionByIdSuccess = function (data) {
            common.HideLoader();
            self.RemoveScrollBar();
            clonedTrId = 0;
            self.model.SetData(data);
            if (data.AnswerDTOList.length > 0) {
                self.AnswerGrid.DataRows(data.AnswerDTOList);
                self.model.AnswerDTOList(data.AnswerDTOList);
                self.BindAnswerTableFromDB();
                $("#myModalLabel").text("Edit Question");
                self.AddScrollBar(data);    // Add Scroll bar
            } else {
                self.AddUpdateAnswer();
            }
            self.EnableDisableControl(true);
            self.TextInputTypeChange();
            self.QuestionTypeChange();
            //if (self.model.IsMapped()) {
            //    $("input[type=checkbox]").addClass("disabled");
            //} else {
            //    $("input[type=checkbox]").removeClass("disabled");
            //}
        },

        // Add scroll bar
        self.AddScrollBar = function (data) {
            if (data.AnswerDTOList.length > 3) {
                if (!$("#divScroll").hasClass("scroll")) {
                    $("#divScroll").addClass("scroll");
                }
            }
        };

        // Remove scroll bar
        self.RemoveScrollBar = function () {
                if ($("#divScroll").hasClass("scroll")) {
                    $("#divScroll").removeClass("scroll");
            }
        };

        // Delete a question with its nested child answer table records
        self.DeleteQuestion = function (data, model) {
            common.ShowLoader();
            var currentRow, row;
            currentRow = $('#tblQuestionList [id^="' + model.Id + '"]');
            row = $('#tblQuestionList').DataTable().row(currentRow[0]);

            if (data.IsMapped) {
                common.AlertBox(common.Messages[languageCode].QuestionAlreadyMapped);
                return;
            }

            $.confirm({
                title: "",
                content: common.Messages[languageCode].DeleteQuestionConfirm + (row[0][0] + 1),
                confirmButton: common.ButtonText[languageCode].OK,
                cancelButton: common.ButtonText[languageCode].Cancel,
                backgroundDismiss: false,
                confirm: function () {
                    common.HideLoader();
                    var isSuccess = false;
                    isSuccess = common.DeleteAJAXCall(common.APIUrls.DeleteQuestion + data.Id, self.DeleteSuccess);
                    if (isSuccess) {
                        row.remove().draw(false);
                    }
                },
                cancel: function () {
                    common.HideLoader();
                },
            });
        };

        // Delete Success
        self.DeleteSuccess = function (data) {
            common.HideLoader();
            if (data["<ErrorCode>k__BackingField"] > 0) {
                common.AlertBox(common.Messages[languageCode].Error);
            } else {
                self.model.ClearData();

            }
        };

        // Open a new page to add question and answers
        self.AddQuestion = function () {
            self.model.ValidationEnabled(false);
            self.model.AnswerDTO.ValidationEnabled(false);
            self.model.ClearData();
            self.AnswerGrid.DataRows([]);
            self.model.AnswerDTOList([]);
            $("#btnSave").show();
            $("#btnUpdate").hide();
            //$('#myModal').modal('show');
            $("#myModalLabel").text("Add Question");
            self.EnableDisableControl(false);
            $('#tblAnswer tr:not(:first)').remove();
            self.AddUpdateAnswer();
            self.RemoveScrollBar();
        };

        // Bind answer table from DB
        self.BindAnswerTableFromDB = function () {
            $('#tblAnswer tr:not(:first)').remove();
            if (self.model.AnswerDTOList().length > 0) {
                $.each(self.model.AnswerDTOList().reverse(), function (index, answerDTO) {
                    var newId, $tr, $clone;
                    newId = clonedTrId++;
                    $tr = $("#answerTemplate");
                    $clone = $tr.clone(true);
                    $($clone[0]).removeAttr("style").removeAttr("id");
                    $tr.after($clone[0]);
                    $($clone[0]).attr("data-selector", newId).attr("id", answerDTO.Id);
                    $($($($clone[0]).find("td")[1]).find(":file")[0]).attr("id", newId).addClass("answerImage");

                    $clone.find('.answerTitle').val(answerDTO.Title);
                    $clone.find('.answerTooltip').val(answerDTO.ToolTip);
                    $clone.find('.imagepath').val(answerDTO.ImagePath);
                    if (answerDTO !== null && answerDTO.ImagePath !== null && answerDTO.ImagePath.trim().length > 0) {
                        $($($($($clone[0]).find("td")[1]).find(":file")[0])[0]).addClass("hidden").removeClass("answerImage");
                        $($($($($clone[0]).find("td")[1]).find(":text")[0])[0]).addClass("hidden");
                        $($($($clone[0]).find("td")[1]).find("img")[0]).removeClass("hidden").attr("src", answerDTO.ImagePath);

                    }
                });
            }
        };

        // Add/Update answer
        self.AddUpdateAnswer = function () {
            var getFirstRow, newId, $tr, $clone;
            getFirstRow = $('#tblAnswer tr').eq(1);
            if ($($($(getFirstRow).find("td")[0]).find(":text")[0]).val() === "") {
                common.AlertBox(common.Messages[languageCode].RequiredAnswer);
                return;
            }

            if ($('#tblAnswer tr').length > 3) {
                if (!$("#divScroll").hasClass("scroll")) {
                    $("#divScroll").addClass("scroll");
                }
            }

            newId = clonedTrId++;
            $tr = $("#answerTemplate");
            $clone = $tr.clone(true);
            $($clone[0]).removeAttr("style").removeAttr("id");
            $tr.after($clone[0]);
            $($clone[0]).attr("data-selector", newId);
            $($($($clone[0]).find("td")[1]).find(":file")[0]).attr("id", newId).addClass("answerImage");
            $clone.find(':text').val('');
        };

        // Set all answers in a answer list array and take image path
        self.BindAllAnswer = function () {
            var globalIdentifier, arrayAnswerList = [], formData, totalUploaders, tableTR, counter = 0;
            formData = new FormData();
            totalUploaders = $('.answerImage').length;

            tableTR = $('#tblAnswer > tbody > tr').not(':first');
            $.each(tableTR, function (index, trElement) {
                var trId, answerId, currentTRAnswerDTO, titleValue, toolTipValue, ImagePath, i, file, fileExtention;
                trId = $(this).attr("data-selector");
                answerId = $(this).attr("id");
                currentTRAnswerDTO = new AnswerModel();
                titleValue = $(this).find('td .answerTitle').val();
                toolTipValue = $(this).find('td .answerTooltip').val();
                ImagePath = $(this).find('td .imagepath').val();
                if (titleValue.trim() !== "") {
                    currentTRAnswerDTO.Id = answerId;
                    currentTRAnswerDTO.Title = titleValue;
                    currentTRAnswerDTO.ToolTip = toolTipValue;

                    if (self.model.QuestionTypeId() === parseInt(common.QuestionType.Number_Slider, 0)) {
                        currentTRAnswerDTO.ImagePath = null;
                    } else {
                        currentTRAnswerDTO.ImagePath = ImagePath;

                        for (i = 0; i < totalUploaders; i++) {
                            file = document.getElementById(trId).files[0];
                            if (parseInt($(this).attr('data-selector'), 0) === parseInt($($('.answerImage')[i]).attr('id'), 0)) {
                                if (file !== undefined && file !== null) {
                                    fileExtention = file.name.substring(file.name.length - 4);
                                    globalIdentifier = common.GenerateGUID();
                                    currentTRAnswerDTO.ImagePath = "Images/AnswerImages/" + globalIdentifier + fileExtention;
                                    formData.append((globalIdentifier + fileExtention), file);
                                } else {
                                    currentTRAnswerDTO.ImagePath = "";
                                }
                            }
                        }
                    }
                    arrayAnswerList.push(currentTRAnswerDTO);
                }
            });
            self.model.AnswerDTOList(arrayAnswerList);
            return formData;
        };

        // Get Language list to fill dropdown box
        self.GetLanguageList = function () {
            common.GetAJAXCall(common.APIUrls.GetLanguage, {}, self.GetLanguageListSuccess, true);
        };

        // Get GetLanguageList Success event
        self.GetLanguageListSuccess = function (data) {
            var array = [];
            $.each(data, function (index, value) {
                array.push(value);
            });
            self.LanguageList(array);
        },

        // Sorting on main question table
        self.SortingPaging = function () {
            $(document).ready(function () {
                $('#tblQuestionList').dataTable({
                    "bPaginate": true,
                    "sDom": '<"top"<"clear">>rt<"bottom"p<"clear">>',
                    "bFilter": false,
                    "bAutoWidth": false,
                    "sPaginationType": "full_numbers",
                    "iDisplayLength": 10,
                    "aoColumnDefs": [
                        { "bSortable": false, "aTargets": [5] }
                    ]
                });
            });
        };

        // Cancel event on poopup
        self.Cancel = function () {
            if (self.model.Id() <= 0) {
                self.CancelMessage(common.Messages[languageCode].AddQuestionPopupCancel);
            } else {
                self.CancelMessage(common.Messages[languageCode].UpdateQuestionPopupCancel);
            }
        };

        // Cancel message on click on cancel button
        self.CancelMessage = function (message) {
            $.confirm({
                title: "",
                content: message,
                confirmButton: common.ButtonText[languageCode].OK,
                cancelButton: common.ButtonText[languageCode].Cancel,
                backgroundDismiss: false,
                confirm: function () {
                    common.HideLoader();
                    $('#myModal').modal('hide');
                },
                cancel: function () {
                    common.HideLoader();
                },
            });
        };

        // Text input type dropdown box change event
        self.TextInputTypeChange = function () {
            if (self.model.TextInputTypeId() > 0) {
                if (self.model.TextInputTypeId() === parseInt(common.TextInputType.Number, 0)) {
                    $("#ddlQuestionType option").each(function (optionValue) {
                        if (optionValue === parseInt(common.QuestionType.Single_Select) || optionValue === parseInt(common.QuestionType.Multi_Select) || optionValue === parseInt(common.QuestionType.TextArea)) {
                            $(this).addClass("hidden");
                        }
                        if (optionValue === (parseInt(common.QuestionType.Number_Slider))) {
                            $(this).removeClass("hidden");
                        }
                    });
                }
                if (self.model.TextInputTypeId() === parseInt(common.TextInputType.AlphaNumeric, 0)) {
                    $("#ddlQuestionType option").each(function (option) {

                        // Add hide class
                        if (option === (parseInt(common.QuestionType.Number_Slider))) {
                            $(this).addClass("hidden");
                        } else if ($(this).hasClass("hidden")) {
                                // Remove hide class
                            $(this).removeClass("hidden");
                        }
                    });
                }
            }
            if (self.model.Id() <= 0) {
                // Make Empty textboxes
                $("#tblAnswer tr:eq(1)").find("td:eq(0) .answerTitle").val("");
                $("#tblAnswer tr:eq(1)").find("td:eq(2) .answerTooltip").val("");
                $("#ddlQuestionType").val("");
            }
        };

        // Question type dropdown box change event
        self.QuestionTypeChange = function () {
            if (self.model.QuestionTypeId() > 0) {
                if (self.model.QuestionTypeId() === parseInt(common.QuestionType.Number_Slider, 0)) {
                    if ($("#tblAnswer tr").length > 2) {
                        common.AlertBox(common.Messages[languageCode].DeleteAnswerToMakeNumberSlider);
                        $("#ddlQuestionType").val(parseInt(common.QuestionType.TextBox, 0));
                        return;
                    }

                    $("#tblHeader tr").find("th:eq(0)").html("Minimum Value");
                    $("#tblHeader tr").find("th:eq(1)").addClass("hidden");
                    $("#tblHeader tr").find("th:eq(2)").html("Maximum Value");
                    $("#tblHeader tr").find("th:eq(3)").addClass("hidden");

                    $("#tblAnswer tr:eq(1)").find("td:eq(1)").addClass("hidden");
                    $("#tblAnswer tr:eq(1)").find("td:eq(3)").addClass("hidden");

                    // Make Empty textboxes
                    if (self.model.Id() <= 0) {
                        $("#tblAnswer tr:eq(1)").find("td:eq(0) .answerTitle").val("");
                        $("#tblAnswer tr:eq(1)").find("td:eq(2) .answerTooltip").val("");
                    }
                } else {
                    $("#tblHeader tr").find("th:eq(0)").html("Text");
                    $("#tblHeader tr").find("th:eq(1)").removeClass("hidden");
                    $("#tblHeader tr").find("th:eq(2)").html("Tooltip");
                    $("#tblHeader tr").find("th:eq(3)").removeClass("hidden");

                    $("#tblAnswer tr:eq(1)").find("td:eq(1)").removeClass("hidden");
                    $("#tblAnswer tr:eq(1)").find("td:eq(3)").removeClass("hidden");

                    // Make Empty textboxes
                    if (self.model.Id() <= 0) {
                        $("#tblAnswer tr:eq(1)").find("td:eq(0) .answerTitle").val("");
                        $("#tblAnswer tr:eq(1)").find("td:eq(2) .answerTooltip").val("");
                    }
                }
            }
        };

        // Load data after DOM created
        $(document).ready(function () {
            // Click on delete answer
            $('.delete').click(function () {
                var afterRemvoedAllAnswer, answerId, tr;
                if (self.model.IsMapped()) {
                    common.AlertBox(common.Messages[languageCode].AnswerQuestionAlreadyMapped);
                    return;
                }
                tr = $(this).closest('tr');
                $.confirm({
                    title: "",
                    content: common.Messages[languageCode].DeleteAnswerConfirm,
                    confirmButton: common.ButtonText[languageCode].OK,
                    cancelButton: common.ButtonText[languageCode].Cancel,
                    backgroundDismiss: false,
                    confirm: function () {
                        common.HideLoader();
                        answerId = $(tr).attr("id");
                        if (answerId !== null) {
                            if (parseInt(answerId, 0) > 0) {
                                afterRemvoedAllAnswer = $.grep(self.model.AnswerDTOList(), function (answer) {
                                    return answer.Id !== parseInt(answerId, 0);
                                });

                                self.model.AnswerDTOList([]);
                                self.model.AnswerDTOList(afterRemvoedAllAnswer);
                               
                            }
                        }
                        clonedTrId--;
                        $(tr).remove();
                        if ($("#tblAnswer tr").length < 5) {
                            self.RemoveScrollBar();
                        }
                    },
                    cancel: function () {
                        common.HideLoader();
                    },
                });
            });

            // Allow number only
            $('.form-control.answerbox').keydown(function (e) {
                if (self.model.TextInputTypeId() > 0) {
                    if (self.model.TextInputTypeId() === parseInt(common.TextInputType.Number, 0) && self.model.QuestionTypeId() === parseInt(common.QuestionType.Number_Slider, 0)) {
                        // Allow: backspace, delete, tab, escape, enter and .
                        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                            // Allow: Ctrl+A, Command+A
                            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                            // Allow: home, end, left, right, down, up
                            (e.keyCode >= 35 && e.keyCode <= 40)) {
                            // let it happen, don't do anything
                            return;
                        }
                        // Ensure that it is a number and stop the keypress
                        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                            e.preventDefault();
                        }

                        // Ensure not to enter more than 4 number in length
                        if ($(e.currentTarget).val().length > 4) {
                            e.preventDefault();
                        }
                    }
                }
            });

            // To display image path in file uploader
            $('.typefile').change(function () {
                $(this).parent('.browseStyle').children('.browse').val($(this).val());
            });
        });
    }

    var questionViewModel = new QuestionViewModel();
    questionViewModel.LoadData();
    questionViewModel.GetQuestionTypeList();
    questionViewModel.GetTextInputTypeList();
    questionViewModel.GetLanguageList();

    ko.applyBindings(questionViewModel, document.getElementById('divQuestion'));

});

