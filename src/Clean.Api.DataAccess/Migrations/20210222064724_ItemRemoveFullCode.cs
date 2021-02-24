using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class ItemRemoveFullCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemFullCode",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemFullCode",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
