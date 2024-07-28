$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
         
            { data: 'productName', "width": "10%" },
            { data: 'category.catagoryName', "width": "10%" },
            { data: 'description', "width": "15%" },
            { data: 'manufacturer', "width": "10%" },
            { data: 'price', "width": "10%" },
            { data: 'size', "width": "5%" },
            { data: 'instructions', "width": "15%" },
            { data: 'ingredients', "width": "10%" },
            {
                data: 'expiryDate',
                "width": "10%",
                "render": function (data, type, row) {
                    if (data) {
                        var date = new Date(data);
                        var formattedDate = date.toLocaleDateString('en-US');
                        return formattedDate;
                    }
                    return "";
                }
            },
            { data: 'stockQuantity', "width": "5%" },
            { data: 'usage', "width": "10%" },
            {
                data: 'isPrescriptionRequired',
                "width": "5%",
                "render": function (data, type, row) {
                    return data ? 'True' : 'False';
                }
            },
            {
                data: null,
                "width": "10%",
                "orderable": false,
                "render": function (data, type, row) {
                    return `
                        <div class="text-center">
                            <a href="/admin/product/upsert/${row.productId}" class="btn btn-primary text-white" style="cursor:pointer;">
                                Edit
                            </a>
                            <a href="/admin/product/delete/${row.productId}" class="btn btn-danger text-white" style="cursor:pointer;">
                                Delete
                            </a>
                        </div>
                    `;
                }
            }
        ]
    });
}
