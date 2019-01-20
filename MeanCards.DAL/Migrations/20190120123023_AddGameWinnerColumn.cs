using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class AddGameWinnerColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                schema: "meancards",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerId",
                schema: "meancards",
                table: "Games",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_WinnerId",
                schema: "meancards",
                table: "Games",
                column: "WinnerId",
                principalSchema: "meancards",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_WinnerId",
                schema: "meancards",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_WinnerId",
                schema: "meancards",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                schema: "meancards",
                table: "Games");
        }
    }
}
