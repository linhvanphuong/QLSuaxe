
var normal = {
    domain: "",
    frm: "",
    url: {
        get_list: "",
        open_Create: "",
        open_Update: "",
        open_Delete: "",
        create_or_update:"",
    },
    element :{
        take : 10,
        skip : 0
    },
    button : {
        create : "",
        search: ""
    },
    table: "",
    init: function (options) {
        normal.domain = options.domain;
        normal.frm = options.frm;
        normal.url = options.url;
        normal.button = options.button;
        normal.table = options.table;
    },
    getData: function (name = "", status = 1) {
        showLoading();
        $.ajax({
            url: normal.domain + normal.url.get_list + "name=" + name + "&status=" + status,
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
    openCreate: function (title) {
        $.ajax({
            url: normal.domain + normal.url.open_Create,
            method: "Get",
            success: function (response) {
                showContentModal(response, title)
            }
        });
    },
    openUpdate: function (id, title) {
        $.ajax({
            url: normal.domain + normal.url.open_Update +"id="+ id,
            method: "Get",
            success: function (response) {
                showContentModal(response, title)
            }
        });
    },
    openDelete: function (id) {
        showDeleteModal("Bạn muốn xóa");
        $('#modal-delete').find('#btnDelete').off("click").on('click', function (e) {
            e.preventDefault();
            console.log("a");
            $.ajax({
                url: normal.domain + normal.url.open_Delete +"id="+id,
                method: "Get",
                success: function (response) {
                    if (response.result) {
                        $("#row_" + id).slideUp();
                        showAlert("Xóa dữ liệu thành công", 2)
                        hideDeleteModal()

                    } else {
                        showAlert(response.message)
                    }
                }
            });
        });
    },
}
var list = {
    frm: "",
    table: "",
    init: function (options) {
        list.frm = options.frm;
        list.table = options.table;
    }
}
var create = {
    domain: "",
    frm: "",
    model: {

    },
    init: function (options) {
        create.domain = options.domain;
        create.frm = options.frm;
        create.button = options.frm;
    },
    Create: function () {
        if (ValidateForm(create.frm)) {
            return;
        }
        $.ajax({
            url: normal.domain + normal.url.create_or_update,
            method: "POST",
            data: create.model
            , success: function (response) {
                hideLoading()
                if (response.result) {
                    // datasource = response.data
                    showAlert(response.message, 2)
                    normal.getData();
                    hideContentModal()

                } else {
                    showAlert(response.message)
                }
            }
        });
    }
}
var update = {
    domain: "",
    frm: "",
    model: {

    },
    init: function (options) {
        update.domain = options.domain;
        update.frm = options.frm;
        update.button = options.frm;
    },
    Update: function () {
        if (ValidateForm(update.frm)) {
            return;
        }
        $.ajax({
            url: normal.domain + normal.url.create_or_update,
            method: "POST",
            data: update.model
            , success: function (response) {
                hideLoading()
                if (response.result) {
                    // datasource = response.data
                    showAlert(response.message, 2)
                    normal.getData();
                    hideContentModal()

                } else {
                    showAlert(response.message)
                }
            }
        });
    }
}