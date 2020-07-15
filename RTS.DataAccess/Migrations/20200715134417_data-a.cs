using Microsoft.EntityFrameworkCore.Migrations;

namespace RTS.DataAccess.Migrations
{
    public partial class dataa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RequestStatuses",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "approved" },
                    { 2, "pending" },
                    { 3, "rejected" },
                    { 4, "Returnd" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
