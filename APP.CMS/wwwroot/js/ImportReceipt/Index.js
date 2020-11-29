$(function(){
    getData();
    $('#frmFilter').find('#btnSearch').on('click', function () {
        var time = $('#frmFilter').find('#txtDate').val();
        getData(time);
    });
    $('#frmFilter').find('#btnCreate').on('click', function () {
        location.href = "/don-hang-nhap" + '/tao-moi';
    });
});
function getData(time = "") {
    showLoading();
    $.ajax({
        url: "/don-hang-nhap/get-list?createdDate="+time,
        method: "Get",
        success: function (response) {
            $('#dtTable').html(response);
            hideLoading()
        }
    })
}