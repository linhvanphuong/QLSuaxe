
$(function () {
    create.init({
        domain: normal.domain,
        frm: $("#frmUpdate"),
    });
    addRequired(create.frm);
});
$('#modal-form').find('#btnSave').off('click').on('click', function (e) {
    e.preventDefault();
    var price = create.frm.find('#txtPrice').val();
    if (price % 1 != 0) {
        toastr.error('Giá phải là số nguyên dương');
        return;
    }
    create.model = {
        Id: create.frm.find('#txtId').val(),
        Name: create.frm.find('#txtName').val(),
        Note: create.frm.find('#txtNote').val(),
        Price: create.frm.find('#txtPrice').val(),
        Status: create.frm.find('#ckbStatus').prop("checked") ? 1 : 2
    }
    create.Create();
})