using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistracijaVozila.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Addauthdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93e88597-7567-4e0a-af32-d3c79597ab97",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33c90c88-b3ea-4817-aad9-b97d6b07c0c9", "AQAAAAIAAYagAAAAEE40hl+lt+NiofVN8FKrkjbJKh0Hrcnq+qSpVp9vAs1IK6bZyDB8l+QXBslAbduvrA==", "096e36b8-1f3f-4f56-a611-dfbae42cf2e3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93e88597-7567-4e0a-af32-d3c79597ab97",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4acecdf-bdd9-445d-8428-1f6d2a3a16f8", "AQAAAAIAAYagAAAAEBn1wo8BQmhwZYZYTtCXleQgPpW6QtmyOOdTvemWEJPXhoAezytTqJHO/smQjDNYmg==", "3ab76896-5de7-4e19-a9ab-16c398157e3f" });
        }
    }
}
