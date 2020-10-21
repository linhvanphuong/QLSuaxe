
$(function () {
    create.init({
        domain: normal.domain,
        frm: $("#frmCreate"),
    });
    addRequired(create.frm);
});
$('#modal-form').find('#btnSave').off('click').on('click', function (e) {
    e.preventDefault();
    create.model = {
        Name: create.frm.find('#txtName').val(),
        Note: create.frm.find('#txtNote').val(),
        Status: create.frm.find('#drStatus').val()
    }
    create.Create();
})
