$(document).ready(function () {
    normal.init({
        domain: "/hang-xe",
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
    normal.getData();
    normal.frm.find(normal.button.search).on('click', function () {
        var name = normal.frm.find('#txtName').val();
        var status = normal.frm.find('#drStatus').val();
        normal.getData(name, status);
    })
    normal.frm.find(normal.button.create).on('click', function () {
        normal.openCreate("Tạo mới hãng xe");
    });

})