using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class ItemOldCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemOldCode",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemOldCode",
                table: "Items");
        }
    }
}
