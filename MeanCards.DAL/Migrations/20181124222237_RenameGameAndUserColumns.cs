using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class RenameGameAndUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameStatus",
                schema: "meancards",
                table: "Games",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "GameCode",
                schema: "meancards",
                table: "Games",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_Games_GameCode",
                schema: "meancards",
                table: "Games",
                newName: "IX_Games_Code");

            migrationBuilder.RenameColumn(
                name: "UserCode",
                schema: "meancards",
                table: "AspNetUsers",
                newName: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "meancards",
                table: "Games",
                newName: "GameStatus");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "meancards",
                table: "Games",
                newName: "GameCode");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Code",
                schema: "meancards",
                table: "Games",
                newName: "IX_Games_GameCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "meancards",
                table: "AspNetUsers",
                newName: "UserCode");
        }
    }
}
