$(function () {
    var frmCreate = $('#frmUpdate');
    addRequired(frmCreate);
    $('select').select2();
    $('#btnCreate').on('click', function () {
        updateBill();
    })
    $('#btnSentToKTV').on('click', function () {
        sentToKTV();
    })
    $('#btnExist').on('click', function () {
        location.href = "/hoa-don/danh-sach"
    })
});
window.addEventListener('load', function () {
    TinhTongTien();
})
$('#frmUpdate').find('#drCustomer').on('change', function () {
    var phone = $(this).find(':selected').data('phone');
    $('#frmUpdate').find('#txtPhone').val('');
    $('#frmUpdate').find('#txtPhone').val(phone);
})
$('#frmUpdate').find('#txtPhone').on('blur', function () {
    var phone = $(this).val();
    if (phone != "") {
        $('#frmUpdate').find('#drCustomer').find('option').each(function () {
            if ($(this).data('phone') == phone) {
                $('#frmUpdate').find('#drCustomer').val($(this).val()).trigger('change');
            }
        })
    }
});
$('#drServices').on('change', function () {
    if (this.value != 0) {
        $.ajax({
            url: "/hoa-don" + "/service-info?id=" + this.value,
            method: "Get",
            success: function (response) {
                //if (response.result) {
                //var option1 = response.data;
                //var trAppend1 = '<tr><td style="width:10%;text-align:center;">' + '<a class="SerXoa" href="javascript:;"><i class="fas fa-trash-alt"></i></a>' + '</td><td style="width:40%">' + option1.name + '</td>' +
                //    '<td class="serviceId" style="width:10%">' + option1.id + '</td>' + '<td class="servicePrice" style="width:20%">' + option1.price + ' VNĐ </td>' +
                //    '<td class="TongTien" style="width:20%">' + option1.price + ' VNĐ</td>';
                $('#listService tbody').append(response);
                $('#drServices').val('0').trigger('change');
                //}
                //else {
                //    showAlert("Lỗi", 1);
                //}
            },
            error: function (response) {
                showAlert("lỗi", response.message)
                $('#drServices').val('0').trigger('change');
            }
        }).then(function () {
            TinhTongTien()
        })
    }
});
$('#drAccessories').on('change', function () {
    if (this.value != 0) {
        var accId = this.value;
        var checkDuplicate = false;
        $('#listAccessories tbody').find('.AssId').each(function () {
            var accIdTd = $(this).text().trim();
            if (accIdTd == accId) {
                checkDuplicate = true;
            }
        });
        console.log(checkDuplicate);
        if (checkDuplicate) {
            return;
        }
        $.ajax({
            url: "/hoa-don" + "/accessory-info?id=" + this.value,
            method: "Get",
            success: function (response) {
                //if (response.result) {
                //    var option2 = response.data;
                //    var trAppend2 = '<tr ><td style="width:10%;text-align:center;">' + '<a class="AccXoa" href="javascript:;"><i class="fas fa-trash-alt"></i></a>' + '</td><td style="width:30%">' + option2.name + '</td>' +
                //        '<td class="AssId" style="width:10%">' + option2.id + '</td>' + '<td class="AssPrice" style="width:10%">' + option2.price + ' VNĐ </td>' +
                //        '<td class="AssQuantity" style="width:10%">' + '<input type="number" data-max="' + option2.quantity + '" onblur="changeThanhTien(' + option2.id + ',' + option2.price + ')" min="1" step="1"  value="1" max="' + option2.quantity + '" id="row2number_' + option2.id + '">' + '</td>' + '<td style="width: 10% ">' + option2.unit + '</td>' +
                //        '<td style="width:20%"><p class="TongTien" id="TT_' + option2.id + '">' + option2.price + ' VNĐ</td>';
                $('#listAccessories tbody').append(response);
                $('#drAccessories').val('0').trigger('change');
                //}
                //else {
                //    showAlert("Lỗi", 1);
                //}
            },
            error: function (response) {
                showAlert("lỗi", response.message)
                $('#drAccessories').val('0').trigger('change');
            }
        }).then(function () {
            TinhTongTien();
        })
    }
});
//var listSvSTT = 0;
//$('#drServices').on('change', function () {
//    $.ajax({
//        url: "/hoa-don" + "/service-info?id=" + this.value,
//        method: "Get",
//        success: function (response) {
//            if (response.result) {
//                var option1 = response.data;
//                console.log(option1);
//                listSvSTT++;
//                var trAppend1 = '<tr><td style="width:10%;text-align:center;">' + '<a class="SerXoa" href="javascript:;"><i class="fas fa-trash-alt"></i></a>' + '</td><td style="width:40%">' + option1.name + '</td>' +
//                    '<td class="serviceId" style="width:10%">' + option1.id + '</td>' + '<td class="servicePrice" style="width:20%">' + option1.price + ' VNĐ </td>' +
//                    '<td class="TongTien" style="width:20%">' + option1.price + ' VNĐ</td>';
//                $('#listService tbody').append(trAppend1);
//                $('#drServices').val('0').trigger('change');
//            }
//            else {
//                showAlert("Lỗi", 1);
//            }
//        }
//    }).then(function () {
//        TinhTongTien();
//    })
//});
//var listAssSTT = 0;
//$('#drAccessories').on('change', function () {
//    $.ajax({
//        url: "/hoa-don" + "/accessory-info?id=" + this.value,
//        method: "Get",
//        success: function (response) {
//            if (response.result) {
//                var option2 = response.data;
//                listAssSTT++;
//                var trAppend2 = '<tr ><td style="width:10%;text-align:center;">' + '<a class="AccXoa" href="javascript:;"><i class="fas fa-trash-alt"></i></a>' + '</td><td style="width:30%">' + option2.name + '</td>' +
//                    '<td class="AssId" style="width:10%">' + option2.id + '</td>' + '<td class="AssPrice" style="width:10%">' + option2.price + ' VNĐ </td>' +
//                    '<td class="AssQuantity" style="width:10%">' + '<input type="number" data-max="' + option2.quantity + '" onblur="changeThanhTien(' + option2.id + ',' + option2.price + ')" min="1" step="1"  value="1" max="' + option2.quantity + '" id="row2number_' + option2.id + '">' + '</td>' + '<td style="width: 10% ">' + option2.unit + '</td>' +
//                    '<td style="width:20%"><p class="TongTien" id="TT_' + option2.id + '">' + option2.price + ' VNĐ</td>';
//                $('#listAccessories tbody').append(trAppend2);
//                $('#drAccessories').val('0').trigger('change');
//            }
//            else {
//                showAlert("Lỗi", 1);
//            }
//        }
//    }).then(function () {
//        TinhTongTien();
//    })
//});
function changeThanhTien(id, price,sender) {
    var sl = $('#row2number_' + id).val();
    var thanhTien = 0;
    var TT = "";
    if (sl % 1 != 0) {
        showAlert("Giá trị phải là số nguyên dương", 1);
        $('#row2number_' + id).val('1');
        sl = 1;
        thanhTien = sl * price;
        TT = thanhTien + "";
        $('#TT_' + id).text('');
        $('#TT_' + id).text(TT);
        TinhTongTien();
        return;
    }
    if (sl < 0) {
        showAlert("Giá trị phải là số nguyên dương", 1);
        $('#row2number_' + id).val('1');
        sl = 1;
        thanhTien = sl * price;
        TT = thanhTien + "";
        $('#TT_' + id).text('');
        $('#TT_' + id).text(TT);
        TinhTongTien();
        return;
    }
    if (sl > $('#row2number_' + id).data('max')) {
        showAlert("Vượt quá số lượng phụ tùng còn lại", 1);
        $('#row2number_' + id).val('1');
        sl = 1;
        thanhTien = sl * price;
        TT = thanhTien + "";
        $('#TT_' + id).text('');
        $('#TT_' + id).text(TT);
        TinhTongTien();
        return;
    }
    thanhTien = sl * price;
    TT = thanhTien + "";
    $('#TT_' + id).text('');
    $('#TT_' + id).text(TT);
    TinhTongTien();
}
function openCreateCustomer() {
    $.ajax({
        url: "/hoa-don" + "/tao-moi-kh",
        method: "Get",
        success: function (response) {
            showContentModal(response, "Tạo mới khách hàng")
        }
    });
}
$("#listService").on('click', '.SerXoa', function (e) {
    var whichtr = $(this).closest("tr");
    whichtr.remove()
    TinhTongTien();
});
$("#listAccessories").on('click', '.AccXoa', function (e) {
    var whichtr = $(this).closest("tr");
    check = 1;
    whichtr.remove();
    TinhTongTien();
});
function openCreateMTType() {
    $.ajax({
        url: "/hoa-don" + "/tao-moi-loai-xe",
        method: "Get",
        success: function (response) {
            showContentModal(response, "Tạo mới khách hàng")
        }
    });
}
function updateBill() {
    if (ValidateForm($('#frmUpdate'))) {
        return;
    }
    var listSv = []
    $('#listService tbody tr').each(function () {
        var sv = {};
        $(this).find('td').each(function () {
            if ($(this).hasClass('serviceId')) {
                sv.ServiceId = $(this).text();
            }
            if ($(this).hasClass('servicePrice')) {
                sv.ServicePrice = $(this).text().replace("VNĐ", "").trim();
            }
        })
        listSv.push(sv);
    });
    var listAss = []
    $('#listAccessories tbody tr').each(function () {
        var ass = {};
        $(this).find('td').each(function () {
            if ($(this).hasClass('AssId')) {
                ass.AccesaryId = $(this).text();
            }
            if ($(this).hasClass('AssPrice')) {
                ass.AccesaryPrice = $(this).text().replace("VNĐ", "").trim();
            }
            if ($(this).hasClass('AssQuantity')) {
                ass.Quantity = $(this).find('input').val();
            }
        })
        listAss.push(ass);
    });
    var TimeInResult = "";
    var timeIn = $('#frmCreate').find('#txtTimeIn').text();
    var a = timeIn.split(" ");
    var TimeInDate = a[0];
    var b = TimeInDate.split("/");
    TimeInResult = b[2] + "/" + b[1] + "/" + b[0] + " " + a[1];
    var model = {
        Id: $('#frmUpdate').find('#txtId').text(),
        MotorLiftId: $('#frmUpdate').find('#drMotorLift').val(),
        CustomerId: $('#frmUpdate').find('#drCustomer').val(),
        MotorTypeId: $('#frmUpdate').find('#drMotorType').val(),
        TimeIn: TimeInResult,
        Status: $('#txtStatus').val(),
        Note: $('#frmUpdate').find('#txtNote').val(),
        ListBill_Services: listSv,
        ListBill_Accessories: listAss,
        CreatedBy: $('#frmUpdate').find('#txtCreatedBy').data('id'),
        UpdatedBy: $('#frmUpdate').find('#drKTV').val()
    }
    showLoading();
    $.ajax({
        url: "/hoa-don/create-or-update",
        method: "POST",
        data: model
        , success: function (response) {
            hideLoading()
            if (response.result) {
                // datasource = response.data
                showAlert(response.message, 2)
                location.href = "/hoa-don/danh-sach"

            } else {
                showAlert(response.message)
            }
        }
    });
}
function sentToKTV() {
    if (ValidateForm($('#frmUpdate'))) {
        return;
    }
    var listSv = []
    $('#listService tbody tr').each(function () {
        var sv = {};
        $(this).find('td').each(function () {
            if ($(this).hasClass('serviceId')) {
                sv.ServiceId = $(this).text();
            }
            if ($(this).hasClass('servicePrice')) {
                sv.ServicePrice = $(this).text().replace("VNĐ", "").trim();
            }
        })
        listSv.push(sv);
    });
    var listAss = []
    $('#listAccessories tbody tr').each(function () {
        var ass = {};
        $(this).find('td').each(function () {
            if ($(this).hasClass('AssId')) {
                ass.AccesaryId = $(this).text();
            }
            if ($(this).hasClass('AssPrice')) {
                ass.AccesaryPrice = $(this).text().replace("VNĐ", "").trim();
            }
            if ($(this).hasClass('AssQuantity')) {
                ass.Quantity = $(this).find('input').val();
            }
        })
        listAss.push(ass);
    });
    var model = {
        Id: $('#frmUpdate').find('#txtId').text(),
        MotorLiftId: $('#frmUpdate').find('#drMotorLift').val(),
        CustomerId: $('#frmUpdate').find('#drCustomer').val(),
        MotorTypeId: $('#frmUpdate').find('#drMotorType').val(),
        TimeIn: $('#frmUpdate').find('#txtTimeIn').text(),
        Status: $('#btnSentToKTV').data('stt'),
        Note: $('#frmUpdate').find('#txtNote').val(),
        ListBill_Services: listSv,
        ListBill_Accessories: listAss,
        CreatedBy: $('#frmUpdate').find('#txtCreatedBy').data('id'),
        UpdatedBy: $('#frmUpdate').find('#drKTV').val()
    }
    showLoading();
    $.ajax({
        url: "/hoa-don/create-or-update",
        method: "POST",
        data: model
        , success: function (response) {
            hideLoading()
            if (response.result) {
                // datasource = response.data
                showAlert(response.message, 2)
                location.href = "/hoa-don/danh-sach"
            } else {
                showAlert(response.message)
            }
        }
    });
}
function TinhTongTien() {
    var TongTien = 0;
    $('.TongTien').each(function () {
        TongTien = TongTien + parseInt($(this).text().replace("VNĐ", "").trim());
    });
    $('#txtTongTien').text('');
    $('#txtTongTien').text(TongTien + " VNĐ");
}