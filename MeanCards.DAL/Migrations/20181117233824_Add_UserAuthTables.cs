using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class Add_UserAuthTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                schema: "meancards",
                table: "Users",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "meancards",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "meancards",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAuths",
                schema: "meancards",
                columns: table => new
                {
                    UserAuthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuths", x => x.UserAuthId);
                    table.ForeignKey(
                        name: "FK_UserAuths_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "meancards",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGoogleAuths",
                schema: "meancards",
                columns: table => new
                {
                    UserGoogleAuthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    GoogleId = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGoogleAuths", x => x.UserGoogleAuthId);
                    table.ForeignKey(
                        name: "FK_UserGoogleAuths_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "meancards",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAuths_UserId",
                schema: "meancards",
                table: "UserAuths",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGoogleAuths_UserId",
                schema: "meancards",
                table: "UserGoogleAuths",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAuths",
                schema: "meancards");

            migrationBuilder.DropTable(
                name: "UserGoogleAuths",
                schema: "meancards");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "meancards",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "meancards",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                schema: "meancards",
                table: "Users",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }
    }
}
