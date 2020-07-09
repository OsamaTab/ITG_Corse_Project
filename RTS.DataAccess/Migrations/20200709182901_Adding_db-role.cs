using Microsoft.EntityFrameworkCore.Migrations;

namespace RTS.DataAccess.Migrations
{
    public partial class Adding_dbrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85abf0d4-d9df-4692-a96f-0ae2a4c6b48a", "c6bc4f23-9698-4629-9a74-1a52ac5c443b", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d47e176a-151d-4b49-bc7e-a8536c6de91f", "1789961f-6a68-4fee-a9dc-27a80f907e8b", "Employee", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85abf0d4-d9df-4692-a96f-0ae2a4c6b48a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d47e176a-151d-4b49-bc7e-a8536c6de91f");
        }
    }
}
