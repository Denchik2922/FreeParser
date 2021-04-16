using Microsoft.EntityFrameworkCore.Migrations;

namespace DBL.Migrations
{
    public partial class Createlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Burses_BurseId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "BurseId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Burses_BurseId",
                table: "Categories",
                column: "BurseId",
                principalTable: "Burses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Burses_BurseId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "BurseId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Burses_BurseId",
                table: "Categories",
                column: "BurseId",
                principalTable: "Burses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
