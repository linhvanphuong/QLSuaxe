
$(function () {
    create.init({
        domain: normal.domain,
        frm: $("#frmCreate"),
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
    var quantity = create.frm.find('#txtQuantity').val();
    if (quantity % 1 != 0) {
        toastr.error('Giá phải là số nguyên dương');
        return;
    }
    create.model = {
        Name: create.frm.find('#txtName').val(),
        Quantity: create.frm.find('#txtQuantity').val(),
        Price: create.frm.find('#txtPrice').val(),
        Unit: create.frm.find('#txtUnit').val()
    }
    create.Create();
})
