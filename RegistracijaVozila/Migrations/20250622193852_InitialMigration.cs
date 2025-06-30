using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojLicneKarte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Osiguranja",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipOsiguranja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osiguranja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoviVozila",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviVozila", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "MarkeVozila",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipVozilaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipVozilaId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkeVozila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkeVozila_TipoviVozila_TipVozilaId",
                        column: x => x.TipVozilaId,
                        principalTable: "TipoviVozila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarkeVozila_TipoviVozila_TipVozilaId1",
                        column: x => x.TipVozilaId1,
                        principalTable: "TipoviVozila",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModeliVozila",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkaVozilaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeliVozila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeliVozila_MarkeVozila_MarkaVozilaId",
                        column: x => x.MarkaVozilaId,
                        principalTable: "MarkeVozila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vozila",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipVozilaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarkaVozilaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelVozilaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VrstaGorivaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistarskaOznaka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaProizvodnje = table.Column<int>(type: "int", nullable: false),
                    ZapreminaMotora = table.Column<float>(type: "real", nullable: false),
                    Masa = table.Column<float>(type: "real", nullable: false),
                    SnagaMotora = table.Column<int>(type: "int", nullable: false),
                    BrojSasije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRegistracije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumPrveRegistracije = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vozila_MarkeVozila_MarkaVozilaId",
                        column: x => x.MarkaVozilaId,
                        principalTable: "MarkeVozila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vozila_ModeliVozila_ModelVozilaId",
                        column: x => x.ModelVozilaId,
                        principalTable: "ModeliVozila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vozila_TipoviVozila_TipVozilaId",
                        column: x => x.TipVozilaId,
                        principalTable: "TipoviVozila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vozila_VrsteGoriva_VrstaGorivaId",
                        column: x => x.VrstaGorivaId,
                        principalTable: "VrsteGoriva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registracije",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumRegistracije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CijenaRegistracije = table.Column<float>(type: "real", nullable: false),
                    Osiguranje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivremenaRegistracija = table.Column<bool>(type: "bit", nullable: false),
                    KlijentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoziloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OsiguranjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registracije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registracije_Klijenti_KlijentId",
                        column: x => x.KlijentId,
                        principalTable: "Klijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registracije_Vozila_VoziloId",
                        column: x => x.VoziloId,
                        principalTable: "Vozila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OsiguranjeRegistracija",
                columns: table => new
                {
                    OsiguranjeVozilaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistracijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsiguranjeRegistracija", x => new { x.OsiguranjeVozilaId, x.RegistracijaId });
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
                name: "IX_MarkeVozila_TipVozilaId",
                table: "MarkeVozila",
                column: "TipVozilaId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkeVozila_TipVozilaId1",
                table: "MarkeVozila",
                column: "TipVozilaId1");

            migrationBuilder.CreateIndex(
                name: "IX_ModeliVozila_MarkaVozilaId",
                table: "ModeliVozila",
                column: "MarkaVozilaId");

            migrationBuilder.CreateIndex(
                name: "IX_OsiguranjeRegistracija_RegistracijaId",
                table: "OsiguranjeRegistracija",
                column: "RegistracijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Registracije_KlijentId",
                table: "Registracije",
                column: "KlijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Registracije_VoziloId",
                table: "Registracije",
                column: "VoziloId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_MarkaVozilaId",
                table: "Vozila",
                column: "MarkaVozilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_ModelVozilaId",
                table: "Vozila",
                column: "ModelVozilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_TipVozilaId",
                table: "Vozila",
                column: "TipVozilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_VrstaGorivaId",
                table: "Vozila",
                column: "VrstaGorivaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsiguranjeRegistracija");

            migrationBuilder.DropTable(
                name: "Osiguranja");

            migrationBuilder.DropTable(
                name: "Registracije");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Vozila");

            migrationBuilder.DropTable(
                name: "ModeliVozila");

            migrationBuilder.DropTable(
                name: "VrsteGoriva");

            migrationBuilder.DropTable(
                name: "MarkeVozila");

            migrationBuilder.DropTable(
                name: "TipoviVozila");
        }
    }
}
