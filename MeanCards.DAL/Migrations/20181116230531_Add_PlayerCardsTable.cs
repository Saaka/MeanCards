using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class Add_PlayerCardsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayersCards",
                schema: "meancards",
                columns: table => new
                {
                    PlayerCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    AnswerCardId = table.Column<int>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersCards", x => x.PlayerCardId);
                    table.ForeignKey(
                        name: "FK_PlayersCards_AnswerCards_AnswerCardId",
                        column: x => x.AnswerCardId,
                        principalSchema: "meancards",
                        principalTable: "AnswerCards",
                        principalColumn: "AnswerCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayersCards_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "meancards",
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersCards_AnswerCardId",
                schema: "meancards",
                table: "PlayersCards",
                column: "AnswerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersCards_PlayerId",
                schema: "meancards",
                table: "PlayersCards",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersCards",
                schema: "meancards");
        }
    }
}
