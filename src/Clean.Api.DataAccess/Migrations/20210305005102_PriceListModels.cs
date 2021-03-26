using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class PriceListModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    PriceListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceListDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrandCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.PriceListId);
                    table.ForeignKey(
                        name: "FK_PriceLists_Brand_BrandCode",
                        column: x => x.BrandCode,
                        principalTable: "Brand",
                        principalColumn: "BrandCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPrice",
                columns: table => new
                {
                    ItemPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ItemPriceUnitPrice = table.Column<double>(type: "float", nullable: false),
                    ItemPriceUnitCost = table.Column<double>(type: "float", nullable: false),
                    ItemPriceIncludesGST = table.Column<bool>(type: "bit", nullable: false),
                    ItemPriceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPrice", x => x.ItemPriceId);
                    table.ForeignKey(
                        name: "FK_ItemPrice_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPrice_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "PriceListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrice_ItemId",
                table: "ItemPrice",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrice_PriceListId",
                table: "ItemPrice",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_BrandCode",
                table: "PriceLists",
                column: "BrandCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPrice");

            migrationBuilder.DropTable(
                name: "PriceLists");
        }
    }
}
