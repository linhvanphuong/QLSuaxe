var ListPermission = [];
$(function () {
    //create.init({
    //    domain: normal.domain,
    //    frm: $("#frmUpdate"),
    //});
    //addRequired(create.frm);
    getListPermission();
});
$('#btnCreate').off("click").on('click', function (e) {
    e.preventDefault();
    create();
});
$('#btnExist').off("click").on('click', function (e) {
    location.href = "/nhom-quyen" + "/danh-sach";
});
function getListPermission() {
    $.ajax({
        url: "/nhom-quyen" + '/get-list-permission-by-role?roleId=' + $('#frmUpdate').find("#txtId").val(),
        method: "GET",
        success(response) {
            ListPermission = response;
        }
    }).then(function () {
        setSelectedActionCode();
    });
}
function setSelectedActionCode() {
    $.each(ListPermission, function (i, item) {
        $("#roles").find("#menu_" + item.menuId + " input[type='checkbox']").each(function () {
            var actionCode = $(this).val()
            console.log(actionCode);
            var action = ListPermission.find(x => x.actionCode == actionCode && x.menuId == item.menuId);
            console.log(action);
            if (action != "" && action!= null) {
                $(this).prop("checked", true);
            }
        })
    })
}
function openPermission(id) {
    $(".list-permission").css("display", "none");
    $("#menu_" + id).css("display", "block");
}
function create() {
    //if (ValidateForm(frmCreate)) {
    //    return;
    //}
    var listRolePermission = [];
    $("#listmenu").find("label").each(function (i, item) {
        var menuid = $(item).data('id');
        var roles = $("#roles").find("#menu_" + menuid + " input[type='checkbox']:checked");
        if (roles.length > 0) {
            roles.each(function () {
                var rolePermission = {
                    MenuId: menuid,
                    ActionCode: $(this).val()
                }
                listRolePermission.push(rolePermission);
            })
        }
        else {
            var rolePermission = {
                MenuId: menuid,
                ActionCode: ""
            }
            listRolePermission.push(rolePermission);
        }
    });
    showLoading();
    $.ajax({
        url: "/nhom-quyen" + "/create-or-update",
        method: "POST",
        data: {
            Id: $('#frmUpdate').find("#txtId").val(),
            Name: $('#frmUpdate').find("#txtName").val(),
            Note: $('#frmUpdate').find("#txtNote").val(),
            Status: $('#frmUpdate').find('#ckbStatus').prop('checked') ? 1 : 2,
            Permissions: listRolePermission
        }
        , success: function (response) {
            if (response.result) {
                // datasource = response.data
                showAlert(response.message, 2)
                location.href = "/nhom-quyen" + '/danh-sach';

            } else {
                showAlert(response.message)
            }
        }
    })
}