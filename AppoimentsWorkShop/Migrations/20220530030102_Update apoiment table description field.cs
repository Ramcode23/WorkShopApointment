using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppoimentsWorkShop.Migrations
{
    public partial class Updateapoimenttabledescriptionfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decripcion",
                table: "Appointments",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Appointments",
                newName: "Decripcion");
        }
    }
}
