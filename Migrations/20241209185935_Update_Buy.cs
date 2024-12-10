using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChodoidoUTE.Migrations
{
    /// <inheritdoc />
    public partial class Update_Buy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Xóa khóa chính hiện tại trước khi thay đổi cột
            migrationBuilder.DropPrimaryKey(
                name: "PK_Buys",
                table: "Buys");

            // Thay đổi kiểu dữ liệu cột `Id` từ long -> int
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Buys",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            // Thêm lại khóa chính
            migrationBuilder.AddPrimaryKey(
                name: "PK_Buys",
                table: "Buys",
                column: "Id");

            // Thêm cột mới `Type`
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Buys",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa cột `Type`
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Buys");

            // Xóa khóa chính trước khi thay đổi cột
            migrationBuilder.DropPrimaryKey(
                name: "PK_Buys",
                table: "Buys");

            // Thay đổi kiểu dữ liệu cột `Id` từ int -> long
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Buys",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            // Thêm lại khóa chính
            migrationBuilder.AddPrimaryKey(
                name: "PK_Buys",
                table: "Buys",
                column: "Id");
        }
    }
}
