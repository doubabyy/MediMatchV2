using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMatch.Server.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DepAnx = table.Column<bool>(type: "bit", nullable: false),
                    DepAnxDesc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SuicThoughts = table.Column<bool>(type: "bit", nullable: false),
                    SuicThoughtsDesc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubstanceAbuse = table.Column<bool>(type: "bit", nullable: false),
                    SubstanceAbuseDesc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupportSystem = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Therapy = table.Column<bool>(type: "bit", nullable: false),
                    TherapyDesc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProblemsDesc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TreatmentGoals = table.Column<string>(type: "nvarchar(450)", nullable: true),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                }); ;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
