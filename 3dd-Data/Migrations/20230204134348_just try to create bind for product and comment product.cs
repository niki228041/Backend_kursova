using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3dd_Data.Migrations
{
    public partial class justtrytocreatebindforproductandcommentproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProductComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProductComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
