$(function () {
    getData();
    $('#btnSearch').click(function () {
        var time = $('#txtDate').val();
        getData(time);
    })
    getData();
    $('#btnExport').click(function () {
        export2Exel();
    })

})
function getData(time = "") {
    $.ajax({
        url:  "/thong-ke-doanh-thu-thang/get-list?time=" + time, 
        method: "Get",
        success: function (response) {
            $('#dtTable').html(response);
            hideLoading()
        }
    })
}
function export2Exel() {
    var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById('dtTable');
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

    // Specify file name
    var filename = 'BCTK' + $('#txtDate').val() + 'xls'
    // Create download link element
    downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['\ufeff', tableHTML], {
            type: dataType
        });
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        // Setting the file name
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
    }
}