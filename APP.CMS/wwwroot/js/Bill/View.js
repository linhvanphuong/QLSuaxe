$(function () {
    $('#btnExist').click(function () {
        location.href = "/hoa-don/danh-sach";
    })
})
window.addEventListener('load', function () {
    TinhTongTien();
})
function TinhTongTien() {
    var TongTien = 0;
    $('.TongTien').each(function () {
        TongTien = TongTien + parseInt($(this).text().replace("VNĐ", "").trim());
    });
    TongTien = new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(TongTien).replace("₫", "").trim();
    $('#txtTongTien').text('');
    $('#txtTongTien').text(TongTien + " VNĐ");
}
$('#btnCreate').on('click', function () {
    updateStatus($('#txtId').text(), 4);
})
function updateStatus(id, status) {
    window.print();
    //$.ajax({
    //    url: "/hoa-don/update-status?id=" + id + "&status=" + status,
    //    method: "Get",
    //})
    //window.onafterprint = (event) => {
    //    $.ajax({
    //        url: "/hoa-don/update-status?id=" + id + "&status=" + status,
    //        method: "Get",
    //    })
    //};
}