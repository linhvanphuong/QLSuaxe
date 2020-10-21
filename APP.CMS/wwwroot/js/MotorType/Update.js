
$(function () {
    create.init({
        domain: normal.domain,
        frm: $("#frmUpdate"),
    });
    addRequired(create.frm);
});
$('#modal-form').find('#btnSave').off('click').on('click', function (e) {
    e.preventDefault();

    create.model = {
        Id: create.frm.find('#txtId').val(),
        Name: create.frm.find('#txtName').val(),
        MotorManufactureID: create.frm.find('txtManufac').val(),
        Status: create.frm.find('#ckbStatus').prop("checked") ? 1 : 2
    }
    create.Create();
})