
$(function () {
    //create.init({
    //    domain: normal.domain,
    //    frm: $("#frmCreate"),
    //});
    //addRequired(create.frm);
});
$('#btnCreate').off("click").on('click', function (e) {
    e.preventDefault();
    create();
});
$('#btnExist').off("click").on('click', function (e) {
    location.href = "/nhom-quyen" + "/danh-sach";
});
//$(".menu-item").click(function () {
//    $(".list-permission").css("display", "none");
//    var id = $(this).find("label").data('id');
//    $("#menuid_" + id).css("display", "block");
//})
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
    $.ajax({
        url: "/nhom-quyen" + "/create-or-update",
        method: "POST",
        data: {
            Name: $('#frmCreate').find("#txtName").val(),
            Note: $('#frmCreate').find("#txtNote").val(),
            Status: $('#frmCreate').find('#ckbStatus').prop('checked') ? 1 : 2,
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



