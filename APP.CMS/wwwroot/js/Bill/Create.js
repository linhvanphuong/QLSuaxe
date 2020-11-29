$(function () {
    var frmCreate = $('#frmCreate');
    addRequired(frmCreate);
    $('select').select2();
    $('select').val('');
    $('#btnCreate').on('click',function () {
        createBill();
    })
    $('#btnExist').on('click', function () {
        location.href = "/hoa-don/danh-sach"
    });
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
$('#frmCreate').find('#drCustomer').on('change', function () {
    var phone = $(this).find(':selected').data('phone');
    $('#frmCreate').find('#txtPhone').val('');
    $('#frmCreate').find('#txtPhone').val(phone);
})
$('#frmCreate').find('#txtPhone').on('blur', function () {
    var phone = $(this).val();
    if (phone != ""){
        $('#frmCreate').find('#drCustomer').find('option').each(function () {
            if ($(this).data('phone') == phone) {
                $('#frmCreate').find('#drCustomer').val($(this).val()).trigger('change');
            }
        })
    } 
});
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
        url: "/hoa-don" +"/tao-moi-kh",
        method: "Get",
        success: function (response) {
            showContentModal(response, "Tạo mới khách hàng")
        }
    });
}
function openCreateMTType() {
    $.ajax({
        url: "/hoa-don" + "/tao-moi-loai-xe",
        method: "Get",
        success: function (response) {
            showContentModal(response, "Tạo mới loại xe")
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
function createBill() {
    if (ValidateForm($('#frmCreate'))) {
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
        MotorLiftId: $('#frmCreate').find('#drMotorLift').val(),
        CustomerId: $('#frmCreate').find('#drCustomer').val(),
        MotorTypeId: $('#frmCreate').find('#drMotorType').val(),
        TimeIn: $('#frmCreate').find('#txtTimeIn').text(),
        Status: $('#txtStatus').val(),
        Note: $('#frmCreate').find('#txtNote').val(),
        ListBill_Services: listSv,
        ListBill_Accessories: listAss,
        UpdatedBy: $('#frmCreate').find('#drKTV').val()
    }
    showLoading()
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
    $('#txtTongTien').text(TongTien +" VNĐ");
}
