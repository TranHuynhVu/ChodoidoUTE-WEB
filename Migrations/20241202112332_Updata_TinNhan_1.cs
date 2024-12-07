using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChodoidoUTE.Migrations
{
    /// <inheritdoc />
    public partial class Updata_TinNhan_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTinNhan_ChiTietTinNhan_TinNhanId",
                table: "ChiTietTinNhan");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTinNhan_TinNhans_TinNhanId2",
                table: "ChiTietTinNhan");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietTinNhan_TinNhanId2",
                table: "ChiTietTinNhan");

            migrationBuilder.DropColumn(
                name: "TinNhanId2",
                table: "ChiTietTinNhan");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTinNhan_TinNhans_TinNhanId",
                table: "ChiTietTinNhan",
                column: "TinNhanId",
                principalTable: "TinNhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTinNhan_TinNhans_TinNhanId",
                table: "ChiTietTinNhan");

            migrationBuilder.AddColumn<int>(
                name: "TinNhanId2",
                table: "ChiTietTinNhan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTinNhan_TinNhanId2",
                table: "ChiTietTinNhan",
                column: "TinNhanId2");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTinNhan_ChiTietTinNhan_TinNhanId",
                table: "ChiTietTinNhan",
                column: "TinNhanId",
                principalTable: "ChiTietTinNhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTinNhan_TinNhans_TinNhanId2",
                table: "ChiTietTinNhan",
                column: "TinNhanId2",
                principalTable: "TinNhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
