using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class upisgodinestudija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkademskaGodina",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkademskaGodina", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UpisUAkGodinu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datum4_LjetniOvjera = table.Column<DateTime>(type: "datetime2", nullable: true),
                    datum3_LjetniUpis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    datum2_ZimskiOvjera = table.Column<DateTime>(type: "datetime2", nullable: true),
                    datum1_ZimskiUpis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    godinaStudija = table.Column<int>(type: "int", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    akademskaGodinaId = table.Column<int>(type: "int", nullable: false),
                    cijenaSkolarine = table.Column<float>(type: "real", nullable: true),
                    evidentiraoKorisnikId = table.Column<int>(type: "int", nullable: false),
                    obnovaGodine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisUAkGodinu", x => x.id);
                    table.ForeignKey(
                        name: "FK_UpisUAkGodinu_AkademskaGodina_akademskaGodinaId",
                        column: x => x.akademskaGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisUAkGodinu_KorisnickiNalog_evidentiraoKorisnikId",
                        column: x => x.evidentiraoKorisnikId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisUAkGodinu_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpisUAkGodinu_akademskaGodinaId",
                table: "UpisUAkGodinu",
                column: "akademskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUAkGodinu_evidentiraoKorisnikId",
                table: "UpisUAkGodinu",
                column: "evidentiraoKorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUAkGodinu_studentId",
                table: "UpisUAkGodinu",
                column: "studentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpisUAkGodinu");

            migrationBuilder.DropTable(
                name: "AkademskaGodina");
        }
    }
}
