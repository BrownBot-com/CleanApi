using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean.Api.DataAccess.Migrations
{
    public partial class AddedItemDiscountGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemDiscountGroup",
                columns: table => new
                {
                    ItemDiscGrpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemDiscGrpDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ItemDiscGrpAdditional = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ItemDiscGrpSupplierRef = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDiscountGroup", x => x.ItemDiscGrpCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDiscountGroup");
        }
    }
}
