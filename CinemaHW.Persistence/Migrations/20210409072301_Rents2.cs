using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaHW.Web.Migrations
{
    public partial class Rents2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_AspNetUsers_UserId1",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_UserId1",
                table: "Rent");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Rent");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Rent",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_UserId",
                table: "Rent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_UserId",
                table: "Rent");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rent",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Rent",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_UserId1",
                table: "Rent",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_AspNetUsers_UserId1",
                table: "Rent",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
