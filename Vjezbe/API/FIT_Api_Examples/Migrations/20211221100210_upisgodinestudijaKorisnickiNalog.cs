using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class upisgodinestudijaKorisnickiNalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datum_added",
                table: "AkademskaGodina",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "datum_update",
                table: "AkademskaGodina",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "evidentiraoKorisnikid",
                table: "AkademskaGodina",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "izmijenioKorisnikid",
                table: "AkademskaGodina",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkademskaGodina_evidentiraoKorisnikid",
                table: "AkademskaGodina",
                column: "evidentiraoKorisnikid");

            migrationBuilder.CreateIndex(
                name: "IX_AkademskaGodina_izmijenioKorisnikid",
                table: "AkademskaGodina",
                column: "izmijenioKorisnikid");

            migrationBuilder.AddForeignKey(
                name: "FK_AkademskaGodina_KorisnickiNalog_evidentiraoKorisnikid",
                table: "AkademskaGodina",
                column: "evidentiraoKorisnikid",
                principalTable: "KorisnickiNalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkademskaGodina_KorisnickiNalog_izmijenioKorisnikid",
                table: "AkademskaGodina",
                column: "izmijenioKorisnikid",
                principalTable: "KorisnickiNalog",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkademskaGodina_KorisnickiNalog_evidentiraoKorisnikid",
                table: "AkademskaGodina");

            migrationBuilder.DropForeignKey(
                name: "FK_AkademskaGodina_KorisnickiNalog_izmijenioKorisnikid",
                table: "AkademskaGodina");

            migrationBuilder.DropIndex(
                name: "IX_AkademskaGodina_evidentiraoKorisnikid",
                table: "AkademskaGodina");

            migrationBuilder.DropIndex(
                name: "IX_AkademskaGodina_izmijenioKorisnikid",
                table: "AkademskaGodina");

            migrationBuilder.DropColumn(
                name: "datum_added",
                table: "AkademskaGodina");

            migrationBuilder.DropColumn(
                name: "datum_update",
                table: "AkademskaGodina");

            migrationBuilder.DropColumn(
                name: "evidentiraoKorisnikid",
                table: "AkademskaGodina");

            migrationBuilder.DropColumn(
                name: "izmijenioKorisnikid",
                table: "AkademskaGodina");
        }
    }
}
