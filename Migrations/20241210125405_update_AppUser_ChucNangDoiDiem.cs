using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChodoidoUTE.Migrations
{
    /// <inheritdoc />
    public partial class update_AppUser_ChucNangDoiDiem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LuotDang",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LuotDang",
                table: "AspNetUsers");
        }
    }
}
