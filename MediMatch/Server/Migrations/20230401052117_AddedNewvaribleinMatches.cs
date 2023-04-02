using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMatch.Server.Migrations
{
    public partial class AddedNewvaribleinMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedAt",
                table: "Matches",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedAt",
                table: "Matches");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestID);
                });
        }
    }
}
