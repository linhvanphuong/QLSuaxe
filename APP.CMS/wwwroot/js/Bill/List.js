
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
        //"order": [],
        "sDom": 'lrtip',
        "aaSorting": [],
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
function updateStatus(id, status) {
    $.ajax({
        url: "/hoa-don/update-status?id=" + id + "&status=" + status,
        method: "Get",
        success: function (response) {
            if (response.result) {
                showAlert("Thành công", 2);
                var stt = $('#txtStatushd').val();
                normal.getData("",stt);
            }
        }
    })
}
function onpenAccepted(id, status) {
    $.ajax({
        url: "/hoa-don/update-status?id=" + id + "&status=" + status,
        method: "Get",
        success: function (response) {
            if (response.result) {
                showAlert("Thành công", 2);
                var stt = 4;
                $('#drStatus').val(4).trigger("change");
                normal.getData("", stt);
            }
        }
    })
}
function openSentKTV(id, status) {
    $.ajax({
        url: "/hoa-don/sent-KTV?id=" + id + "&status=" + status,
        method: "Get",
        success: function (response) {
            if (response.result) {
                showAlert("Thành công", 2);
                var stt = 3;
                normal.getData("", stt);
            }
        }
    })
}