using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalFinder.Core.Migrations
{
    public partial class MadeTheHospitalIdPropertyOfHospitalUpdateOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalUpdates_Hospitals_HospitalId",
                table: "HospitalUpdates");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "HospitalUpdates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalUpdates_Hospitals_HospitalId",
                table: "HospitalUpdates",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalUpdates_Hospitals_HospitalId",
                table: "HospitalUpdates");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "HospitalUpdates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalUpdates_Hospitals_HospitalId",
                table: "HospitalUpdates",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
