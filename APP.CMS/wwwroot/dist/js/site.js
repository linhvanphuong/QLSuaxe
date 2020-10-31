function ValidateForm(frm) {
    var error = false;
    frm.find('input').each(function () {
        if ($(this).attr('required')) {
            if ($(this).hasClass('error')) {
                $(this).removeClass('error');
            }
            if ($(this).val() === '') {
                $(this).addClass('error');
                error = true;

            }
        }
    });
    frm.find('select').each(function () {
        if ($(this).attr('required')) {
            if ($(this).hasClass('error')) {
                $(this).removeClass('error');
            }
            if ($(this).val() === null) {
                $(this).addClass('error');
                error = true;
            }
            if ($(this).val() == 0) {
                $(this).addClass('error');
                error = true;
            }
        }
    });
    frm.find('.select2').each(function () {
        if ($(this).attr('required')) {
            if ($(this).hasClass('error')) {
                $(this).removeClass('error');
            }
            if ($(this).val() === null) {
                $(this).addClass('error');
                error = true;
            }
            if ($(this).val() == 0) {
                $(this).addClass('error');
                error = true;
            }
        }
    });
    if (error) {
        toastr.error('Có mục nhập nào đó đánh dấu * mà bạn chưa nhập đầy đủ dữ liệu. Yêu cầu kiểm tra lại')
    }
    return error;
}
function addRequired(frm) {
    frm.find('label').each(function () {
        if ($(this).attr('required')) {
            $(this).append('<i class="fa fa-asterisk ml-1" aria-hidden="true"></i>')
        }
    });
}

function showAlert(message, error = 1) {
    if (error === 1) {
        toastr.error(message)
    }
    else if (error === 2) {
        toastr.success(message)
    }
    //removeAlert();
}
function removeAlert() {
    if ($('#alert').addClass("show")) {
        setTimeout(function () { $('#alert').removeClass('show') }, 3000);
    }
}
function resetForm(frm) {
    frm.find('input[type="text"]').val('');
    frm.find('select').val('');
}
function fillMonth(select, defaultValue) {
    console.log("create thang")
    //var currentmonth = new Date().getMonth()
    if (select !== null) {
        for (var i = 0; i < _month.length; i++) {
            select.append(`<option value="${_month[i].value}">
                                       ${_month[i].text}
                                  </option>`);
        }
    }
    //if (defaultValue === -1) {
    select.val(defaultValue)
    //}
}
function fillPageSize(div) {
    var html = '<div class="row" style="width:90px"><select id="slPageSize" class="form-control" style="margin-bottom: 10px;">' +
        `<option value="10" active>10</option>` +
        `<option value="20">20</option>` +
        `<option value="30">30</option>` +
        `<option value="50">50</option>` +
        `<option value="100">100</option>` +
        `<option value="10000000">Tất cả</option>` +
        '</select></div >'
    div.html(html)

}
function fillYear(select, defaultValue) {
    var currentyear = new Date().getFullYear()

    for (var i = currentyear - 10; i < currentyear + 1; i++) {
        select.append(`<option value="${i}">
                                       Năm ${i}
                                  </option>`);
    }
    if (defaultValue === 0) {
        select.val(currentyear)
    }
}
var _month = [
    {
        value: 1,
        text: "Tháng 1"
    },
    {
        value: 2,
        text: "Tháng 2"
    },
    {
        value: 3,
        text: "Tháng 3"

    },
    {
        value: 4,
        text: "Tháng 4"
    },
    {
        value: 5,
        text: "Tháng 5"
    },
    {
        value: 6,
        text: "Tháng 6"
    },
    {
        value: 7,
        text: "Tháng 7"
    },
    {
        value: 8,
        text: "Tháng 8"
    },
    {
        value: 9,
        text: "Tháng 9"
    },
    {
        value: 10,
        text: "Tháng 10"
    },
    {
        value: 11,
        text: "Tháng 11"
    },
    {
        value: 12,
        text: "Tháng 12"
    },
]
function showLoading() {
    $('body').loadingModal({
        position: 'auto',
        text: '',
        color: '#fff',
        opacity: '0.7',
        backgroundColor: 'rgb(0,0,0)',
        animation: 'doubleBounce'
    });
}
function hideLoading() {
    $('body').loadingModal('destroy');

}
function stringToSlug(str) {
    // remove accents
    var from = "àáãảạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệđùúủũụưừứửữựòóỏõọôồốổỗộơờớởỡợìíỉĩịäëïîöüûñçýỳỹỵỷ",
        to = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeduuuuuuuuuuuoooooooooooooooooiiiiiaeiiouuncyyyyy";
    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(RegExp(from[i], "gi"), to[i]);
    }

    str = str.toLowerCase()
        .trim()
        .replace(/[^a-z0-9\-]/g, '-')
        .replace(/-+/g, '-');

    return str;
}
function normalVNese(str) {
    var from = "àáãảạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệđùúủũụưừứửữựòóỏõọôồốổỗộơờớởỡợìíỉĩịäëïîöüûñçýỳỹỵỷ",
        to = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeduuuuuuuuuuuoooooooooooooooooiiiiiaeiiouuncyyyyy";
    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(RegExp(from[i], "gi"), to[i]);
    }
    return str;
}
function setDatetime(type) {//Set all element that have class = datepicker to daterangepicker plugin
    $('.datepicker').daterangepicker({ // allow set single datetime
        singleDatePicker: true,
        timePicker: true,      
        locale: {
            format: "DD/MM/YYYY HH:mm:ss",
            cancelLabel: 'Xóa',
            applyLabel: 'Lưu'
        }
    });
    $('.datepicker').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
}
function setDatetimeForCreateContent(type) {//Set all element that have class = datepicker to daterangepicker plugin
    $('.datepicker').daterangepicker({ // allow set single datetime
        singleDatePicker: true,
        startDate: new Date(),
        timePicker: true,
        locale: {
            format: "DD/MM/YYYY HH:mm:ss",
            cancelLabel: 'Xóa',
            applyLabel: 'Lưu'
        }
    });
    $('.datepicker').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
}
function setDatetimeForNhuanButCreate() {
    $('.datepicker').daterangepicker({ // allow set single datetime
        singleDatePicker: true,
        startDate: new Date(),
        timePicker: true,
        locale: {
            format: "MM/YYYY",
            cancelLabel: 'Xóa',
            applyLabel: 'Lưu'
        }
    });
    $('.datepicker').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
}
function setDatetimeForNhuanBut() {
    $('.datepicker').daterangepicker({ // allow set single datetime
        singleDatePicker: true,
        timePicker: true,
        locale: {
            format: "MM/YYYY",
            cancelLabel: 'Xóa',
            applyLabel: 'Lưu'
        }
    });
    $('.datepicker').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
}

function setTextArea() { //Set all element that have class = textarea to summernote plugin
    var BrowserButton = function (context) {
        var ui = $.summernote.ui;
        // create button
        var button = ui.button({
            contents: '<i class="fas fa-server"></i> Thư viện',
            tooltip: 'browser',
            click: function () {
                // invoke insertText method with 'hello' on editor module.
                BrowserServer();
            }
        });
        return button.render();   // return button as jquery object
    }
    $('#txtContent').summernote({
        placeholder: 'Nội dung bài viết',
        tabsize: 2,
        lang: 'vi-VN',
        height: 400,
        fontSizes: ['12', '14', '16', '18', '24', '36', '48'],
        toolbar: [
            ['fontname', ['fontname']],
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']],
            ['insert', ['picture', 'video', 'link', 'table', 'hr']],
            ['misc', ['fullscreen', 'codeview', 'undo', 'redo', 'help']],
            ['mybutton', ['browser']]
        ],
        buttons: {
            browser: BrowserButton
        },
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    UploadImage(files[i]);
                }
            }
        }
    });

}
function convertToDateTime(input) {
    //Convert string DD/MM/YYYY HH:mm:ss to dateObject
    input = input.replaceAll('/', '-')
    var reggie = /(\d{2})-(\d{2})-(\d{4}) (\d{2}):(\d{2}):(\d{2})/;
    var dateArray = reggie.exec(input);
    var dateObject = new Date(
        (+dateArray[3]), //year
        (+dateArray[2]) - 1, //month Careful, month starts at 0!
        (+dateArray[1]), //day
        (+dateArray[4]), // Hours
        (+dateArray[5]), //Minute
        (+dateArray[6]) //second
    );
    return dateObject;
}
function convertToDateTimeForThongKe(input) {
    //Convert string DD/MM/YYYY to dateObject
    input = input.replaceAll('/', '-')
    var reggie = /(\d{2})-(\d{4})/;
    var dateArray = reggie.exec(input);
    var dateObject = new Date(
        (+dateArray[2]), //year
        (+dateArray[1]) - 1, //month Careful, month starts at 0!
    );
    return dateObject;
}



