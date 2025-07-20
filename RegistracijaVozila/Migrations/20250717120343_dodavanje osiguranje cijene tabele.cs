using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjeosiguranjecijenetabele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OsiguranjeCijene",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsiguranjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinKw = table.Column<int>(type: "int", nullable: false),
                    MaxKw = table.Column<int>(type: "int", nullable: false),
                    PricePerKw = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsiguranjeCijene", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsiguranjeCijene_Osiguranja_OsiguranjeId",
                        column: x => x.OsiguranjeId,
                        principalTable: "Osiguranja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsiguranjeCijene_OsiguranjeId",
                table: "OsiguranjeCijene",
                column: "OsiguranjeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsiguranjeCijene");
        }
    }
}
