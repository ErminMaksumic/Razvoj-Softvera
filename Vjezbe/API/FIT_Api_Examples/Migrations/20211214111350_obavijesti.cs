using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class obavijesti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Obavijest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naslov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tekst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datum_kreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    evidentiraoKorisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijest", x => x.id);
                    table.ForeignKey(
                        name: "FK_Obavijest_KorisnickiNalog_evidentiraoKorisnikID",
                        column: x => x.evidentiraoKorisnikID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obavijest_evidentiraoKorisnikID",
                table: "Obavijest",
                column: "evidentiraoKorisnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obavijest");
        }
    }
}
