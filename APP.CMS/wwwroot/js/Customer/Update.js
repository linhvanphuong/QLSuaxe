
$(function () {
    create.init({
        domain: normal.domain,
        frm: $("#frmUpdate"),
    });
    addRequired(create.frm);
});
$('#modal-form').find('#btnSave').off('click').on('click', function (e) {
    e.preventDefault();
    var sex = 0;
    $(create.frm.find("[name='radioSex']")).each(function (index) {
        if ($(this.checked)) {
            sex = $(this).val();
        }
    })
    create.model = {
        Id: create.frm.find('#hdId').val(),
        Name: create.frm.find('#txtName').val(),
        Birth: create.frm.find('#txtNS').val(),
        Sex: sex == 1 ? true : false,
        Phone: create.frm.find('#txtPhone').val(),
        Address: create.frm.find('#txtAddress').val(),
        Status: create.frm.find('#ckbStatus').prop("checked") ? 1 : 2
    }
    create.Create();
})