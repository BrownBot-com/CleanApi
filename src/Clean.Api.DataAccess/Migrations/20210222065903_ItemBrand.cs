using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class ItemBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandCode",
                table: "Items",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_BrandCode",
                table: "Items",
                column: "BrandCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Branch_BrandCode",
                table: "Items",
                column: "BrandCode",
                principalTable: "Branch",
                principalColumn: "BranchCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Branch_BrandCode",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_BrandCode",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BrandCode",
                table: "Items");
        }
    }
}
