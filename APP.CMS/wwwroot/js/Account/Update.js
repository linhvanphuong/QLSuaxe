
$(function () {
    create.init({
        domain: normal.domain,
        frm: $("#frmUpdate"),
    });
    create.frm.find('#txtRole').select2()
    addRequired(create.frm);
});
$('#modal-form').find('#btnSave').off('click').on('click', function (e) {
    e.preventDefault();
    create.model = {
        Id: create.frm.find('#txtId').val(),
        EmployeeId: create.frm.find('#txtEmployee').val(),
        UserName: create.frm.find('#txtName').val(),
        Password: create.frm.find('#txtPassword').val(),
        ListRole: create.frm.find('#txtRole').val(),
        Status: create.frm.find('#ckbStatus').prop("checked") ? 1 : 2
    }
    create.Create();
})