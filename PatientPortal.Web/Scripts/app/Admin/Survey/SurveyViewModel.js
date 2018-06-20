//************************************** Survey View Model : knockout view model **************************************//
/*jslint browser: true, bitwise: true, for: true, this : true */
/*global  $,baseUrl,define,languageCode,window,location,ko,document,self,FormData, _data */
define(['surveymodel', 'yaleNexGrid', 'questiontreemodel', 'answertreemodel', 'producttreemodel', 'surveyquestionmapmodel', 'common', 'jqueryMigrate'], function (SurveyModel, PatientPortalGrid, QuestionTreeModel, AnswerTreeModel, ProductTreeModel, SurveyQuestionMapModel, Common, jqueryMigrate) {
    // 'use strict';
    function SurveyViewModel() {
        var self = this, common;
        self.yaleGrid = new PatientPortalGrid();
        self.LanguageList = ko.observableArray([]);
        self.FillQuestionListFromDB = [];
        self.QuestionList = [];
        self.QuestionAllList = [];
        self.ProductAllList = [];
        self.ProductList = [];
        self.AddNotForLoop = [];
        self.QuestionListFromDB = [{ id: 0, text: "Drag your questions here", expanded: true, NodeType: "Main", items: [] }];
        self.ProductTree = new ProductTreeModel();
        self.QuestionOrderNumber = 0;
        self.isMainNode = 0;
        self.sameQuestionNumber = 0;
        self.IsAnswerHasProduct = false;
        self.questionCounter = 1;
        common = new Common();
        self.model = new SurveyModel();

        //Function to Load Data using WEB API
        self.LoadData = function () {
            $("#liSurveys").addClass("active");
            common.GetAJAXCall(common.APIUrls.GetSurvey, {}, self.LoadDataSuccess, true);
        };

        // Get LoadData Success event
        self.LoadDataSuccess = function (data) {
            self.yaleGrid.DataRows([]);
            self.yaleGrid.DataRows(data);
            self.SortingPaging();
            common.HideLoader();
        },

        // Copy Survey Link
        self.CopySurveyLink = function (data) {
            self.CopyToClipboard(data.EncryptSurveyIdLanguageId);
        };

        self.CopyToClipboard = function (surveyLink) {
            if ($.browser.chrome) {
                var textField = document.createElement('textarea');
                textField.innerText = surveyLink;
                document.body.appendChild(textField);
                textField.select();
                document.execCommand('copy');
                textField.remove();
            }
            else {
                var $temp = $("<input />");
                $("body").append($temp);
                $temp.val(surveyLink).select();

                var result = false;
                try {
                    result = document.execCommand("copy");
                } catch (err) {
                    console.log("Copy error: " + err);
                }

                $temp.remove();
                return result;
            }
        };

        // Set Question ToolTip
        self.SetQuestionToolTip = function () {
            var questionId, questionDTO, questionEl, answerId, answerDTO;
            $("#QuestionListTreeView .questionFolder").each(function (el) {
                questionId = $("#QuestionListTreeView").data("kendoTreeView").dataSource._data[el].QuestionId;
                questionDTO = $.grep(self.QuestionAllList, function (questionDTO) {
                    return questionDTO.Id === questionId;
                });
                questionEl = $($(this).closest("span").parent()[0]).attr("title", questionDTO[0].Title);

                // Set Answer Tooltip
                if (questionDTO[0].AnswerDTOList.length > 0) {
                    questionDTO[0].AnswerDTOList.forEach(function (answerDTOObj, index) {

                        answerId = $("#QuestionListTreeView").data("kendoTreeView").dataSource._data[el].items[index].AnswerId;
                        answerDTO = $.grep(questionDTO[0].AnswerDTOList, function (answerDTO) {
                            return answerDTO.Id === answerId;
                        });
                        $($($(questionEl).parent().parent().find("li div span .answerFolder")[index])[0]).parent().attr("title", answerDTO[0].Title);
                    });
                }
            });
        };

        // Set Product ToolTip
        self.SetProductToolTip = function () {
            var productId, productDTO;
            $("#ProductListTreeView .productFolder").each(function (el) {
                productId = $("#ProductListTreeView").data("kendoTreeView").dataSource._data[el].ProductId;
                productDTO = $.grep(self.ProductAllList, function (productDTO) {
                    return productDTO.Id === productId;
                });
                $($(this).closest("span").parent()[0]).attr("title", productDTO[0].Name);
            });
        };

        // Function use to get all questions list
        self.GetQuestionListToFillTree = function () {
            var id = self.model.LanguageId();
            if (id > 0) {
                self.QuestionAllList = [];
                common.GetAJAXCall(common.APIUrls.GetSurveyQuestionList + id, {}, self.GetQuestionListToFillTreeSuccess, false);
            }
        };

        // Get GetQuestionListToFillTree Success event
        self.GetQuestionListToFillTreeSuccess = function (data) {
            $.each(data, function (index, questionDTO) {
                self.QuestionAllList.push(questionDTO);
                self.QuestionList.push(self.CreateQuestionTree(questionDTO));
            });

            // Function use to set data source of question tree view
            self.FillQuestionListTree();

            // Set question ToolTip
            self.SetQuestionToolTip();

            // Function use to make product tree
            self.GetProductListToFillTree();
        },

        // Function use to get all product list
        self.GetProductListToFillTree = function () {
            self.ProductAllList = [];
            common.GetAJAXCall(common.APIUrls.GetProduct, {}, self.GetProductListToFillTreeSuccess, true);
        };

        // Get GetProductListToFillTree Success event
        self.GetProductListToFillTreeSuccess = function (data) {
            var languageId = self.model.LanguageId();
            $.each(data, function (index, productDTO) {
                self.ProductAllList.push(productDTO);
                if (productDTO.LanguageId === languageId) {
                    self.ProductList.push(self.CreateProductTree(productDTO));
                }
            });

            // Function use to set data source of product tree view
            self.FillProductListTree();

            // Set product tooltip
            self.SetProductToolTip();
        },

        // Set data source of question list tree view
        self.FillQuestionListTree = function () {
            self.DestroyTree('QuestionListTreeView');
            var dataSource = self.QuestionList;
            $("#QuestionListTreeView").kendoTreeView({
                dragAndDrop: true,
                dataSource: dataSource,
                drop: self.OnDropItemFromTree
            });
        };

        // Set data source of product list tree view
        self.FillProductListTree = function () {
            self.DestroyTree('ProductListTreeView');
            var dataSource = self.ProductList;
            $("#ProductListTreeView").kendoTreeView({
                dragAndDrop: true,
                dataSource: dataSource,
                drop: self.OnDropItemFromTree
            });
        };

        // Set data source of product list tree view
        self.FillUpdateMappingListTree = function () {
            self.DestroyTree('MappingListTreeView');

            // Set all nodes under first default
            $.each(self.FillQuestionListFromDB, function (index, questionDTO) {
                if (questionDTO.QuestionId > 0) {
                    self.QuestionListFromDB[0].items.push(questionDTO);
                }
            });

            // Set datasource for mapped tree view
            $("#MappingListTreeView").kendoTreeView({
                dragAndDrop: true,
                dataSource: self.QuestionListFromDB,
                drop: self.OnDropItemFromTree
            });
        };

        // Method to destroy tree view with its elements
        self.DestroyTree = function (treeElementId) {
            var treeview = $("#" + treeElementId).data("kendoTreeView");
            if (treeview) {
                treeview.destroy();
                $("#" + treeElementId).empty();
            } else {
                $("#" + treeElementId).empty();
            }
        };

        // Function use to call when drag and drop from tree
        self.OnDropItemFromTree = function (e) {

            var sourceItem, destinationNode, targetTree, parentNodeType, dst, first, position;
           
            
            sourceItem = this.dataItem(e.sourceNode).toJSON();
            destinationNode = $(e.destinationNode);
            targetTree = destinationNode.closest("[data-role='treeview']").data("kendoTreeView");
            parentNodeType = $("#MappingListTreeView").data("kendoTreeView").dataItem(e.destinationNode);

            // Prevent first node
            dst = $("#MappingListTreeView").data("kendoTreeView").dataItem(e.destinationNode);
            var mappingTree = $("#MappingListTreeView");
            first = mappingTree.data("kendoTreeView").dataItem(mappingTree.find(".k-item:first"));
            position = e.dropPosition;

            // Prevent to drop a node before or after of first node
            if (dst && dst.uid === first.uid && (position === "before" || position === "after")) {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDragAndDrop);
                return false;
            }

            // Invalid drop location
            if (destinationNode.length <= 0 || !parentNodeType) {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDragAndDrop);
                return false;
            }

            // To prevent a question/answer/product to make it nested of a node
            if (sourceItem.NodeType === parentNodeType.NodeType && e.sourceNode.id != "MappingListTreeView_tv_active") {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDrop);
                return false;
            }

            // To prevent to drop answer in any question
            if (parentNodeType.NodeType === "QuestionType" && sourceItem.NodeType === "AnswerType") {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDrop);
                return false;
            }

            // To prevent a question/answer/product to make it nested of a node
            if (parentNodeType.NodeType === "ProductType" || (sourceItem.NodeType === "ProductType" && parentNodeType.NodeType === "QuestionType")) {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDrop);
                return false;
            }

            // To prevent a question to drop with in a question of a node
            if (sourceItem.NodeType === "QuestionType" && parentNodeType.NodeType === "QuestionType" && e.sourceNode.id != "MappingListTreeView_tv_active") {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDrop);
                return false;
            }

            if (parentNodeType.NodeType === "AnswerType" && parentNodeType.items.length > 0) {
                var productCounter = 0;
                for (var counter = 0; counter < parentNodeType.items.length; counter++) {
                    if (sourceItem.ProductId === parentNodeType.items[counter].ProductId) {
                        e.preventDefault();
                        common.AlertBox(common.Messages[languageCode].InvalidDrop);
                        return false;
                    }

                    if (parentNodeType.items[counter].ProductId > 0)
                    {
                        productCounter++;
                    }

                    if (sourceItem.NodeType === "QuestionType" && productCounter > 0)
                    {
                        e.preventDefault();
                        common.AlertBox(common.Messages[languageCode].InvalidDrop);
                        return false;
                    }
                }
            }

            // To prevent a question, if a question already within a destination node
            //if (parentNodeType.NodeType !== "Main" && sourceItem.NodeType !== parentNodeType.NodeType) { //&& parentNodeType.items.length > 0
            //    common.AlertBox(common.Messages[languageCode].InvalidDrop);
            //    return false;
            //}

            // To prevent a product to make it with answer of a node
            if (sourceItem.NodeType === "ProductType" && parentNodeType.NodeType === "AnswerType" && (position === "after" || position === "before")) {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDrop);
                return false;
            }


            // To prevent from dropping a product or answer in the main node
            if (parentNodeType.NodeType === "Main" && (sourceItem.NodeType === "AnswerType" || sourceItem.NodeType === "ProductType")) {
                e.preventDefault();
                common.AlertBox(common.Messages[languageCode].InvalidDrop);
                return false;
            }
            if (e.sourceNode.id == "MappingListTreeView_tv_active" && e.destinationNode.id == "" && (sourceItem.NodeType !== "QuestionType" && parentNodeType.NodeType !== "QuestionType") && (sourceItem.NodeType !== "ProductType" || parentNodeType.NodeType !== "AnswerType") && (parentNodeType.NodeType !== "ProductType" || (sourceItem.NodeType !== "ProductType" && parentNodeType.NodeType !== "QuestionType"))) {
                e.preventDefault();
            }
            if (e.sourceNode.id == "QuestionListTreeView_tv_active" || e.sourceNode.id == "ProductListTreeView_tv_active") {
                e.preventDefault();
                if (position === "before") {
                    targetTree.insertBefore(sourceItem, destinationNode);
                } else if (position === "after") {
                    targetTree.insertAfter(sourceItem, destinationNode);
                } else {
                    targetTree.append(sourceItem, destinationNode);
                }
            }
        };

        // Create Question Tree View
        self.CreateQuestionTree = function (questionDTO) {
            var QuestionTree = new QuestionTreeModel();
            QuestionTree.QuestionId = questionDTO.Id;
            //QuestionTree.text = common.SetTextLength(questionDTO.Title);
            QuestionTree.text = questionDTO.Title;
            QuestionTree.expanded = false;
            QuestionTree.NodeType = "QuestionType";

            if (questionDTO.AnswerDTOList.length > 0) {
                $.each(questionDTO.AnswerDTOList, function (index, answerDTO) {
                    QuestionTree.items.push(self.CreateAnswerTree(answerDTO, questionDTO.QuestionTypeId));
                });
            } else {
                QuestionTree.items = [];
            }
            return QuestionTree;
        };

        // Create Answer Tree View
        self.CreateAnswerTree = function (answerDTO, questionTypeId) {
            var AnswerTree = new AnswerTreeModel();
            AnswerTree.AnswerId = answerDTO.Id;
            if (questionTypeId === parseInt(common.QuestionType.Number_Slider, 0)) {
                AnswerTree.text = answerDTO.Title + " - " + answerDTO.ToolTip;
            } else {
                //AnswerTree.text = common.SetTextLength(answerDTO.Title);
                AnswerTree.text = answerDTO.Title;
            }
            AnswerTree.expanded = false;
            AnswerTree.NodeType = "AnswerType";
            AnswerTree.items = [];
            return AnswerTree;
        };

        // Create Product Tree View
        self.CreateProductTree = function (productDTO) {
            var productTree = new ProductTreeModel();
            productTree.ProductId = productDTO.Id;
            //productTree.text = common.SetTextLength(productDTO.Name);
            productTree.text = productDTO.Name;
            productTree.expanded = true;
            productTree.NodeType = "ProductType";
            return productTree;
        };

        // Remove Node
        self.RemoveNode = function () {
            //if (self.model.IsMapped()) {
            //    common.AlertBox(common.Messages[languageCode].NodeAlreadyMapped);
            //    return;
            //}
            var selectedNode, currentNode;
            selectedNode = $("#MappingListTreeView").data("kendoTreeView").select();

            currentNode = $("#MappingListTreeView").data("kendoTreeView").dataItem(selectedNode);
            if (currentNode.NodeType === "Main") {
                common.AlertBox(common.Messages[languageCode].InvalidDelete);
                return false;
            }
            if (currentNode.NodeType === "AnswerType") {
                common.AlertBox(common.Messages[languageCode].InvalidDelete);
                return false;
            }
            $("#MappingListTreeView").data("kendoTreeView").remove(selectedNode);
        };

        // Get Mapped questions from treeview and create DTO collection for SurveyQuestionMap
        self.SaveMappedTree = function () {
            var parentSurveyQuestionMapId = 0, questionId = 0;
            self.model.SurveyQuestionMapList = [];
            var treeviewDataSource = $("#MappingListTreeView").data("kendoTreeView").dataSource.view().toJSON();
            if (treeviewDataSource.length > 0) {

                treeviewDataSource[0].items.forEach(function (questionTree) {
                    self.isMainNode++;
                    self.questionCounter++;
                    self.sameQuestionNumber++;
                    self.GetQuestionNode(questionTree, parentSurveyQuestionMapId, self.questionCounter, self.sameQuestionNumber);
                    questionId = questionTree.QuestionId;
                });
            }
        };

        // Recursive function to get nth level of questions or product
        self.GetQuestionNode = function (questionTree, parentSurveyQuestionMapId, questionNodeNumber, sameQuestionNumber) {
            //number = sameQuestionNumber;
            var guid = 0, answerCounter, isTreeHasNode = false, currentQuestionId, number;
            var questionSerialNumber = questionNodeNumber;
            if (questionTree.items.length > 0) {
                var answerItemLength = 0;
                questionTree.items.forEach(function (answer) {
                    answerItemLength++;
                    var surveyQuestionMapModel = new SurveyQuestionMapModel();


                    // Set same Question number
                    surveyQuestionMapModel.SameNodeNumber = questionSerialNumber;

                    // Set Question Id
                    surveyQuestionMapModel.QuestionId = questionTree.QuestionId;

                    // Set Answer Id
                    surveyQuestionMapModel.AnswerId = answer.AnswerId;

                    // Set same node number
                    surveyQuestionMapModel.IsMainNode = self.isMainNode;



                    surveyQuestionMapModel.SameQuestionNumber = sameQuestionNumber;


                    answer.items.forEach(function (nextNode) {



                        if (parentSurveyQuestionMapId > 0 && parentSurveyQuestionMapId != questionTree.QuestionId) {
                            surveyQuestionMapModel.IsParent = true;
                        }
                        else {
                            surveyQuestionMapModel.IsParent = false;
                        }

                        if (nextNode.NodeType == "ProductType") {
                            // Set record order number
                            surveyQuestionMapModel.QuestionOrderNumber = self.QuestionOrderNumber++;

                            surveyQuestionMapModel.ProductId = nextNode.ProductId;
                            parentSurveyQuestionMapId = null;

                            if (!self.IsAnswerHasProduct) {
                                self.IsAnswerHasProduct = true;
                            }
                        }

                        if (nextNode.NodeType == "QuestionType") {
                            number = sameQuestionNumber
                            parentSurveyQuestionMapId = questionTree.QuestionId;
                            surveyQuestionMapModel.ProductId = null;

                            // Child Question if any
                            surveyQuestionMapModel.ChildQuestionId = nextNode.QuestionId;
                            questionNodeNumber++;
                            number++;
                            // Set record order number
                            surveyQuestionMapModel.QuestionOrderNumber = self.QuestionOrderNumber++;
                            self.GetQuestionNode(nextNode, parentSurveyQuestionMapId, questionNodeNumber, number);

                        }

                        var tempJSONDataMK = {};
                        tempJSONDataMK = ko.toJS(surveyQuestionMapModel); // Push temprory data
                        var surveyFinalQuestionMapModel = {};
                        ko.mapping.fromJS(tempJSONDataMK, {}, surveyFinalQuestionMapModel);
                        self.model.SurveyQuestionMapList.push(surveyFinalQuestionMapModel);

                    });

                    if (answer.items.length <= 0) {
                        // Set record order number
                        surveyQuestionMapModel.QuestionOrderNumber = self.QuestionOrderNumber++;
                        self.model.SurveyQuestionMapList.push(surveyQuestionMapModel);
                    }
                    currentQuestionId = questionTree.QuestionId;
                });
            }
            if (number) {
                self.sameQuestionNumber = number;
            }

            self.questionCounter = questionNodeNumber;
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

        //Function to Delete the Record
        self.DeleteSurvey = function (data, model) {
            common.ShowLoader();
            var currentRow, row;
            currentRow = $('#tblSurveyList [id^="' + model.Id + '"]');
            row = $('#tblSurveyList').DataTable().row(currentRow[0]);

            if (model.IsMapped) {
                common.AlertBox(common.Messages[languageCode].SurveyAlreadyMapped);
                return;
            }
            $.confirm({
                title: "",
                content: common.Messages[languageCode].DeleteSurveyConfirm + (row[0][0] + 1),
                confirmButton: common.ButtonText[languageCode].OK,
                cancelButton: common.ButtonText[languageCode].Cancel,
                backgroundDismiss: false,
                confirm: function () {
                    common.HideLoader();
                    var isSuccess = false;
                    isSuccess = common.DeleteAJAXCall(common.APIUrls.DeleteSurvey + data.Id, self.DeleteSuccess);
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
            if (data["<ErrorCode>k__BackingField"] > 0) {
                common.AlertBox(common.Messages[languageCode].Error);
            } else {
                self.model.ClearData();
            }
        };

        //Function to Delete the Record
        self.edit = function (data) {
            self.model.SetData(data);
        };

        // Check there should be atleast one question with its product or nested question
        self.QuestionValid = function () {
            var noRecord = false;
            if (!self.IsAnswerHasProduct) {
                noRecord = true;
            }
            return noRecord;
        };

        // Insert survey record
        self.InsertSurvey = function (data) {
            common.ShowLoader();
            self.model.ValidationEnabled(true);

            if (!self.model.isValid()) {
                common.HideLoader();
                self.model.Errors.showAllMessages();
                return;
            }
            self.SaveMappedTree();
            if (data.model.IsActive() && !self.IsAnswerHasProduct) {
                common.AlertBox(common.Messages[languageCode].NoDataInNode);
                return;
            }
            var surveyDTO = JSON.stringify(ko.mapping.toJS(data.model));
            var isSuccess = false;
            common.PostAJAXCall(common.APIUrls.PostSurvey, surveyDTO, self.SuccessSurvey, true);
        };

        // Success Survey
        self.SuccessSurvey = function (data) {
            if (data["<ErrorCode>k__BackingField"] > 0) {
                common.AlertBox(common.Messages[languageCode].Error);
            } else {
                common.AlertBoxRefreshPage(common.Messages[languageCode].Insert);
            }
        };

        // Update survey record
        self.UpdateSurvey = function (data) {

            common.ShowLoader();
            if (!self.model.isValid()) {
                common.HideLoader();
                self.model.Errors.showAllMessages();
                return;
            }
            self.SaveMappedTree();
            if (data.model.IsActive() && !self.IsAnswerHasProduct) {
                common.AlertBox(common.Messages[languageCode].NoDataInNode);
                return;
            }
            var surveyDTO = JSON.stringify(ko.mapping.toJS(data.model));
            var isSuccess = false;
            common.PutAJAXCall(common.APIUrls.PutSurvey, surveyDTO, self.UpdateSuccess, true);
        };

        // Update Success
        self.UpdateSuccess = function (data) {
            if (data["<ErrorCode>k__BackingField"] > 0) {
                common.AlertBox(common.Messages[languageCode].Error);
            } else {
                common.AlertBoxRefreshPage(common.Messages[languageCode].Update);
            }
        };

        // Get record by survey id
        self.GetSurveyById = function (surveyId) {
            common.ShowLoader();
            common.GetAJAXCall(common.APIUrls.GetSurveyById + surveyId, {}, self.GetSurveyByIdSuccess, false);
        };

        // Get GetLanguageList Success event
        self.GetSurveyByIdSuccess = function (data) {
            $("#spnInsert").hide();
            $("#spnUpdate").show();
            $('#addSurvey').modal('show');
            $("#ddlLanguage").attr("disabled", true);
            $("#myModalLabel").text("Edit Survey");
            self.DestroyTree('QuestionListTreeView');
            self.DestroyTree('ProductListTreeView');
            self.QuestionList = [];
            self.ProductList = [];
            self.QuestionListFromDB = [{ text: "Drag your questions here", expanded: true, NodeType: "Main", items: [] }];
            self.FillQuestionListFromDB = [];
            self.model.SetData(data);
            self.GetQuestionListToFillTree();
            self.CreateNodeFromDB(self.model);
            self.FillUpdateMappingListTree();
            self.CreateSurveyLink(data.EncryptSurveyIdLanguageId);
            common.HideLoader();
        },

        // Create Survey link in EDIT mode
        self.CreateSurveyLink = function (surveyIdLanguageId) {
            $("#divSurveyLink").removeClass("hidden");
            $("#spnSurveyLink").html(common.RedirctPage.SurveyUser + surveyIdLanguageId);
        };

        // Create Survey link in EDIT mode
        self.CopySurvey = function (data) {
            self.GetSurveyById(data.Id);
            $("#spnInsert").show();
            $("#spnUpdate").hide();
            self.model.Id(0);
            self.model.IsMapped(false);
        };

        // Get question from a question list
        self.GetQuestionFromList = function (questionId) {
            var questionDTO = $.grep(self.QuestionAllList, function (questionDTO) {
                return questionDTO.Id === questionId;
            });
            return questionDTO;
        };

        // To find an item in remove list
        self.FindAlreadyAddedRecord = function (surveyQuestionMapId) {
            var isAddedAlready = false, i;
            for (i = 0; i < self.AddNotForLoop.length; i++) {
                if (self.AddNotForLoop[i].Id === surveyQuestionMapId) {
                    return true;
                }
            }
            return isAddedAlready;
        };

        // Create tree view map from DB data
        self.CreateNodeFromDB = function () {
            self.AddNotForLoop = [];
            if (self.model.SurveyQuestionMapList() !== null && self.model.SurveyQuestionMapList().length > 0) {
                var nodeNumber = 0;
                self.model.SurveyQuestionMapList().forEach(function (surveyQuestionMapDTO, index) {
                    if (nodeNumber != surveyQuestionMapDTO.IsMainNode) {
                        var questTree = self.SetQuestionForTree(surveyQuestionMapDTO);
                        nodeNumber = surveyQuestionMapDTO.IsMainNode;
                        self.FillQuestionListFromDB.push(questTree);
                    }
                });
            }
        };

        // Create question for tree
        self.SetQuestionForTree = function (surveyQuestionMapDTO) {
            var QuestionTree, questionDTO;

            QuestionTree = new QuestionTreeModel();
            if (surveyQuestionMapDTO) {
                questionDTO = self.GetQuestionFromList(surveyQuestionMapDTO.QuestionId);
                QuestionTree.QuestionId = questionDTO[0].Id;
                QuestionTree.text = questionDTO[0].Title;
                QuestionTree.expanded = false;
                QuestionTree.NodeType = "QuestionType";
                QuestionTree.items = self.SetAnswerForQuestionInTree(questionDTO[0], surveyQuestionMapDTO);
            }
            return QuestionTree;
        };

        self.SetAnswerForQuestionInTree = function (questionDTO, surveyQuestionMapDTO) {
            var AnswerListDB = [], productTree, nextSurveyQuestionMapDTO;
            $.each(questionDTO.AnswerDTOList, function (index, answerDTO) {
                var questionTree;

                self.AddNotForLoop.push(surveyQuestionMapDTO);
                var AnswerTree = new AnswerTreeModel();
                AnswerTree.AnswerId = answerDTO.Id;

                if (parseInt(answerDTO.Title) > -1 && parseInt(answerDTO.ToolTip) > -1) {
                    AnswerTree.text = answerDTO.Title + " - " + answerDTO.ToolTip;
                } else {
                    AnswerTree.text = answerDTO.Title;
                }

                AnswerTree.expanded = false;
                AnswerTree.NodeType = "AnswerType";
                if (surveyQuestionMapDTO != null && surveyQuestionMapDTO != null && surveyQuestionMapDTO.ProductId == null && surveyQuestionMapDTO.AnswerId == answerDTO.Id && surveyQuestionMapDTO.ChildQuestionId != null && surveyQuestionMapDTO.ChildQuestionId > 0) {
                    nextSurveyQuestionMapDTO = self.GetNextSurveyQuestionMapRecord(surveyQuestionMapDTO);
                    questionTree = self.SetQuestionForTree(nextSurveyQuestionMapDTO);
                    AnswerTree.items.push(questionTree);
                    AnswerListDB.push(AnswerTree);

                } else if (surveyQuestionMapDTO != null && surveyQuestionMapDTO.ProductId != null && surveyQuestionMapDTO.AnswerId == answerDTO.Id) {
                    AnswerListDB.push(self.SetProductInTree(surveyQuestionMapDTO, AnswerTree, questionDTO.Id, answerDTO.Id));
                }
                else {
                    AnswerTree.items = [];
                    AnswerListDB.push(AnswerTree);
                }
                if (nextSurveyQuestionMapDTO != null && nextSurveyQuestionMapDTO.Id == 6205) {
                    var abc = "";
                }

                nextSurveyQuestionMapDTO = self.GetNextSurveyQuestionMapRecord(nextSurveyQuestionMapDTO);
                surveyQuestionMapDTO = nextSurveyQuestionMapDTO;
            });

            return AnswerListDB;
        },

        // Set product tree node in mapped treeview
        self.SetProductInTree = function (surveyQuestionMapDTO, AnswerTree, questionId, answerId) {
            var isAddedAlready = false;
            var productTree = self.AddProductinAnswer(surveyQuestionMapDTO);
            AnswerTree.items.push(productTree);

            for (var counter = 0; counter < self.AddNotForLoop.length; counter++) {
                if (self.AddNotForLoop[counter].Id == surveyQuestionMapDTO.Id) {
                    isAddedAlready = true;
                }
            }

            if (!isAddedAlready) {
                self.AddNotForLoop.push(surveyQuestionMapDTO);
            }

            var nextSurveyQuestionMapDTO = self.GetNextSurveyQuestionMapRecord(surveyQuestionMapDTO);
            if (nextSurveyQuestionMapDTO != null && nextSurveyQuestionMapDTO.ProductId != null && nextSurveyQuestionMapDTO.QuestionId == questionId && nextSurveyQuestionMapDTO.AnswerId == answerId) {
                surveyQuestionMapDTO = nextSurveyQuestionMapDTO;
                self.SetProductInTree(surveyQuestionMapDTO, AnswerTree, surveyQuestionMapDTO.QuestionId, surveyQuestionMapDTO.AnswerId);
            }
            return AnswerTree;
        },

        // Get Next Survey Question Map record
        self.GetNextSurveyQuestionMapRecord = function (surveyQuestionMapDTO) {
            var nextSurveyQuestionMapDTO = self.model.SurveyQuestionMapList()[self.AddNotForLoop.length];
            return nextSurveyQuestionMapDTO;
        };

        // Add products in tree
        self.AddProductinAnswer = function (surveyQuestionMapDTO) {
            var productTree = new ProductTreeModel();
            productTree.ProductId = surveyQuestionMapDTO.IProductDTO.Id;
            productTree.text = surveyQuestionMapDTO.IProductDTO.Name;
            productTree.expanded = true;
            productTree.NodeType = "ProductType";
            return productTree;
        };

        // Open a new page to add survey
        self.OpenAddSurvey = function () {
            self.model.ValidationEnabled(false);
            self.model.ClearData();
            self.model.LanguageId("");
            //$('#addSurvey').modal('show');
            $("#ddlLanguage").attr("disabled", false);
            $("#spnInsert").show();
            $("#spnUpdate").hide();
            self.model.IsMapped(false);
            $("#myModalLabel").text("Add Survey");
            $("#divSurveyLink").addClass("hidden");
            $("#spnSurveyLink").html('');
            self.DestroyTree("QuestionListTreeView");
            self.DestroyTree("ProductListTreeView");
            self.DestroyTree("MappingListTreeView");
            self.EmptyTreeView();
        };

        // Language dropdown box change event
        self.LanguageChange = function () {
            if (self.model.LanguageId() > 0) {
                self.EmptyTreeView();
            }
        };

        // Make emtpy all tree views
        self.EmptyTreeView = function () {
            self.QuestionList = [];
            self.ProductList = [];
            self.QuestionListFromDB = [{ text: "Drag your questions here", expanded: true, NodeType: "Main", items: [] }];
            self.FillQuestionListFromDB = [];
            self.GetQuestionListToFillTree();
            self.FillUpdateMappingListTree();
        };

        // Set sorting and paging on question list
        self.SortingPaging = function () {
            $(document).ready(function () {
                $('#tblSurveyList').dataTable({
                    "bPaginate": true,
                    "sDom": '<"top"<"clear">>rt<"bottom"p<"clear">>',
                    "bFilter": false,
                    "bAutoWidth": false,
                    "sPaginationType": "full_numbers",
                    "iDisplayLength": 10,
                    "aoColumnDefs": [
                        { "bSortable": false, "aTargets": [4] }
                    ]
                });


            });
        };

        // Cancel event on poopup
        self.Cancel = function () {
            if (self.model.Id() <= 0) {
                self.CancelMessage(common.Messages[languageCode].AddSurveyPopupCancel);
            } else {
                self.CancelMessage(common.Messages[languageCode].UpdateSurveyPopupCancel);
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
                    $('#addSurvey').modal('hide');
                },
                cancel: function () {
                    common.HideLoader();
                },
            });
        };
    }

    var surveyViewModel = new SurveyViewModel();
    surveyViewModel.LoadData();
    surveyViewModel.GetLanguageList();
    ko.applyBindings(surveyViewModel, document.getElementById('surveyDiv'));
});