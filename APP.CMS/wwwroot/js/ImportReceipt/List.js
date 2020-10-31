
$(function () {
    setDataTable();
});
function setDataTable() {
    $('#dtTable').find('#tblDisplay').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": false,
        "autoWidth": false,
        "responsive": true,
        "sDom": 'lrtip',
        "columnDefs": [
            { "orderable": false, "targets": 3 }
        ],
        "language": {
            "emptyTable": "Không tìm thấy dữ liệu"
        }
    });
}
