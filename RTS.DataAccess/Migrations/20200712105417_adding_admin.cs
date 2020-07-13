using Microsoft.EntityFrameworkCore.Migrations;

namespace RTS.DataAccess.Migrations
{
    public partial class adding_admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85abf0d4-d9df-4692-a96f-0ae2a4c6b48a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d47e176a-151d-4b49-bc7e-a8536c6de91f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7D9B7113-A8F8-4035-99A7-A20DD400F6A3", "c28b3d40-dbe7-4a83-b936-056ef693f727", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd5b5d61-af41-49da-b59e-2bcfb46e7c8f", "513f15d7-ab95-496a-b2fa-fbf9ecf42fd8", "Employee", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2301D884-221A-4E7D-B509-0113DCC043E1", 0, "484e3d2e-c522-4d6f-a38e-bd8258940284", "admin@i.com", true, false, null, "admin@i.com", "admin", "AQAAAAEAACcQAAAAEIHA+wI3uNyFVrVB7ibShT1dPgEZmDMPNO0/Y6E7MmTGddXJPfmqUZwX1Ixsy85RkA==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2301D884-221A-4E7D-B509-0113DCC043E1", "7D9B7113-A8F8-4035-99A7-A20DD400F6A3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd5b5d61-af41-49da-b59e-2bcfb46e7c8f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2301D884-221A-4E7D-B509-0113DCC043E1", "7D9B7113-A8F8-4035-99A7-A20DD400F6A3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D9B7113-A8F8-4035-99A7-A20DD400F6A3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85abf0d4-d9df-4692-a96f-0ae2a4c6b48a", "c6bc4f23-9698-4629-9a74-1a52ac5c443b", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d47e176a-151d-4b49-bc7e-a8536c6de91f", "1789961f-6a68-4fee-a9dc-27a80f907e8b", "Employee", null });
        }
    }
}
