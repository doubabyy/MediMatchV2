using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMatch.Server.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "AcceptsInsurance",
                table: "Doctor",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "AcceptsInsurance",
                table: "Doctor",
                type: "nvarchar(3)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
