using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class registracijavoziloregOznaka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumRegistracije",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "RegistarskaOznaka",
                table: "Vozila");

            migrationBuilder.AddColumn<string>(
                name: "RegistarskaOznaka",
                table: "Registracije",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistarskaOznaka",
                table: "Registracije");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumRegistracije",
                table: "Vozila",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RegistarskaOznaka",
                table: "Vozila",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
