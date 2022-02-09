using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class prijavaispitaveza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PrijavaIspita_IspitID",
                table: "PrijavaIspita",
                column: "IspitID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaIspita_StudentID",
                table: "PrijavaIspita",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_PrijavaIspita_Ispit_IspitID",
                table: "PrijavaIspita",
                column: "IspitID",
                principalTable: "Ispit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrijavaIspita_Student_StudentID",
                table: "PrijavaIspita",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrijavaIspita_Ispit_IspitID",
                table: "PrijavaIspita");

            migrationBuilder.DropForeignKey(
                name: "FK_PrijavaIspita_Student_StudentID",
                table: "PrijavaIspita");

            migrationBuilder.DropIndex(
                name: "IX_PrijavaIspita_IspitID",
                table: "PrijavaIspita");

            migrationBuilder.DropIndex(
                name: "IX_PrijavaIspita_StudentID",
                table: "PrijavaIspita");
        }
    }
}
