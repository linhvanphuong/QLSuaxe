
$(function () {
    list.init({
        frm: $('#dtTable'),
        table: "#tblDisplay",
    });
    setDataTable();
});
function setDataTable() {
    list.frm.find(list.table).DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": true,
        "sDom": 'lrtip',
        "columnDefs": [
            { "orderable": false, "targets": 2 },
            { "orderable": false, "targets": 3 }
        ],
        "language": {
            "emptyTable": "Không tìm thấy dữ liệu"
        }
    });
}
function openUpdate(id) {
    normal.openUpdate(id, "Cập nhật dịch vụ");
}
function openDelete(id) {
    normal.openDelete(id);
}