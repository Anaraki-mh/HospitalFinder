using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalFinder.Core.Migrations
{
    public partial class AddedHospitalAndHospitalUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longtitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpenTime = table.Column<int>(type: "int", nullable: true),
                    CloseTime = table.Column<int>(type: "int", nullable: true),
                    Telephone = table.Column<long>(type: "bigint", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GoogleMapsLink = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longtitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OpenTime = table.Column<int>(type: "int", nullable: true),
                    CloseTime = table.Column<int>(type: "int", nullable: true),
                    Telephone = table.Column<long>(type: "bigint", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalUpdates_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HospitalUpdates_HospitalId",
                table: "HospitalUpdates",
                column: "HospitalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalUpdates");

            migrationBuilder.DropTable(
                name: "Hospitals");
        }
    }
}
