$(function () {
    var frmCreate = $('#frmUpdate');
    addRequired(frmCreate);
    $('select').select2();
    $('select').val('');
    $('#btnCreate').on('click', function () {
        createBill();
    })
    $('#btnExist').on('click', function () {
        location.href = "/don-hang-nhap/danh-sach"
    })
});
window.addEventListener('load', function () {
    TinhTongTien();
})
function TinhTongTien() {
    var TongTien = 0;
    $('.TongTien').each(function () {
        TongTien = TongTien + parseInt($(this).text().replace("VNĐ", "").trim());
    });
    $('#txtTongTien').text('');
    $('#txtTongTien').text(TongTien + " VNĐ");
}