$(document).ready(function () {

    $('#loginbtn').off("click").on('click', function (e) {
        DangNhap();
    })

});
function DangNhap() {
    showLoading();
    $.ajax({
        url: "/tai-khoan" + "/dang-nhap",
        type: "POST",
        data: {
            UserName: $('#userName').val(),
            Password: $('#password').val()
        }
        , success: function (response) {
            hideLoading();
            if (response.result == true) {
                showAlert(response.message, 2);
                $(location).attr('href', "/")
            }
            else {
                showAlert(response.message, 1);
            }
        }
    });
}