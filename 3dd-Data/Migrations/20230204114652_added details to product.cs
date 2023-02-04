using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3dd_Data.Migrations
{
    public partial class addeddetailstoproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Products");
        }
    }
}
