using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudKitchenERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBestSeller",
                table: "MenuItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTodaySpecial",
                table: "MenuItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PreparationTime",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBestSeller",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "IsTodaySpecial",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "MenuItems");
        }
    }
}
