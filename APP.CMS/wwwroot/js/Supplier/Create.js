
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
        Address: create.frm.find('#txtAddress').val(),
        Phone: create.frm.find('#txtPhone').val(),
        Email: create.frm.find('#txtEmail').val(),
        Website: create.frm.find('#txtWebsite').val()
    }
    create.Create();
})
