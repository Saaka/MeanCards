using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "meancards");

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "meancards",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "meancards",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MappedUserId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AnswerCards",
                schema: "meancards",
                columns: table => new
                {
                    AnswerCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    IsAdultContent = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerCards", x => x.AnswerCardId);
                    table.ForeignKey(
                        name: "FK_AnswerCards_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "meancards",
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCards",
                schema: "meancards",
                columns: table => new
                {
                    QuestionCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    IsAdultContent = table.Column<bool>(nullable: false),
                    NumberOfAnswers = table.Column<byte>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCards", x => x.QuestionCardId);
                    table.ForeignKey(
                        name: "FK_QuestionCards_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "meancards",
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                schema: "meancards",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameStatus = table.Column<byte>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    ShowAdultContent = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "meancards",
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_User_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "meancards",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "meancards",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "meancards",
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameRounds",
                schema: "meancards",
                columns: table => new
                {
                    GameRoundId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(nullable: false),
                    QuestionCardId = table.Column<int>(nullable: false),
                    RoundOwnerId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RoundWinnerId = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRounds", x => x.GameRoundId);
                    table.ForeignKey(
                        name: "FK_GameRounds_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "meancards",
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRounds_QuestionCards_QuestionCardId",
                        column: x => x.QuestionCardId,
                        principalSchema: "meancards",
                        principalTable: "QuestionCards",
                        principalColumn: "QuestionCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRounds_Players_RoundOwnerId",
                        column: x => x.RoundOwnerId,
                        principalSchema: "meancards",
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRounds_Players_RoundWinnerId",
                        column: x => x.RoundWinnerId,
                        principalSchema: "meancards",
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAnswers",
                schema: "meancards",
                columns: table => new
                {
                    PlayerAnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameRoundId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    IsSelectedAnswer = table.Column<bool>(nullable: false),
                    AnswerCardId = table.Column<int>(nullable: false),
                    SecondaryAnswerCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAnswers", x => x.PlayerAnswerId);
                    table.ForeignKey(
                        name: "FK_PlayerAnswers_AnswerCards_AnswerCardId",
                        column: x => x.AnswerCardId,
                        principalSchema: "meancards",
                        principalTable: "AnswerCards",
                        principalColumn: "AnswerCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerAnswers_GameRounds_GameRoundId",
                        column: x => x.GameRoundId,
                        principalSchema: "meancards",
                        principalTable: "GameRounds",
                        principalColumn: "GameRoundId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerAnswers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "meancards",
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerAnswers_AnswerCards_SecondaryAnswerCardId",
                        column: x => x.SecondaryAnswerCardId,
                        principalSchema: "meancards",
                        principalTable: "AnswerCards",
                        principalColumn: "AnswerCardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCards_LanguageId",
                schema: "meancards",
                table: "AnswerCards",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_GameId",
                schema: "meancards",
                table: "GameRounds",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_QuestionCardId",
                schema: "meancards",
                table: "GameRounds",
                column: "QuestionCardId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_RoundOwnerId",
                schema: "meancards",
                table: "GameRounds",
                column: "RoundOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_RoundWinnerId",
                schema: "meancards",
                table: "GameRounds",
                column: "RoundWinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_LanguageId",
                schema: "meancards",
                table: "Games",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_OwnerId",
                schema: "meancards",
                table: "Games",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAnswers_AnswerCardId",
                schema: "meancards",
                table: "PlayerAnswers",
                column: "AnswerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAnswers_GameRoundId",
                schema: "meancards",
                table: "PlayerAnswers",
                column: "GameRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAnswers_PlayerId",
                schema: "meancards",
                table: "PlayerAnswers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAnswers_SecondaryAnswerCardId",
                schema: "meancards",
                table: "PlayerAnswers",
                column: "SecondaryAnswerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                schema: "meancards",
                table: "Players",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCards_LanguageId",
                schema: "meancards",
                table: "QuestionCards",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerAnswers",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "AnswerCards",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "GameRounds",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "QuestionCards",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "Games",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "User",
                schema: "meancards");
        }
    }
}
