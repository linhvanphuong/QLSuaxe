$(document).ready(function () {
    normal.init({
        domain: "/phu-tung",
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
    normal.getData = function (name = "") {
        showLoading();
        $.ajax({
            url: normal.domain + normal.url.get_list + "name=" + name,
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
        normal.getData(name);
    })
    normal.frm.find(normal.button.create).on('click', function () {
        normal.openCreate("Tạo mới phụ tùng");
    });

})