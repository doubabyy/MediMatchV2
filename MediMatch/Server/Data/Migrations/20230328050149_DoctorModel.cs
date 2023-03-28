using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMatch.Server.Data.Migrations
{
    public partial class DoctorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(400)", nullable: false),
                    Rates = table.Column<int>(type: "int", nullable: false),
                    AcceptsInsurance = table.Column<string>(type: "nvarchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_Doctor_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");
        }
    }
}
