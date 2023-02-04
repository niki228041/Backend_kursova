using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3dd_Data.Migrations
{
    public partial class configuratedcompanytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Companys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Companys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Companys");
        }
    }
}
