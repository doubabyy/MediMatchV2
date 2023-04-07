using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMatch.Server.Migrations
{
    public partial class FK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_DoctorId",
                table: "Matches",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PatientId",
                table: "Matches",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_DoctorId",
                table: "Bills",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientId",
                table: "Bills",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Doctors_DoctorId",
                table: "Bills",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Doctors_DoctorId",
                table: "Matches",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Patients_PatientId",
                table: "Matches",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Doctors_DoctorId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Doctors_DoctorId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Patients_PatientId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_DoctorId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_PatientId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Bills_DoctorId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PatientId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
