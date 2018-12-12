using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class RenameGameRoundOwnerAndWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_OwnerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_WinnerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "WinnerPlayerId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "OwnerPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_WinnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_WinnerPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_OwnerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_OwnerPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_Players_OwnerPlayerId",
                schema: "meancards",
                table: "GameRounds",
                column: "OwnerPlayerId",
                principalSchema: "meancards",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_Players_WinnerPlayerId",
                schema: "meancards",
                table: "GameRounds",
                column: "WinnerPlayerId",
                principalSchema: "meancards",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_OwnerPlayerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Players_WinnerPlayerId",
                schema: "meancards",
                table: "GameRounds");

            migrationBuilder.RenameColumn(
                name: "WinnerPlayerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "WinnerId");

            migrationBuilder.RenameColumn(
                name: "OwnerPlayerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_WinnerPlayerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_OwnerPlayerId",
                schema: "meancards",
                table: "GameRounds",
                newName: "IX_GameRounds_OwnerId");

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
    }
}
