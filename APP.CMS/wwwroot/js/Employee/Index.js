$(document).ready(function () {
    normal.init({
        domain: "/nhan-vien",
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
    normal.getData = function (name = "", jobPositionId = 0) {
        showLoading();
        $.ajax({
            url: normal.domain + normal.url.get_list + "name=" + name + "&jobPositionId=" + jobPositionId,
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
    }
    normal.getData();
    normal.frm.find(normal.button.search).on('click', function () {
        var name = normal.frm.find('#txtName').val();
        var jobPositionId = normal.frm.find('#drJob').val();
        normal.getData(name, jobPositionId);
    })
    normal.frm.find(normal.button.create).on('click', function () {
        normal.openCreate("Tạo mới nhân viên");
    });

})