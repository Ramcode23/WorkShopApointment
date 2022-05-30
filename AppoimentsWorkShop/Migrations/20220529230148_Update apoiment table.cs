using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppoimentsWorkShop.Migrations
{
    public partial class Updateapoimenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");
        }
    }
}
