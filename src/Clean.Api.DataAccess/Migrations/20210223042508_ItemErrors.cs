using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class ItemErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemErrors",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemFullCode",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemErrors",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemFullCode",
                table: "Items");
        }
    }
}
