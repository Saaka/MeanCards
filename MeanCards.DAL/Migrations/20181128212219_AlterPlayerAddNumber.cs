using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class AlterPlayerAddNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_RoundOwnerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_RoundWinnerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.RenameColumn(
                name: "RoundWinnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "WinnerId");

            migrationBuilder.RenameColumn(
                name: "RoundOwnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "RoundNumber",
                schema: "meancards",
                table: "GameRounds",
                newName: "Number");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_RoundWinnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_RoundOwnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                schema: "meancards",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_Players_OwnerId",
                schema: "meancards",
                table: "GameRounds",
                column: "OwnerId",
                principalSchema: "meancards",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_Players_WinnerId",
                schema: "meancards",
                table: "GameRounds",
                column: "WinnerId",
                principalSchema: "meancards",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_OwnerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_WinnerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "Number",
                schema: "meancards",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "RoundWinnerId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "RoundOwnerId");

            migrationBuilder.RenameColumn(
                name: "Number",
                schema: "meancards",
                table: "GameRounds",
                newName: "RoundNumber");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_WinnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_RoundWinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_OwnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_RoundOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_Players_RoundOwnerId",
                schema: "meancards",
                table: "GameRounds",
                column: "RoundOwnerId",
                principalSchema: "meancards",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_Players_RoundWinnerId",
                schema: "meancards",
                table: "GameRounds",
                column: "RoundWinnerId",
                principalSchema: "meancards",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
