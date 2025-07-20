using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class osiguranjenonnulltyperemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registracije_Osiguranja_OsiguranjeId",
                table: "Registracije");

            migrationBuilder.DropColumn(
                name: "TipOsiguranja",
                table: "Osiguranja");

            migrationBuilder.AlterColumn<Guid>(
                name: "OsiguranjeId",
                table: "Registracije",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registracije_Osiguranja_OsiguranjeId",
                table: "Registracije",
                column: "OsiguranjeId",
                principalTable: "Osiguranja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registracije_Osiguranja_OsiguranjeId",
                table: "Registracije");

            migrationBuilder.AlterColumn<Guid>(
                name: "OsiguranjeId",
                table: "Registracije",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "TipOsiguranja",
                table: "Osiguranja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Registracije_Osiguranja_OsiguranjeId",
                table: "Registracije",
                column: "OsiguranjeId",
                principalTable: "Osiguranja",
                principalColumn: "Id");
        }
    }
}
