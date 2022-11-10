using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalFinder.Core.Migrations
{
    public partial class AddedOperationTypeToHospitalUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperationType",
                table: "HospitalUpdates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationType",
                table: "HospitalUpdates");
        }
    }
}
