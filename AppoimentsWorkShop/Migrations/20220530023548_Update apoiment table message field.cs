using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppoimentsWorkShop.Migrations
{
    public partial class Updateapoimenttablemessagefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Appointments");
        }
    }
}
