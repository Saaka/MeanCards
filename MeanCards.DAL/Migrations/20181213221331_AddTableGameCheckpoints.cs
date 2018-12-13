using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class AddTableGameCheckpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameCheckpoints",
                schema: "meancards",
                columns: table => new
                {
                    GameCheckpointId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(nullable: false),
                    CheckpointCode = table.Column<string>(maxLength: 32, nullable: false),
                    OperationType = table.Column<string>(maxLength: 64, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCheckpoints", x => x.GameCheckpointId);
                    table.ForeignKey(
                        name: "FK_GameCheckpoints_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "meancards",
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCheckpoints_GameId",
                schema: "meancards",
                table: "GameCheckpoints",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCheckpoints",
                schema: "meancards");
        }
    }
}
