using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class ItemUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemStockBin",
                table: "ItemStocks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemDiscountGroup",
                table: "Items",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemPriceListGroup",
                table: "Items",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemPurchaseQty",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemStockGroup",
                table: "Items",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemStockBin",
                table: "ItemStocks");

            migrationBuilder.DropColumn(
                name: "ItemDiscountGroup",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemPriceListGroup",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemPurchaseQty",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemStockGroup",
                table: "Items");
        }
    }
}
