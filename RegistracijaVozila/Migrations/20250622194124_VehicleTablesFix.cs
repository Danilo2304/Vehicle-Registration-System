using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class VehicleTablesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkeVozila_TipoviVozila_TipVozilaId1",
                table: "MarkeVozila");

            migrationBuilder.DropIndex(
                name: "IX_MarkeVozila_TipVozilaId1",
                table: "MarkeVozila");

            migrationBuilder.DropColumn(
                name: "TipVozilaId1",
                table: "MarkeVozila");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TipVozilaId1",
                table: "MarkeVozila",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarkeVozila_TipVozilaId1",
                table: "MarkeVozila",
                column: "TipVozilaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkeVozila_TipoviVozila_TipVozilaId1",
                table: "MarkeVozila",
                column: "TipVozilaId1",
                principalTable: "TipoviVozila",
                principalColumn: "Id");
        }
    }
}
