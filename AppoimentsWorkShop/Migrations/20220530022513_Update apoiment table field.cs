using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppoimentsWorkShop.Migrations
{
    public partial class Updateapoimenttablefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_CreatedById",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_UpdatedById",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Appointments",
                newName: "WorkShopId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Appointments",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UpdatedById",
                table: "Appointments",
                newName: "IX_Appointments_WorkShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CreatedById",
                table: "Appointments",
                newName: "IX_Appointments_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_ClientId",
                table: "Appointments",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_WorkShopId",
                table: "Appointments",
                column: "WorkShopId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_ClientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_WorkShopId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "WorkShopId",
                table: "Appointments",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Appointments",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_WorkShopId",
                table: "Appointments",
                newName: "IX_Appointments_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                newName: "IX_Appointments_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_CreatedById",
                table: "Appointments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_UpdatedById",
                table: "Appointments",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
