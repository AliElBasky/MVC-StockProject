using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeZoneStock.Migrations
{
    public partial class AddQuantityColumnToStoreItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "StoreItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "StoreItem");
        }
    }
}
