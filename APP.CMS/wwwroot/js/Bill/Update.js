$(function () {
    var frmCreate = $('#frmUpdate');
    addRequired(frmCreate);
    $('select').select2();
    $('select').val('');
    $('#btnCreate').on('click', function () {
        createBill();
    })
    $('#btnExist').on('click', function () {
        location.href = "/hoa-don/danh-sach-phieu-tam-tinh"
    })
});
window.addEventListener('load', function () {
    TinhTongTien();
})
var listSvSTT = 0;
$('#drServices').on('change', function () {
    $.ajax({
        url: "/hoa-don" + "/service-info?id=" + this.value,
        method: "Get",
        success: function (response) {
            if (response.result) {
                var option1 = response.data;
                console.log(option1);
                listSvSTT++;
                var trAppend1 = '<tr><td style="width:10%;text-align:center;">' + '<a class="SerXoa" href="javascript:;"><i class="fas fa-trash-alt"></i></a>' + '</td><td style="width:40%">' + option1.name + '</td>' +
                    '<td class="serviceId" style="width:10%">' + option1.id + '</td>' + '<td class="servicePrice" style="width:20%">' + option1.price + ' VNĐ </td>' +
                    '<td class="TongTien" style="width:20%">' + option1.price + ' VNĐ</td>';
                $('#listService tbody').append(trAppend1);
                $('#drServices').val('0').trigger('change');
            }
            else {
                showAlert("Lỗi", 1);
            }
        }
    }).then(function () {
        TinhTongTien();
    }) 
});
var listAssSTT = 0;
$('#drAccessories').on('change', function () {
    $.ajax({
        url: "/hoa-don" + "/accessory-info?id=" + this.value,
        method: "Get",
        success: function (response) {
            if (response.result) {
                var option2 = response.data;
                listAssSTT++;
                var trAppend2 = '<tr ><td style="width:10%;text-align:center;">' + '<a class="AccXoa" href="javascript:;"><i class="fas fa-trash-alt"></i></a>' + '</td><td style="width:30%">' + option2.name + '</td>' +
                    '<td class="AssId" style="width:10%">' + option2.id + '</td>' + '<td class="AssPrice" style="width:10%">' + option2.price + ' VNĐ </td>' +
                    '<td class="AssQuantity" style="width:10%">' + '<input type="number" data-max="' + option2.quantity + '" onblur="changeThanhTien(' + option2.id + ',' + option2.price + ')" min="1" step="1"  value="1" max="' + option2.quantity + '" id="row2number_' + option2.id + '">' + '</td>' + '<td style="width: 10% ">' + option2.unit + '</td>' +
                    '<td style="width:20%"><p class="TongTien" id="TT_' + option2.id + '">' + option2.price + ' VNĐ</td>';
                $('#listAccessories tbody').append(trAppend2);
                $('#drAccessories').val('0').trigger('change');
            }
            else {
                showAlert("Lỗi", 1);
            }
        }
    }).then(function () {
        TinhTongTien();
    }) 
});
function changeThanhTien(id, price) {
    console.log(id);
    var sl = $('#row2number_' + id).val();
    var thanhTien = 0;
    var TT = "";
    if (sl > $('#row2number_' + id).data('max')) {
        showAlert("Vượt quá số lượng phụ tùng còn lại", 1);
        $('#row2number_' + id).val('1');
        sl = 1;
        thanhTien = sl * price;
        TT = thanhTien + " VNĐ";
        $('#TT_' + id).text('');
        $('#TT_' + id).text(TT);
        TinhTongTien();
        return;
    }
    thanhTien = sl * price;
    TT = thanhTien + " VNĐ";
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
function createBill() {
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
        Id: $('#frmUpdate').find('#txtId').val(),
        MotorLiftId: $('#frmUpdate').find('#txtMotorLift').data('id'),
        CustomerId: $('#frmUpdate').find('#txtCustomer').data('id'),
        MotorTypeId: $('#frmUpdate').find('#txtMotorType').data('id'),
        TimeIn: $('#frmUpdate').find('#txtTimeIn').text(),
        Status: $('#txtStatushd').val(),
        Note: $('#frmUpdate').find('#txtNote').val(),
        ListBill_Services: listSv,
        ListBill_Accessories: listAss,
        CreatedBy: $('#frmUpdate').find('#txtCreatedBy').data('id'),
        UpdatedBy: $('#frmUpdate').find('#txtUpdatedBy').data('id')
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
                if (model.Status == 3) {
                    location.href = "/danh-sach-hoa-don/xem?id=" + model.Id;
                }
                location.href = "/hoa-don/danh-sach-phieu-tam-tinh"
                

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