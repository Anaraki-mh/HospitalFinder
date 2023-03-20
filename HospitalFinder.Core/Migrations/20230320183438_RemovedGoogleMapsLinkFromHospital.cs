using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalFinder.Core.Migrations
{
    public partial class RemovedGoogleMapsLinkFromHospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMapsLink",
                table: "Hospitals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLink",
                table: "Hospitals",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: true);
        }
    }
}
