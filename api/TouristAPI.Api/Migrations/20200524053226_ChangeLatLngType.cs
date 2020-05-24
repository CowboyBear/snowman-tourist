using Microsoft.EntityFrameworkCore.Migrations;

namespace TouristAPI.Api.Migrations
{
    public partial class ChangeLatLngType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Lng",
                table: "location",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "location",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Lng",
                table: "location",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Lat",
                table: "location",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
