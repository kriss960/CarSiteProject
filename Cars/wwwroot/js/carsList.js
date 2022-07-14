var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/cars/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "brand", "width": "20%" },
            { "data": "model", "width": "20%" },
            { "data": "price", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Cars/ViewAd?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            View
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No ads found!"
        },
        "width": "100%"
    });
}