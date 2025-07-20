using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class updatebazeosiguranje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsiguranjeRegistracija");

            migrationBuilder.AddColumn<Guid>(
                name: "OsiguranjeId",
                table: "Registracije",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registracije_OsiguranjeId",
                table: "Registracije",
                column: "OsiguranjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registracije_Osiguranja_OsiguranjeId",
                table: "Registracije",
                column: "OsiguranjeId",
                principalTable: "Osiguranja",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registracije_Osiguranja_OsiguranjeId",
                table: "Registracije");

            migrationBuilder.DropIndex(
                name: "IX_Registracije_OsiguranjeId",
                table: "Registracije");

            migrationBuilder.DropColumn(
                name: "OsiguranjeId",
                table: "Registracije");

            migrationBuilder.CreateTable(
                name: "OsiguranjeRegistracija",
                columns: table => new
                {
                    RegistracijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsiguranjeVozilaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsiguranjeRegistracija", x => new { x.RegistracijaId, x.OsiguranjeVozilaId });
                    table.ForeignKey(
                        name: "FK_OsiguranjeRegistracija_Osiguranja_OsiguranjeVozilaId",
                        column: x => x.OsiguranjeVozilaId,
                        principalTable: "Osiguranja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OsiguranjeRegistracija_Registracije_RegistracijaId",
                        column: x => x.RegistracijaId,
                        principalTable: "Registracije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsiguranjeRegistracija_OsiguranjeVozilaId",
                table: "OsiguranjeRegistracija",
                column: "OsiguranjeVozilaId");
        }
    }
}
