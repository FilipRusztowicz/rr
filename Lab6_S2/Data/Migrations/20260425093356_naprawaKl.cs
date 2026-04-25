using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class naprawaKl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kluby",
                columns: table => new
                {
                    KlubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rokZalozenia = table.Column<byte>(type: "TINYINT", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kluby", x => x.KlubId);
                });

            migrationBuilder.CreateTable(
                name: "Zawodnicy",
                columns: table => new
                {
                    ZawodnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nrKoszulki = table.Column<byte>(type: "TINYINT", nullable: false),
                    kondycja = table.Column<decimal>(type: "NUMERIC(2,1)", nullable: false),
                    czyKontuzja = table.Column<bool>(type: "bit", nullable: false),
                    imie = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    KlubId = table.Column<int>(type: "int", nullable: true),
                    StatystykaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zawodnicy", x => x.ZawodnikId);
                    table.ForeignKey(
                        name: "FK_Zawodnicy_Kluby_KlubId",
                        column: x => x.KlubId,
                        principalTable: "Kluby",
                        principalColumn: "KlubId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statystyki",
                columns: table => new
                {
                    StatystykaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rozegraneMecze = table.Column<byte>(type: "TINYINT", nullable: false),
                    zdobyteGole = table.Column<byte>(type: "TINYINT", nullable: false),
                    ZawodnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statystyki", x => x.StatystykaId);
                    table.ForeignKey(
                        name: "FK_Statystyki_Zawodnicy_ZawodnikId",
                        column: x => x.ZawodnikId,
                        principalTable: "Zawodnicy",
                        principalColumn: "ZawodnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kluby",
                columns: new[] { "KlubId", "Nazwa", "rokZalozenia" },
                values: new object[,]
                {
                    { 1, "Legia Warszawa", (byte)227 },
                    { 2, "Lech Poznań", (byte)153 }
                });

            migrationBuilder.InsertData(
                table: "Zawodnicy",
                columns: new[] { "ZawodnikId", "KlubId", "StatystykaId", "czyKontuzja", "imie", "kondycja", "nrKoszulki" },
                values: new object[,]
                {
                    { 1, 1, 0, false, "Robert", 9.5m, (byte)9 },
                    { 2, 2, 0, true, "Kamil", 7m, (byte)15 },
                    { 3, 2, 0, false, "Wojciech", 8m, (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "Statystyki",
                columns: new[] { "StatystykaId", "ZawodnikId", "rozegraneMecze", "zdobyteGole" },
                values: new object[,]
                {
                    { 1, 1, (byte)120, (byte)85 },
                    { 2, 2, (byte)45, (byte)5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statystyki_ZawodnikId",
                table: "Statystyki",
                column: "ZawodnikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zawodnicy_KlubId",
                table: "Zawodnicy",
                column: "KlubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statystyki");

            migrationBuilder.DropTable(
                name: "Zawodnicy");

            migrationBuilder.DropTable(
                name: "Kluby");
        }
    }
}
