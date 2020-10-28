$(document).ready(function () {
    normal.init({
        domain: "/hoa-don",
        frm: $('#frmFilter'),
        url: {
            get_list: "/get-list?",
            open_Create: "/tao-moi",
            open_Update: "/cap-nhat?",
            open_Delete: "/delete?",
            create_or_update: "/create-or-update"
        },
        button: {
            create: "#btnCreate",
            search: "#btnSearch"
        },
        table: $('#dtTable')
    });
    var stt =  $('#txtStatushd').val();
    normal.getData = function (time = "", status) {
        showLoading();
        $.ajax({
            url: normal.domain + normal.url.get_list + "time=" + time + "&status=" + status,
            method: "Get",
            data: {
                take: normal.element.take,
                skip: normal.element.skip
            }
            , success: function (response) {
                normal.table.html(response);
                hideLoading()
            }
        })
    },
    normal.getData("",stt);
    normal.frm.find(normal.button.search).on('click', function () {
        var time = normal.frm.find('#txtDate').val();
        var status = normal.frm.find('#drStatus').val();
        normal.getData(time, status);
    })
    normal.frm.find(normal.button.create).on('click', function () {
        location.href = normal.domain + '/tao-moi';
    });
});