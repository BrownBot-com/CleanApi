using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class FixItemBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Branch_BrandCode",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandCode);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Brand_BrandCode",
                table: "Items",
                column: "BrandCode",
                principalTable: "Brand",
                principalColumn: "BrandCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Brand_BrandCode",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Branch_BrandCode",
                table: "Items",
                column: "BrandCode",
                principalTable: "Branch",
                principalColumn: "BranchCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
