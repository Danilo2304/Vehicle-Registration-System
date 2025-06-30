using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class vrstagorivatableremove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_VrsteGoriva_VrstaGorivaId",
                table: "Vozila");

            migrationBuilder.DropTable(
                name: "VrsteGoriva");

            migrationBuilder.DropIndex(
                name: "IX_Vozila_VrstaGorivaId",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "VrstaGorivaId",
                table: "Vozila");

            migrationBuilder.AddColumn<string>(
                name: "VrstaGoriva",
                table: "Vozila",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VrstaGoriva",
                table: "Vozila");

            migrationBuilder.AddColumn<Guid>(
                name: "VrstaGorivaId",
                table: "Vozila",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VrsteGoriva",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteGoriva", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_VrstaGorivaId",
                table: "Vozila",
                column: "VrstaGorivaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_VrsteGoriva_VrstaGorivaId",
                table: "Vozila",
                column: "VrstaGorivaId",
                principalTable: "VrsteGoriva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
