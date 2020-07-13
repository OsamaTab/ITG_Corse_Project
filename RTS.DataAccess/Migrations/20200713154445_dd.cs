using Microsoft.EntityFrameworkCore.Migrations;

namespace RTS.DataAccess.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d56e22d-93e5-4864-a94b-1e0d98f2a2ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "881cf1cf-d858-469e-aea6-ddacd61e4d19");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "126fd8e3-851c-4c27-8ac1-99704c58a06f", "b51af477-dbc8-4a65-89d3-9f6f5214553a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1c996ac-eef7-43f6-91a8-50db6ee84568", "a5cbfa67-816a-4d9c-9854-6f7bb2dcb628", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "126fd8e3-851c-4c27-8ac1-99704c58a06f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1c996ac-eef7-43f6-91a8-50db6ee84568");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "881cf1cf-d858-469e-aea6-ddacd61e4d19", "928f0d7e-6bf2-4726-9faa-462cd11cb698", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d56e22d-93e5-4864-a94b-1e0d98f2a2ca", "fbb83d2c-74fa-46d1-b161-a06cddb6f539", "Employee", "EMPLOYEE" });
        }
    }
}
