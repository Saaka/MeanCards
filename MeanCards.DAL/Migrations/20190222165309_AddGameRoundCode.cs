using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class AddGameRoundCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "meancards",
                table: "GameRounds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "meancards",
                table: "GameRounds");
        }
    }
}
