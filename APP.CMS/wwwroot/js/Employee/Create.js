
$(function () {
    create.init({
        domain: normal.domain,
        frm: $("#frmCreate"),
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
        Name: create.frm.find('#txtName').val(),
        JobPositionId: create.frm.find('#drJob').val(),
        Birth: create.frm.find('#txtNS').val(),
        Sex: sex == 1 ? true : false,
        JoinedDate: create.frm.find('#txtJoinedDate').val(),
        IdentityId: create.frm.find('#txtPhone').val(),
        Phone: create.frm.find('#txtPhone').val(),
        Address: create.frm.find('#txtAddress').val(),
        Note: create.frm.find('#txtNote').val()
    }
    create.Create();
})
