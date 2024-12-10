using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChodoidoUTE.Migrations
{
    /// <inheritdoc />
    public partial class update_RateStar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RateStars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RateStars_UserId",
                table: "RateStars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RateStars_AspNetUsers_UserId",
                table: "RateStars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RateStars_AspNetUsers_UserId",
                table: "RateStars");

            migrationBuilder.DropIndex(
                name: "IX_RateStars_UserId",
                table: "RateStars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RateStars");
        }
    }
}
