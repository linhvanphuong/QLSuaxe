
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
            { "orderable": false, "targets": 4 },
            { "orderable": false, "targets": 5 }
        ],
        "language": {
            "emptyTable": "Không tìm thấy dữ liệu"
        }
    });
}
function openUpdate(id) {
    location.href = normal.domain + '/cap-nhat?id='+id;
}
function openDelete(id) {
    normal.openDelete(id);
}