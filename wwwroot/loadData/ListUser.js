var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#dataTableUser').DataTable({
        "ajax": {
            "url": "/admin/user/list",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return '<p style="margin-top:60px">' + (meta.row + 1) + '</p>';
                },
                "width": "5%",
                "className": "text-center"
            },
            {
                "data": "imgUrl",
                "render": function (data) {
                    return `<img src="${data}" alt="Hình ảnh danh mục" style="width: 150px; height: 150px; object-fit: cover;">`;
                },
                "width": "15%",
                "className": "text-center"
            },
            {
                "data": "name",
                "render": function (data, type, row) {
                    return '<h6 style="margin-top:65px"><strong>' + data + '</strong></h6>';
                },
                "width": "15%",
                "className": "text-center column-name"
            },
            {
                "data": "email",
                "render": function (data) {
                    return `<p style="margin-top:65px">${data}</p>`;
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "status",
                "render": function (data) {
                    let badgeClass = "";
                    let badgeText = "";

                    switch (data) {
                        case 0:
                            badgeClass = "badge-info";
                            badgeText = "Chờ xử lý";
                            break;
                        case 1:
                            badgeClass = "badge-success";
                            badgeText = "Đã duyệt tài khoản";
                            break;
                        case -1:
                            badgeClass = "badge-danger";
                            badgeText = "Đã khóa tài khoản";
                            break;
                        default:
                            badgeClass = "badge-secondary";
                            badgeText = "Không xác định tình trạng";
                            break;
                    }

                    return `<span class="badge ${badgeClass}" style="margin-top:65px;padding:5px 10px">${badgeText}</span>`;
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "id",
                "render": function (data, type, row) {
                    let buttons = '';
                    if (row.status === 0) {
                        buttons = `
                            <a href="/admin/user/detail/${row.id}" class="btn btn-sm btn-primary text-white mr-1" style="width:48%;border: 2px solid #000000; margin-top:60px">
                                <i class='fas fa-eye' style="margin-right:10px"></i>
                                Chi tiết
                            </a>
                            <a class="btn btn-sm btn-success text-white mr-1" onclick="Approve('/admin/user/approve/${row.id}')" style="width:48%;border: 2px solid #000000; margin-top:60px">
                                <i class='fas fa-check' style="margin-right:10px"></i>
                                Duyệt tài khoản
                            </a>
                        `;
                    } else if (row.status === 1) {
                        buttons = `
                            <a href="/admin/user/detail/${row.id}" class="btn btn-sm btn-primary text-white mr-1" style="width:48%;border: 2px solid #000000; margin-top:60px">
                                <i class='fas fa-eye' style="margin-right:10px"></i>
                                Chi tiết
                            </a>
                            <a class="btn btn-sm btn-danger text-white" onclick="Deny('/admin/user/deny/${row.id}')" style="width:48%;border: 2px solid #000000; margin-top:60px">
                                <i class='fas fa-times' style="margin-right:10px"></i>
                                Khóa tài khoản
                            </a>
                        `;
                    } else {
                        buttons = `
                            <a href="/admin/user/detail/${row.id}" class="btn btn-sm btn-primary text-white mr-1" style="width:55%;border: 2px solid #000000; margin-top:60px">
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
            "loadingRecords": "Loading...",
        },
        "width": "100%",
        "dom": '<"row"<"col-md-6"l><"col-md-6"f>>rt<"row"<"col-md-6"i><"col-md-6"p>>',
        "pagingType": "full_numbers",
        "renderer": "bootstrap"
    });
    dataTable.ajax.reload();
}

async function Deny(url) {
    const result = await Swal.fire({
        title: 'Bạn có muốn khóa tài khoản này?',
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
        title: 'Bạn có muốn duyệt tài khoản này?',
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

$('#openModal').click(function () {
    var fileUrl = '/data/DanhSachSinhVienDangKy.xlsx'; 
    $.ajax({
        url: fileUrl,
        type: 'GET',
        xhrFields: {
            responseType: 'arraybuffer' 
        },
        beforeSend: function () {
            $('#modalContent').html('<div class="spinner-border" role="status"><span class="visually-hidden">Loading...</span></div>');
        },
        success: function (data) {
            var workbook = XLSX.read(data, { type: 'array' });
            var html = XLSX.utils.sheet_to_html(workbook.Sheets[workbook.SheetNames[0]]);

            var $html = $('<div>').html(html); 
            $html.find('tr').slice(0, 3).remove();

            $html.find('table').css('width', '1050px');

            $html.find('tr').eq(1).find('td').addClass('text-center').css('font-weight', 'bold');

            $html.find('tr').slice(2).each(function () {
                $(this).find('td').addClass('text-center');
            });
            $html.append('<button id="assignAccountBtn" class="btn btn-success mt-3">Cấp tài khoản cho sinh viên</button>');
            $('#modalContent').html($html.html());
            $('#detailModal').modal('show');
            $(document).on('click', '#assignAccountBtn', async () => {
                const result = await Swal.fire({
                    title: 'Bạn có muốn cấp tài khoản cho những sinh viên này?',
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
                            type: "POST",
                            url: '/admin/user/provide'
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
            });
        },
        error: function () {
            $('#modalContent').html('<p>Lỗi khi tải tệp Excel.</p>');
        }
    });
});
