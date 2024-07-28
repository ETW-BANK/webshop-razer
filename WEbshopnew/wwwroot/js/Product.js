
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
   dataTable= $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'productId', "width": "10%" },
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
                "render": function (data) {
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
                "render": function (data) {
                    return data ? 'True' : 'False';
                }
            },
            {
                data: 'productId',
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert/${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a onClick=Delete('/Admin/Product/delete/${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>
                    `;
                },
                "width": "25%"
            }
        ]
    });
}



function Delete(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });


}