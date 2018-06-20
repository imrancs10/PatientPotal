//************************************** Product View Model : knockout view model **************************************//
/*jslint browser: true, for: true, this : true*/
/*global $,baseUrl,define,languageCode,window,location,ko,document,self,FormData */
define(['productmodel', 'PatientPortalgrid', 'productimagemodel', 'common'], function (ProductModel, PatientPortalGrid, ProductImages, Common) {
    'use strict';
    function ProductViewModel() {
        var common, self = this, numberOfRows;
        common = new Common();
        numberOfRows = 1;
        self.ProductImagesDTOList = ko.observableArray([]);
        self.ProductImagesDTO = new ProductImages();
        self.ProductGrid = new PatientPortalGrid();
        self.ProductImagesGrid = new PatientPortalGrid();
        self.LanguageList = ko.observableArray([]);
        self.model = new ProductModel();
        self.ImageList = ko.observableArray([]);
        self.IsEdit = false;
        self.IsPopupOpen = false;
        self.SeekIndex = ko.observable(-1);
        self.ProductNameIfCopied = '';
        self.SeekIndex = ko.observable(-1);
        self.productId = null;

        // Get product images
        self.SaveProductImages = function (data) {
            $.ajax({
                type: "POST",
                url: common.APIUrls.SaveProductImage,
                data: data,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function () {
                    self.GetProductList(self.productId);
                },
                error: function () {
                    common.AlertBox(common.Messages[languageCode].Error);
                }
            });
        };

        // Get product images table and bind with list collection
        self.SetProductImages = function () {
            self.ProductImagesDTOList = [];
            var formData, totalUploaders, globalIdentifier, i, tableTR, productImagesDTO, mainFileName, mainFile, mainFileExtention, thumbFileName, thumbFile, mainThumbFileExtention, productImageId, mainImagePath, radioButtonValue, thumbImagePath;
            var fileSize = 0;
            formData = new FormData();
            totalUploaders = ($('input:file').length - 2) / 2;
            for (i = 1; i <= totalUploaders; i = i + 1) {
                tableTR = $('#tblImage > tbody > tr').not(':first');
                productImagesDTO = new ProductImages();
                globalIdentifier = common.GenerateGUID();
                mainFileName = $($($(tableTR[i - 1]).find("td")[0]).find(":file")[0]).attr("id");
                if (document.getElementById(mainFileName) !== null) {
                    var ab = mainFileName.ClientID;
                    mainFile = document.getElementById(mainFileName).files[0];
                    mainFile = document.getElementById(mainFileName).files[0];
                    if (mainFile && mainFile !== null && mainFile.size > 0) {
                        mainFileExtention = mainFile.name.substring(mainFile.name.length - 4);
                        formData.append("1" + globalIdentifier + mainFileExtention, mainFile);
                        $(tableTR[i - 1]).find('td .imagepath').html("Images/ProductImages/" + globalIdentifier + mainFileExtention);
                    }
                }
                thumbFileName = $($($(tableTR[i - 1]).find("td")[1]).find(":file")[0]).attr("id");
                if (document.getElementById(thumbFileName) !== null) {
                    thumbFile = document.getElementById(thumbFileName).files[0];
                    if (thumbFile && thumbFile !== null && thumbFile.size > 0) {
                        mainThumbFileExtention = thumbFile.name.substring(thumbFile.name.length - 4);
                        formData.append("2" + globalIdentifier + mainThumbFileExtention, thumbFile);
                        $(tableTR[i - 1]).find('td .thumbimagepath').html("Images/ProductImages/ProductThumbnails/" + globalIdentifier + mainThumbFileExtention);
                    }
                }

                productImageId = $(tableTR[i - 1]).attr("id");
                radioButtonValue = $(tableTR[i - 1]).find('td .radioIsPrimary').prop("checked");
                mainImagePath = $(tableTR[i - 1]).find('td .imagepath').html();
                thumbImagePath = $(tableTR[i - 1]).find('td .thumbimagepath').html();

                if (mainImagePath !== "" || thumbImagePath !== "") {
                    productImagesDTO.Id = productImageId;
                    productImagesDTO.ImagePath = mainImagePath;
                    productImagesDTO.ThumbnailPath = thumbImagePath;
                    productImagesDTO.IsPrimary = radioButtonValue;
                    self.ProductImagesDTOList.push(productImagesDTO);
                }
            }
            return formData;
        };

        // Check if there is images with product, so one file should be primary
        self.CheckOnePrimaryImage = function () {
            var isPrimary = false, isPrimaryCounter;
            if (self.ProductImagesDTOList.length > 0) {
                for (isPrimaryCounter = 0; isPrimaryCounter < self.ProductImagesDTOList.length; isPrimaryCounter = isPrimaryCounter + 1) {

                    if (self.ProductImagesDTOList[isPrimaryCounter].IsPrimary) {
                        isPrimary = true;
                        break;
                    }
                }
            } else {
                isPrimary = true;
            }
            return isPrimary;
        };

        // Get Editor value
        self.GetEditorValue = function () {
            return $("#txtJQEditor").data("kendoEditor").value();
        };

        // Set Editor value
        self.SetEditorValue = function (content) {
            return $("#txtJQEditor").data("kendoEditor").value(content);
        };

        // Insert a new product record
        self.InsertProduct = function (data) {
            common.ShowLoader();
            var productDTO, type, formData;
            formData = self.SetProductImages();
            self.model.ValidationEnabled(true);

            if (!self.CheckOnePrimaryImage()) {
                common.AlertBox(common.Messages[languageCode].ProductPrimaryImage);
                return;
            }

            if (!self.IsEdit && self.model.Name() !== "" && self.ProductNameIfCopied !== "" && self.model.Name() == self.ProductNameIfCopied) {
                common.AlertBox(common.Messages[languageCode].DuplicateProductName);
                return;
            }


            if (self.model.isValid()) {
                if (self.IsEdit && self.model.IsMapped() && self.ProductImagesDTOList.length <= 0) {
                    common.AlertBox(common.Messages[languageCode].ProductRequiredImages);
                    return;
                }

                data.model.Description(self.GetEditorValue());
                self.model.ProductImagesDTOList(self.ProductImagesDTOList.slice());

                type = self.IsEdit ? "PUT" : "POST";
                productDTO = JSON.stringify(ko.mapping.toJS(data.model));

                var isSuccess = false;

                if (self.IsEdit) {
                    isSuccess = common.PutAJAXCall(common.APIUrls.PostPutProduct + type, productDTO, self.InsertProductSuccess, false);
                } else {
                    isSuccess = common.PostAJAXCall(common.APIUrls.PostPutProduct + type, productDTO, self.InsertProductSuccess, false);
                }
                if (isSuccess) {
                    self.SaveProductImages(formData);
                    common.AlertBox(type === "PUT" ? common.Messages[languageCode].Update : common.Messages[languageCode].Insert);
                }
            } else {
                common.HideLoader();
                self.model.Errors.showAllMessages();
            }
            self.IsPopupOpen = false;
        };

        // Get InsertProduct Success event
        self.InsertProductSuccess = function (data) {
            self.productId = data.Id;
            $('#myModal').modal('hide');
        };

        // Copy product link
        self.ChangeOnCopy = function () {
            self.IsEdit = false;
            self.IsPopupOpen = true;
            self.GetProductById(self.model.MainId());
            self.ProductNameIfCopied = self.model.Name();
            self.BindImageTableFromDB();
            self.model.Id(0);
            //self.model.MainId(0);
            $("#ddlLanguage").attr("disabled", true);
            $('#myModalLabel').text('Copy Product');
            self.model.ValidationEnabled(false);
            self.model.IsMapped(false);
        };

        // Edit product link
        self.ChangeOnEdit = function () {
            common.ShowLoader();
            $('#myModalLabel').text('Edit Product');
            self.ProductNameIfCopied = '';
            self.IsEdit = true;
            self.IsPopupOpen = true;
            self.GetProductById(self.model.MainId());
            self.BindImageTableFromDB();
        };

        // Get product list
        self.GetProductList = function (productId) {
            self.productId = productId;
            $("#liProducts").addClass("active");
            self.model.ValidationEnabled(false);
            common.GetAJAXCall(common.APIUrls.GetProduct, {}, self.ProductListSuccess, true);
        };

        // Get ProductList Success event
        self.ProductListSuccess = function (data) {
            common.HideLoader();
            if (data !== null && data.length > 0) {
                var i, index = 0;
                if (self.productId != null) {
                    for (i = 0; i < data.length; i++) {
                        if (data[i].Id == self.productId) {
                            index = i;
                            break;
                        }
                    }
                }
                self.ProductGrid.DataRows(data);
                var countImages = self.GetImageCount();
                self.ImageList(data[index].ProductImagesDTOList.slice(0, countImages));
                self.SeekIndex(countImages);
                if (self.ImageList().length > 0) {
                    $('#divImageList').removeClass("hidden");
                } else {
                    $('#divImageList').addClass("hidden");
                }
                self.model.SetData(data[index]);
                self.SetEditorValue(data[index].Description);
                self.model.MainDescription(data[index].Description);
                //$("#ulProductGrid li").first().addClass("active");
                $("[data-selector='divMainProductDetail']").removeClass("hidden");

                $('#ulProductGrid').children().removeClass('active');
                $('#ulProductGrid').children().eq(index).addClass('active');
            } else {
                $("#divproductList").hide();
                $("#divProductDetail").hide();
                $("#spanNoProduct").removeClass("hidden");
                $("[data-selector='divMainProductDetail']").addClass("hidden");
            }
            self.LoadScrollBar();
            self.GetLanguageList();
        },

        // Get Product by id
        self.GetProductById = function (id, isEdit) {
            common.ShowLoader();
            common.GetAJAXCall(common.APIUrls.GetProductById + id, {}, self.GetProductByIdSuccess, false);
        };

        // Get product by id success event
        self.GetProductByIdSuccess = function (data) {
            common.HideLoader();
            if ($("#divScroll").hasClass("scroll")) {
                $("#divScroll").removeClass("scroll");
            }

            var countImages = self.GetImageCount();
            self.ImageList(data.ProductImagesDTOList.slice(0, countImages));
            self.SeekIndex(countImages);
            if (self.ImageList().length > 0) {
                $('#divImageList').removeClass("hidden");
            } else {
                $('#divImageList').addClass("hidden");
            }
            self.model.SetData(data);
            $.each($("#ulProductGrid li"), function () {
                if ($(this).hasClass("active")) {
                    $(this).removeClass("active");
                }
            });
            $("#liProduct" + data.Id).addClass("active");
            $("#ddlLanguage").attr("disabled", true);

            self.SetEditorValue(data.Description);
            if (self.IsPopupOpen && data.ProductImagesDTOList.length <= 0) {
                numberOfRows = 1;
                self.AddFileUpload();
            }

            // Add Scroll bar
            if (data.ProductImagesDTOList.length > 3) {
                if (!$("#divScroll").hasClass("scroll")) {
                    $("#divScroll").addClass("scroll");
                }
            }
        };

        // Bind product image table from DB
        self.BindImageTableFromDB = function () {
            numberOfRows = 1;
            $('#tblImage tr:not(:first)').remove();
            if (self.model.ProductImagesDTOList().length > 0) {
                $.each(self.model.ProductImagesDTOList(), function (index, productImagesDTO) {
                    var $tr, $clone;
                    index = index + 1;
                    $tr = $("#imageTemplate");
                    $clone = $tr.clone(true);
                    $($clone[0]).removeAttr("style").removeAttr("id");
                    $tr.after($clone[0]);
                    $($clone[0]).attr("data-selector", numberOfRows).attr("id", productImagesDTO.Id);
                    $($($($clone[0]).find("td")[0]).find(":file")[0]).attr("id", "fileMainImage" + numberOfRows).addClass("productImage");
                    $($($($clone[0]).find("td")[1]).find(":file")[0]).attr("id", "fileThumbImage" + numberOfRows).addClass("productThumbImage");

                    $clone.find('.radioIsPrimary').prop("checked", productImagesDTO.IsPrimary);
                    $clone.find('.imagepath').html(productImagesDTO.ImagePath);
                    $clone.find('.thumbimagepath').html(productImagesDTO.ThumbnailPath);
                    if (productImagesDTO !== null) {
                        $($($($($clone[0]).find("td")[0]).find(":file")[0])[0]).addClass("hidden").removeClass("productImage");
                        $($($($($clone[0]).find("td")[1]).find(":file")[0])[0]).addClass("hidden").removeClass("productThumbImage");

                        $($($($($clone[0]).find("td")[0]).find(":text")[0])[0]).addClass("hidden");
                        $($($($($clone[0]).find("td")[1]).find(":text")[0])[0]).addClass("hidden");
                        if (productImagesDTO.ImagePath != null && productImagesDTO.ImagePath != "") {
                            $($($($clone[0]).find("td")[0]).find("img")[0]).removeClass("hidden").attr("src", productImagesDTO.ImagePath);
                        }
                        if (productImagesDTO.ThumbnailPath != null && productImagesDTO.ThumbnailPath != "") {
                            $($($($clone[0]).find("td")[1]).find("img")[0]).removeClass("hidden").attr("src", productImagesDTO.ThumbnailPath);
                        }
                    }
                    numberOfRows = numberOfRows + 1;
                });
            }
        };

        // Delete product
        self.Delete = function () {
            common.ShowLoader();
            if (self.model.IsMapped()) {
                common.AlertBox(common.Messages[languageCode].ProductAlreadyMapped);
                return;
            }
            var id = self.model.Id();
            $.confirm({
                title: "",
                content: common.Messages[languageCode].DeleteProductConfirm,
                confirmButton: common.ButtonText[languageCode].OK,
                cancelButton: common.ButtonText[languageCode].Cancel,
                backgroundDismiss: false,
                confirm: function () {
                    common.HideLoader();
                    common.DeleteAJAXCall(common.APIUrls.DeleteProduct + id, self.DeleteProductSuccess);
                },
                cancel: function () {
                    common.HideLoader();
                },
            });
        };

        // DeleteProduct Success event
        self.DeleteProductSuccess = function () {
            location.reload();
        },

        // Get Language list to fill dropdown box
        self.GetLanguageList = function () {
            common.GetAJAXCall(common.APIUrls.GetLanguage, {}, self.GetLanguageListSuccess, true);
        };

        // Get Language List success
        self.GetLanguageListSuccess = function (data) {
            var array = [];
            $.each(data, function (index, value) {
                index = index + 1;
                array.push(value);
            });
            self.LanguageList(array);
            common.HideLoader();

        },


        // Bind text editor for product description
        self.BindProductDescription = function () {
            $("#txtJQEditor").kendoEditor({
                tools: [
                    "fontName",
                    //"fontSize",
                    "bold",
                    "italic",
                    "underline",
                    "justifyLeft",
                    "justifyCenter",
                    "justifyRight",
                    "indent",
                    "outdent",
                    "insertUnorderedList",
                    "insertOrderedList"
                ],
                keydown: function (e) {
                    // TODO :Enter key names
                    if (this.value().length > 6062 && (e.keyCode !== 37 && e.keyCode !== 39 && e.keyCode !== 38 && e.keyCode !== 40 && e.keyCode !== 8 && e.keyCode !== 9)) {
                        e.preventDefault();
                        return false;
                    }
                },
                paste: function (e) {
                    if (this.value().length > 6062 || e.html.replace(/(<([^>]+)>)/ig, "").length > 6062) {
                        var getMaxLimit = e.html.replace(/(<([^>]+)>)/ig, "").substring(0, 6062);
                        e.html = '';
                        this.value('');
                        e.html = getMaxLimit;
                    }
                }
            });
        };

        // Add a new row for product Image
        self.AddFileUpload = function () {
            var $tr, $clone, getFirstRow;
            if (numberOfRows < 7) {
                // Check first row is blank or not
                getFirstRow = $('#tblImage tr').eq(1);
                if (!$($($(getFirstRow).find("td")[0]).find(":file")[0]).hasClass("hidden") && !$($($(getFirstRow).find("td")[1]).find(":file")[0]).hasClass("hidden")) {
                    if ($($($(getFirstRow).find("td")[0]).find(":file")[0]).val() === "" && $($($(getFirstRow).find("td")[1]).find(":file")[0]).val() === "" && numberOfRows != 1) {
                        common.AlertBox(common.Messages[languageCode].RequiredProductImage);
                        return;
                    }
                }

                // Add Scroll bar
                if ($('#tblImage tr').length > 3) {
                    if (!$("#divScroll").hasClass("scroll")) {
                        $("#divScroll").addClass("scroll");
                    }
                }

                $tr = $("#imageTemplate");
                $clone = $tr.clone(true);
                $($clone[0]).removeAttr("style").removeAttr("id");
                $tr.after($clone[0]);
                $($clone[0]).attr("data-selector", numberOfRows);
                $($($($clone[0]).find("td")[0]).find(":file")[0]).attr("id", "fileMainImage" + numberOfRows).addClass("productImage");
                $($($($clone[0]).find("td")[1]).find(":file")[0]).attr("id", "fileThumbImage" + numberOfRows).addClass("productThumbImage");
                numberOfRows = numberOfRows + 1;
            } else {
                common.AlertBox(common.Messages[languageCode].MaximumNumberOfProductImage);
            }
        };

        // Add product
        self.AddProduct = function () {
            self.IsEdit = false;
            self.model.IsMapped(false);
            numberOfRows = 1;
            self.model.ClearData();
            $("#ddlLanguage").attr("disabled", false);
            $('#tblImage tr:not(:first)').remove();
            $('#myModalLabel').text('Add Product');
            self.model.ValidationEnabled(false);
            self.AddFileUpload();
            self.SetEditorValue('');
            if ($("#divScroll").hasClass("scroll")) {
                $("#divScroll").removeClass("scroll");
            }
        };

        // Load scroll bar
        self.LoadScrollBar = function () {
            $(document).ready(function () {
                $("a[rel='load-content']").click(function (e) {
                    e.preventDefault();
                    var url = $(this).attr("href");
                    $.get(url, function (data) {
                        $(".sidebar .mCSB_container").append(data); //load new content inside .mCSB_container

                        //scroll-to appended content 
                        $(".sidebar").mCustomScrollbar("scrollTo", "h2:last");
                    });
                });

                $(".sidebar").delegate("a[href='top']", "click", function (e) {
                    e.preventDefault();
                    $(".sidebar").mCustomScrollbar("scrollTo", $(this).attr("href"));
                });
            });
        };

        // Cancel event on poopup
        self.Cancel = function () {
            self.IsPopupOpen = false;
            if (self.model.Id() <= 0) {
                self.CancelMessage(common.Messages[languageCode].AddProductPopupCancel);
            } else {
                self.CancelMessage(common.Messages[languageCode].UpdateProductPopupCancel);
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

        $(document).ready(function () {
            // Click on delete product image
            $('.deleteImage').click(function () {
                var tr, afterRemvoedAllProductImages, productImagesId;
                tr = $(this).closest('tr');
                $.confirm({
                    title: "",
                    content: common.Messages[languageCode].DeleteProductImageConfirm,
                    confirmButton: common.ButtonText[languageCode].OK,
                    cancelButton: common.ButtonText[languageCode].Cancel,
                    backgroundDismiss: false,
                    confirm: function () {
                        common.HideLoader();
                        productImagesId = $(tr).attr("id");
                        if (productImagesId !== null) {
                            if (parseInt(productImagesId, 0) > 0) {
                                afterRemvoedAllProductImages = $.grep(self.model.ProductImagesDTOList(), function (productImage) {
                                    return productImage.Id !== parseInt(productImagesId, 0);
                                });

                                self.model.ProductImagesDTOList([]);
                                self.model.ProductImagesDTOList(afterRemvoedAllProductImages);
                            }
                        }
                        $(tr).remove();
                        numberOfRows = numberOfRows - 1;
                    },
                    cancel: function () {
                        common.HideLoader();
                    },
                });
            });

            // To display image path in file uploader
            $('.typefile').change(function () {
                $(this).parent('.browseStyle').children('.browse').val($(this).val());
            });
        });

        // Remove and set class for border of image
        self.SelectProductImage = function (data, target) {
            $('#imgProduct').attr('src', data.ImagePath);
            $('[id*=imageThumbnail]').each(function () {
                $(this).parent().removeClass('borderimageThumnail');
            });
            $(target.currentTarget).parent().addClass('borderimageThumnail');
        };

        // To get next image in image slider
        self.NextImage = function () {
            var imageCount = self.GetImageCount();
            if (self.model.ProductImagesDTOList().length > self.SeekIndex()) {
                self.ImageList(self.model.ProductImagesDTOList().slice(self.SeekIndex(), self.SeekIndex() + imageCount));
                self.SeekIndex(self.SeekIndex() + imageCount);
            }
        };

        // To get previous images in image slider
        self.PreviousImage = function () {
            var imageCount = self.GetImageCount();
            if (self.SeekIndex() > imageCount) {
                self.ImageList(self.model.ProductImagesDTOList().slice(self.SeekIndex() - 2 * imageCount, self.SeekIndex() - imageCount));
                self.SeekIndex(self.SeekIndex() - imageCount);
            }
        };

        // To get number of images to display according to width of window
        self.GetImageCount = function () {
            if (window.innerWidth <= 767) {
                return 2;
            } else if (window.innerWidth == 768) {
                return 2;
            } else if (window.innerWidth <= 1024 && window.innerWidth > 768) {
                return 3;
            } else {
                return 4;
            }
        }
    };

    var productViewModel = new ProductViewModel();
    productViewModel.GetProductList();
    productViewModel.BindProductDescription();
    ko.applyBindings(productViewModel, document.getElementById('DivProduct'));
});

