using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyEmployees.Migrations
{
    public partial class identitydatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a924aae-2eef-4cbb-9044-38c25750cac7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b14f3434-9967-4a2d-bc3c-29fe53392d09");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34972bd2-354f-4031-8694-7e68ac55a4f5", "e114a8ad-b516-4e89-a9fd-7c68d1d6f987", "Manger", "MANGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0de5cef-1947-4fa0-8972-4b1fa7d17c22", "5dc9c5c5-c235-4035-91d4-4990842684d7", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34972bd2-354f-4031-8694-7e68ac55a4f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0de5cef-1947-4fa0-8972-4b1fa7d17c22");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4a924aae-2eef-4cbb-9044-38c25750cac7", "d03dd519-d4d3-402d-89a9-0db3d9ab397e", "Manger", "MANGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b14f3434-9967-4a2d-bc3c-29fe53392d09", "79878bbe-48a9-4cae-ac58-1271f38b90e5", "Administrator", "ADMINISTRATOR" });
        }
    }
}
