using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChodoidoUTE.Migrations
{
    /// <inheritdoc />
    public partial class ChiTietTinNhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTinNhan_AspNetUsers_NguoiGuiId",
                table: "ChiTietTinNhan");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTinNhan_TinNhans_TinNhanId",
                table: "ChiTietTinNhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietTinNhan",
                table: "ChiTietTinNhan");

            migrationBuilder.RenameTable(
                name: "ChiTietTinNhan",
                newName: "ChiTietTinNhans");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietTinNhan_TinNhanId",
                table: "ChiTietTinNhans",
                newName: "IX_ChiTietTinNhans_TinNhanId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietTinNhan_NguoiGuiId",
                table: "ChiTietTinNhans",
                newName: "IX_ChiTietTinNhans_NguoiGuiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietTinNhans",
                table: "ChiTietTinNhans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTinNhans_AspNetUsers_NguoiGuiId",
                table: "ChiTietTinNhans",
                column: "NguoiGuiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTinNhans_TinNhans_TinNhanId",
                table: "ChiTietTinNhans",
                column: "TinNhanId",
                principalTable: "TinNhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTinNhans_AspNetUsers_NguoiGuiId",
                table: "ChiTietTinNhans");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTinNhans_TinNhans_TinNhanId",
                table: "ChiTietTinNhans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietTinNhans",
                table: "ChiTietTinNhans");

            migrationBuilder.RenameTable(
                name: "ChiTietTinNhans",
                newName: "ChiTietTinNhan");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietTinNhans_TinNhanId",
                table: "ChiTietTinNhan",
                newName: "IX_ChiTietTinNhan_TinNhanId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietTinNhans_NguoiGuiId",
                table: "ChiTietTinNhan",
                newName: "IX_ChiTietTinNhan_NguoiGuiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietTinNhan",
                table: "ChiTietTinNhan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTinNhan_AspNetUsers_NguoiGuiId",
                table: "ChiTietTinNhan",
                column: "NguoiGuiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTinNhan_TinNhans_TinNhanId",
                table: "ChiTietTinNhan",
                column: "TinNhanId",
                principalTable: "TinNhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
