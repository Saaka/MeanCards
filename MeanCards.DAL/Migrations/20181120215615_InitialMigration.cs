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
                name: "AspNetRoles",
                schema: "meancards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "meancards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "meancards",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 8, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "meancards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "meancards",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "meancards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "meancards",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "meancards",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "meancards",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "meancards",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "meancards",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "meancards",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "meancards",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "meancards",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerCards",
                schema: "meancards",
                columns: table => new
                {
                    AnswerCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 256, nullable: true),
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
                name: "Games",
                schema: "meancards",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameCode = table.Column<string>(maxLength: 32, nullable: false),
                    GameStatus = table.Column<byte>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
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
                        name: "FK_Games_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "meancards",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    Text = table.Column<string>(maxLength: 256, nullable: false),
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
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "meancards",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "meancards",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "meancards",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "meancards",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "meancards",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "meancards",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "meancards",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Games_GameCode",
                schema: "meancards",
                table: "Games",
                column: "GameCode",
                unique: true);

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
                name: "IX_PlayersCards_AnswerCardId",
                schema: "meancards",
                table: "PlayersCards",
                column: "AnswerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersCards_PlayerId",
                schema: "meancards",
                table: "PlayersCards",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCards_LanguageId",
                schema: "meancards",
                table: "QuestionCards",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "PlayerAnswers",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "PlayersCards",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "GameRounds",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "AnswerCards",
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
                name: "AspNetUsers",
                schema: "meancards");
        }
    }
}
