using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updateZawodnika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatystykaId",
                table: "Zawodnicy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Zawodnicy",
                keyColumn: "ZawodnikId",
                keyValue: 1,
                column: "StatystykaId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Zawodnicy",
                keyColumn: "ZawodnikId",
                keyValue: 2,
                column: "StatystykaId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Zawodnicy",
                keyColumn: "ZawodnikId",
                keyValue: 3,
                column: "StatystykaId",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatystykaId",
                table: "Zawodnicy");
        }
    }
}
