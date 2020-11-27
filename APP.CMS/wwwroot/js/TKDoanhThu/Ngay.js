$(function () {
    getData();
    $('#btnSearch').click(function () {
        var time = $('#txtDate').val();
        getData(time);
    })
    getData();
    $('#btnExport').click(function () {
        exportPdf();
    })

})
function getData(time = "") {
    $.ajax({
        url:  "/thong-ke-doanh-thu-ngay/get-list?time=" + time, 
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
function exportPdf() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var title = '<div>'
    //title += '<h1 style="text-align:center;color:#000;font-weight:bold"> THỐNG KÊ DOANH THU NGÀY ' + dd + "/" + mm + "/" + yyyy + '</h1>';
    var body = $("#dtTable").html() + '</div>';
    var footer = '<div style="text-align:right;margin-right:50px"><p><i>Hà Nội, Ngày ' + dd + ' tháng ' + mm + ' năm ' + yyyy + '</i></p>';
    footer += '<p style="margin-right:90px;" ><b>Người lập biểu</b><br>' + '<i>(Ký ghi rõ họ tên)</i><br><br></p></div>';

    var html =/* header +*/ /*title +*/ body + footer;
    var fileName = 'BCNgay_' + dd + mm + yyyy + '.pdf';
    //console.log(sourceHTML);
    var input = {
        html: html,
        fileName: fileName
    } 
    $.ajax({
        url: "/thong-ke-doanh-thu-ngay/export-pdf",
        method: "POST",
        data: {
            input: input
        },
        xhrFields: {
            responseType: 'blob' // to avoid binary data being mangled on charset conversion
        },
        success: function (blob, status, xhr) {
            // check for a filename
            var filename = "";
            var disposition = xhr.getResponseHeader('Content-Disposition');
            if (disposition && disposition.indexOf('attachment') !== -1) {
                var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                var matches = filenameRegex.exec(disposition);
                if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
            }

            if (typeof window.navigator.msSaveBlob !== 'undefined') {
                // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                window.navigator.msSaveBlob(blob, filename);
            } else {
                var URL = window.URL || window.webkitURL;
                var downloadUrl = URL.createObjectURL(blob);

                if (filename) {
                    // use HTML5 a[download] attribute to specify filename
                    var a = document.createElement("a");
                    // safari doesn't support this yet
                    if (typeof a.download === 'undefined') {
                        window.location.href = downloadUrl;
                    } else {
                        a.href = downloadUrl;
                        a.download = filename;
                        document.body.appendChild(a);
                        a.click();
                    }
                } else {
                    window.location.href = downloadUrl;
                }

                setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
            }
        }
    });
}