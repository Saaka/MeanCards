using Microsoft.EntityFrameworkCore.Migrations;

namespace MeanCards.DAL.Migrations
{
    public partial class AddDisplayName : Migration
    {
        const string MigrationScript = "UPDATE meancards.AspNetUsers SET [DisplayName]=[UserName]";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "meancards",
                table: "AspNetUsers",
                maxLength: 128,
                nullable: true);

            migrationBuilder.Sql(MigrationScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                schema: "meancards",
                table: "AspNetUsers");
        }
    }
}
