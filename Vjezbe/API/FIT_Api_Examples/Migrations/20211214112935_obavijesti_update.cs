using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class obavijesti_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datum_update",
                table: "Obavijest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "izmijenioKorisnikID",
                table: "Obavijest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Obavijest_izmijenioKorisnikID",
                table: "Obavijest",
                column: "izmijenioKorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijest_KorisnickiNalog_izmijenioKorisnikID",
                table: "Obavijest",
                column: "izmijenioKorisnikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obavijest_KorisnickiNalog_izmijenioKorisnikID",
                table: "Obavijest");

            migrationBuilder.DropIndex(
                name: "IX_Obavijest_izmijenioKorisnikID",
                table: "Obavijest");

            migrationBuilder.DropColumn(
                name: "datum_update",
                table: "Obavijest");

            migrationBuilder.DropColumn(
                name: "izmijenioKorisnikID",
                table: "Obavijest");
        }
    }
}
