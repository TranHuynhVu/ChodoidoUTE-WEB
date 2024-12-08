var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#dataTableCategory').DataTable({
        "ajax": {
            "url": "/admin/category/list",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                },
                "width": "5%",
                "className": "text-center"
            },
            {
                "data": "url",
                "render": function (data) {
                    return `<img src="${data}" alt="Hình ảnh danh mục" style="width: 150px; height: 150px; object-fit: cover;">`;
                },
                "width": "15%",
                "className": "text-center"
            },
            {
                "data": "name",
                "width": "30%",
                "className": "text-center column-name",
                "render": function (data, type, row) {
                    return '<h6 style="margin-top:65px"><strong>' + data + '</strong></h6>';
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/admin/category/edit/${data}" class="btn btn-sm btn-warning text-white mr-1" style="width:48%;margin-top:60px;border: 2px solid #000000;" >
                            Sửa
                        </a>
                        <a class="btn btn-sm btn-danger text-white" onclick="Delete('/admin/category/delete/${data}')" style="width:50%;margin-top:60px;border: 2px solid #000000;">
                            Xóa
                        </a>
                    `;
                },
                "width": "20%",
                "className": "text-center"
            }
        ],
        "language": {
            "emptyTable": "Không có dữ liệu",
            "lengthMenu": "_MENU_",
            "zeroRecords": "Không tìm thấy",
            "info": "Đang hiển thị trang _PAGE_ của _PAGES_",
            "infoEmpty": "Không có dữ liệu",
            "infoFiltered": "(được lọc từ _MAX_ tổng dữ liệu)",
            "search": "",
            searchPlaceholder: 'Tìm kiếm',
            "loadingRecords": "Loading...",

        },
        "dom": '<"row"<"col-md-6"l><"col-md-6"f>>rt<"row"<"col-md-6"i><"col-md-6"p>>',
        "pagingType": "full_numbers",
        "renderer": "bootstrap"
    });
    dataTable.ajax.reload();
}

async function Delete(url) {
    const result = await Swal.fire({
        title: 'Bạn có muốn xóa danh mục này?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Xác nhận',
        cancelButtonText: 'Hủy',
        confirmButtonColor: '#28A745',
        cancelButtonColor: '#d33',
        focusConfirm: false
    });
    if (result.isConfirmed) {
        const currentPage = dataTable.page();
        try {
            const response = await $.ajax({
                type: "PATCH",
                url: url
            });
            if (response.status === 200) {
                await Swal.fire({
                    position: "center",
                    icon: "success",
                    title: response.message,
                    showConfirmButton: false,
                    timer: 1500
                });
            } else {
                await Swal.fire({
                    icon: "error",
                    title: "Lỗi xảy ra",
                    text: response.message
                });
            }
            dataTable.ajax.reload(() => {
                dataTable.page(currentPage).draw(false);
            });
        } catch (error) {
            toastr.error('Có lỗi xảy ra, vui lòng thử lại!');
        }
    }
}