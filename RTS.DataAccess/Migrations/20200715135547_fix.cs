using Microsoft.EntityFrameworkCore.Migrations;

namespace RTS.DataAccess.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemRequests_AspNetUsers_RequestedUserId",
                table: "itemRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_CurentUserId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "CurentUserId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedUserId",
                table: "itemRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ItemOwner",
                table: "itemRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_itemRequests_AspNetUsers_RequestedUserId",
                table: "itemRequests",
                column: "RequestedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_CurentUserId",
                table: "Items",
                column: "CurentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemRequests_AspNetUsers_RequestedUserId",
                table: "itemRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_CurentUserId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "CurentUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestedUserId",
                table: "itemRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOwner",
                table: "itemRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_itemRequests_AspNetUsers_RequestedUserId",
                table: "itemRequests",
                column: "RequestedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_CurentUserId",
                table: "Items",
                column: "CurentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
