$(document).ready(function () {
    normal.init({
        domain: "/menu",
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
    normal.getData = function (name = "", status = 1, parentId = -1) {
        showLoading();
        $.ajax({
            url: normal.domain + normal.url.get_list + "name=" + name + "&status=" + status + "&parentId=" + parentId,
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
        var parentId = normal.frm.find('#drParentMenu').val();
        normal.getData(name, status, parentId);
    })
    normal.frm.find(normal.button.create).on('click', function () {
        normal.openCreate("Tạo mới menu");
    });

})