$(document).ready(function () {
    normal.init({
        domain: "/khach-hang",
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
    normal.getData = function (name = "", phone = "") {
        showLoading();
        $.ajax({
            url: normal.domain + normal.url.get_list + "name=" + name + "&phone=" + phone,
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
        var phone = normal.frm.find('#txtPhone').val();
        normal.getData(name, phone);
    })
    normal.frm.find(normal.button.create).on('click', function () {
        normal.openCreate("Tạo mới khách hàng");
    });

})