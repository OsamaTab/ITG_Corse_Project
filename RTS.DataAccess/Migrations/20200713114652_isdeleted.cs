using Microsoft.EntityFrameworkCore.Migrations;

namespace RTS.DataAccess.Migrations
{
    public partial class isdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ceb67af-72c2-4816-abf7-b981699e7222");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "880a526a-8c9f-43dc-beb0-0e16d56b3dac");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "itemCategories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "881cf1cf-d858-469e-aea6-ddacd61e4d19", "928f0d7e-6bf2-4726-9faa-462cd11cb698", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d56e22d-93e5-4864-a94b-1e0d98f2a2ca", "fbb83d2c-74fa-46d1-b161-a06cddb6f539", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d56e22d-93e5-4864-a94b-1e0d98f2a2ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "881cf1cf-d858-469e-aea6-ddacd61e4d19");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "itemCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ceb67af-72c2-4816-abf7-b981699e7222", "558d893e-5cf9-4519-a835-9c81038b5832", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "880a526a-8c9f-43dc-beb0-0e16d56b3dac", "935509c6-6489-4fc1-9edc-85789366b83c", "Employee", "EMPLOYEE" });
        }
    }
}
