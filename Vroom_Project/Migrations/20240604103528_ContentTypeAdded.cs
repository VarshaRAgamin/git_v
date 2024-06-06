using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vroom_Project.Migrations
{
    public partial class ContentTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contenttype",
                table: "UsersAccount",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "UsersAccount",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contenttype",
                table: "UsersAccount");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "UsersAccount");
        }
    }
}
