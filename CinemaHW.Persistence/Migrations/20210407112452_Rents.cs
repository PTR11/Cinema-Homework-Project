using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaHW.Web.Migrations
{
    public partial class Rents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Room_RoomId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_RoomId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "RentId",
                table: "Places",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rent_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rent_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_RentId",
                table: "Places",
                column: "RentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_ProgramId",
                table: "Rent",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_UserId1",
                table: "Rent",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Rent_RentId",
                table: "Places",
                column: "RentId",
                principalTable: "Rent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Rent_RentId",
                table: "Places");

            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Places_RentId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "RentId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Places_RoomId",
                table: "Places",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Room_RoomId",
                table: "Places",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
