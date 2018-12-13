using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class AlterGameCheckpointRenameCheckpointCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checkpoint",
                schema: "meancards",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "CheckpointCode",
                schema: "meancards",
                table: "GameCheckpoints",
                newName: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "meancards",
                table: "GameCheckpoints",
                newName: "CheckpointCode");

            migrationBuilder.AddColumn<string>(
                name: "Checkpoint",
                schema: "meancards",
                table: "Games",
                nullable: true);
        }
    }
}
