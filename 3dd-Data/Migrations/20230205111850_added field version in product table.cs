using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3dd_Data.Migrations
{
    public partial class addedfieldversioninproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Products");
        }
    }
}
