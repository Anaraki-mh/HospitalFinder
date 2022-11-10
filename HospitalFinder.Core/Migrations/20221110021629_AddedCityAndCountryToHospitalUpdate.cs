using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalFinder.Core.Migrations
{
    public partial class AddedCityAndCountryToHospitalUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullAddress",
                table: "HospitalUpdates",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "HospitalUpdates",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "HospitalUpdates",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "HospitalUpdates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "HospitalUpdates");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "HospitalUpdates");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "HospitalUpdates");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "HospitalUpdates",
                newName: "FullAddress");
        }
    }
}
