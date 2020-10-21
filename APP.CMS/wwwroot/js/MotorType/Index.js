$(document).ready(function () {
    normal.init({
        domain: "/loai-xe",
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
    normal.getData = function (name = "", status = 1, manufactureId = 0) {
        showLoading();
        $.ajax({
            url: normal.domain + normal.url.get_list + "name=" + name + "&status=" + status + "&manufactureId=" + manufactureId,
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
        var status = normal.frm.find('#drStatus').val();
        var jobPositionId = normal.frm.find('#drJob').val();
        normal.getData(name,status, jobPositionId);
    })
    normal.frm.find(normal.button.create).on('click', function () {
        normal.openCreate("Tạo mới loại xe");
    });

})