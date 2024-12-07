using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChodoidoUTE.Migrations
{
    /// <inheritdoc />
    public partial class Updata_TinNhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaXem",
                table: "TinNhans");

            migrationBuilder.RenameColumn(
                name: "ThoiGianGui",
                table: "TinNhans",
                newName: "LastMessageTime");

            migrationBuilder.RenameColumn(
                name: "NoiDung",
                table: "TinNhans",
                newName: "LastMessage");

            migrationBuilder.CreateTable(
                name: "ChiTietTinNhan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinNhanId = table.Column<int>(type: "int", nullable: false),
                    NguoiGuiId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinNhanId2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietTinNhan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietTinNhan_AspNetUsers_NguoiGuiId",
                        column: x => x.NguoiGuiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietTinNhan_ChiTietTinNhan_TinNhanId",
                        column: x => x.TinNhanId,
                        principalTable: "ChiTietTinNhan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChiTietTinNhan_TinNhans_TinNhanId2",
                        column: x => x.TinNhanId2,
                        principalTable: "TinNhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTinNhan_NguoiGuiId",
                table: "ChiTietTinNhan",
                column: "NguoiGuiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTinNhan_TinNhanId",
                table: "ChiTietTinNhan",
                column: "TinNhanId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTinNhan_TinNhanId2",
                table: "ChiTietTinNhan",
                column: "TinNhanId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietTinNhan");

            migrationBuilder.RenameColumn(
                name: "LastMessageTime",
                table: "TinNhans",
                newName: "ThoiGianGui");

            migrationBuilder.RenameColumn(
                name: "LastMessage",
                table: "TinNhans",
                newName: "NoiDung");

            migrationBuilder.AddColumn<bool>(
                name: "DaXem",
                table: "TinNhans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
