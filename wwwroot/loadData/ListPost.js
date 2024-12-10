var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#dataTablePost').DataTable({
        "ajax": {
            "url": "/admin/post/list", // Đường dẫn API lấy dữ liệu
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return `<p>${meta.row + 1}</p>`;
                },
                "width": "5%",
                "className": "text-center"
            },
            {
                "data": "fullName",
                "render": function (data) {
                    return `<h6><strong>${data}</strong></h6>`;
                },
                "width": "5%",
                "className": "text-center"
            },
            {
                "data": "title",
                "render": function (data) {
                    return `<h6><strong>${data}</strong></h6>`;
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "postProductStatus",
                "render": function (data) {
                    let badgeClass = "";
                    let badgeText = "";

                    switch (data) {
                        case "CHO_DUYET":
                            badgeClass = "badge-info";
                            badgeText = "Chờ duyệt";
                            break;
                        case "DA_DUYET":
                            badgeClass = "badge-success";
                            badgeText = "Đã duyệt";
                            break;
                        case "TU_CHOI":
                            badgeClass = "badge-danger";
                            badgeText = "Đã từ chối";
                            break;
                        case "DA_AN":
                            badgeClass = "badge-primary";
                            badgeText = "Đã ẩn";
                            break;
                        default:
                            badgeClass = "badge-secondary";
                            badgeText = "Đã bán";
                            break;
                    }

                    return `<span class="badge ${badgeClass}" style="padding:5px 10px">${badgeText}</span>`;
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "price",
                "render": function (data) {
                    return `<span>${data.toLocaleString()} VND</span>`;
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "id",
                "render": function (data, type, row) {
                    if (row.postProductStatus === "CHO_DUYET") {
                        buttons = `
                            <a href="/admin/post/detail/${row.id}" class="btn btn-sm btn-primary text-white mr-1" style="width:32%;border: 2px solid #000000;">
                                <i class='fas fa-eye' style="margin-right:10px"></i>
                                Chi tiết
                            </a>
                            <a class="btn btn-sm btn-success text-white mr-1" onclick="Approve('/admin/post/approve/${row.id}')" style="width:32%;border: 2px solid #000000;">
                                <i class='fas fa-check' style="margin-right:10px"></i>
                                Duyệt
                            </a>
                            <a class="btn btn-sm btn-danger text-white" onclick="Deny('/admin/post/deny/${row.id}')" style="width:32%;border: 2px solid #000000;">
                                <i class='fas fa-times' style="margin-right:10px"></i>
                                Từ chối
                            </a>
                        `;
                    } else {
                        buttons = `
                            <a href="/admin/post/detail/${row.id}" class="btn btn-sm btn-primary text-white mr-1" style="width:50%;border: 2px solid #000000;">
                                <i class='fas fa-eye' style="margin-right:10px"></i>
                                Chi tiết
                            </a>
                        `;
                    }
                    return buttons;
                },
                "width": "30%",
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
            "loadingRecords": "Đang tải..."
        },
        "width": "100%",
        "dom": '<"row"<"col-md-6"l><"col-md-6"f>>rt<"row"<"col-md-6"i><"col-md-6"p>>',
        "pagingType": "full_numbers",
        "renderer": "bootstrap",
        "autoWidth": false
    });
    dataTable.ajax.reload();
}

async function Deny(url) {
    const result = await Swal.fire({
        title: 'Bạn có muốn từ chối bài đăng này?',
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

async function Approve(url) {
    const result = await Swal.fire({
        title: 'Bạn có muốn duyệt bài đăng này?',
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