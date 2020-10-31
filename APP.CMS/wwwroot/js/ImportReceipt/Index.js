$(function(){
    getData();
    $('#frmCreate').find('#btnSearch').on('click', function () {
        var time = $('#frmCreate').find('#txtDate').val();
        normal.getData(time);
    });
    $('#frmCreate').find('#btnCreate').on('click', function () {
        location.href = "/don-hang-nhap" + '/tao-moi';
    });
});
function getData(time = "") {
    showLoading();
    $.ajax({
        url: "/don-hang-nhap/get-list?time="+time,
        method: "Get",
        success: function (response) {
            $('#dtTable').html(response);
            hideLoading()
        }
    })
}