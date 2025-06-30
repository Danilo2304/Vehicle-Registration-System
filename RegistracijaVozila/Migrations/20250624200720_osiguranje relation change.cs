using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class osiguranjerelationchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OsiguranjeRegistracija",
                table: "OsiguranjeRegistracija");

            migrationBuilder.DropIndex(
                name: "IX_OsiguranjeRegistracija_RegistracijaId",
                table: "OsiguranjeRegistracija");

            migrationBuilder.DropColumn(
                name: "OsiguranjeId",
                table: "Registracije");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OsiguranjeRegistracija",
                table: "OsiguranjeRegistracija",
                columns: new[] { "RegistracijaId", "OsiguranjeVozilaId" });

            migrationBuilder.CreateIndex(
                name: "IX_OsiguranjeRegistracija_OsiguranjeVozilaId",
                table: "OsiguranjeRegistracija",
                column: "OsiguranjeVozilaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OsiguranjeRegistracija",
                table: "OsiguranjeRegistracija");

            migrationBuilder.DropIndex(
                name: "IX_OsiguranjeRegistracija_OsiguranjeVozilaId",
                table: "OsiguranjeRegistracija");

            migrationBuilder.AddColumn<Guid>(
                name: "OsiguranjeId",
                table: "Registracije",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OsiguranjeRegistracija",
                table: "OsiguranjeRegistracija",
                columns: new[] { "OsiguranjeVozilaId", "RegistracijaId" });

            migrationBuilder.CreateIndex(
                name: "IX_OsiguranjeRegistracija_RegistracijaId",
                table: "OsiguranjeRegistracija",
                column: "RegistracijaId");
        }
    }
}
