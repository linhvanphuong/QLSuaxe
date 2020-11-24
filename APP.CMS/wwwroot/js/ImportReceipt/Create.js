$(function () {
    var frmCreate = $('#frmCreate');
    addRequired(frmCreate);
    $('select').select2();
    $('select').val('');
    $('#btnCreate').on('click',function () {
        createBill();
    })
    $('#btnExist').on('click', function () {
        location.href = "/don-hang-nhap/danh-sach"
    });
});
var listAssSTT = 0;
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
            url: "/don-hang-nhap" + "/accessory-info?id=" + this.value,
            method: "Get",
            success: function (response) {
                if (response.result) {
                    var option2 = response.data;
                    listAssSTT++;
                    var trAppend2 = '<tr ><td style="width:10%;text-align:center;">' + '<a class="AccXoa" href="javascript:;"><i class="fas fa-trash-alt"></i></a>' + '</td><td style="width:30%">' + option2.name + '</td>' +
                        '<td class="AssId" style="width:10%">' + option2.id + '</td>' + '<td class="AssPrice" style="width:10%">' + '<input class="AssImportPrice"  onblur="changeThanhTien(' + option2.id + ',this)"  type="number" min="0" step="1" value="0" id="row2SL_' + option2.id + '" >' + '</td>' +
                        '<td class="AssQuantity" style="width:10%">' + '<input type="number" data-max="' + option2.quantity + '" onblur="changeThanhTien(' + option2.id + ',this)" min="1" step="1"  value="1" id="row2number_' + option2.id + '">' + '</td>' + '<td style="width: 10% ">' + option2.unit + '</td>' +
                        '<td style="width:20%"><p class="TongTien" id="TT_' + option2.id + '"> 0 </td>';
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
    }
});

function changeThanhTien(id,sender) {
    if (sender.value % 1 != 0) {
        showAlert("Giá trị phải là số nguyên dương");
        sender.value = 0;
    }
    if (sender.value < 0) {
        showAlert("Giá trị phải là số nguyên dương")
        sender.value = 0;
    }
    var sl = $('#row2number_' + id).val();
    var dongianhap = $('#row2SL_' + id).val();
    var thanhTien = 0;
    var TT = "";
    thanhTien = sl * dongianhap;
    TT = thanhTien + "";
    $('#TT_' + id).text('');
    $('#TT_' + id).text(TT);
    TinhTongTien();
}
function openCreateSuplier() {
    $.ajax({
        url: "/don-hang-nhap" +"/tao-moi-ncc",
        method: "Get",
        success: function (response) {
            showContentModal(response, "Tạo mới nhà cung cấp")
        }
    });
}
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
    var listAss = []
    $('#listAccessories tbody tr').each(function () {
        var ass = {};
        $(this).find('td').each(function () {
            if ($(this).hasClass('AssId')) {
                ass.AccesoryId = $(this).text();
            }
            if ($(this).hasClass('AssPrice')) {
                ass.ImportPrice = $(this).find('input').val();
            }
            if ($(this).hasClass('AssQuantity')) {
                ass.Quantity = $(this).find('input').val();
            }
        })
        listAss.push(ass);
    });
    if (listAss.length == 0) {
        showAlert("Chưa có phụ tùng nhập");
        return;
    }
    var model = {
        SupplierId: $('#frmCreate').find('#drSuplier').val(),
        CreatedDate: $('#frmCreate').find('#txtCreatedDate').text(),
        listAccessory: listAss,
        UpdatedBy: $('#frmCreate').find('#drKTV').val()
    }
    showLoading()
    $.ajax({
        url: "/don-hang-nhap/create-or-update",
        method: "POST",
        data: model
        , success: function (response) {
            hideLoading()
            if (response.result) {
                // datasource = response.data
                showAlert(response.message, 2)
                location.href = "/don-hang-nhap/danh-sach"

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
