﻿
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
        ParentId: create.frm.find('#drParentMenu').val(),
        Name: create.frm.find('#txtName').val(),
        Url: create.frm.find('#txtUrl').val(),
        Order: create.frm.find('#txtOrder').val(),
        Note: create.frm.find('#txtNote').val(),
        Status: create.frm.find('#ckbStatus').prop("checked") ? 1 : 2
    }
    create.Create();
})