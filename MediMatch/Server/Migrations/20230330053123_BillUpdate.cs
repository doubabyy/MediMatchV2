using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMatch.Server.Migrations
{
    public partial class BillUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "paymentType",
                table: "Bills",
                newName: "PaymentType");

            migrationBuilder.RenameColumn(
                name: "cardNum",
                table: "Bills",
                newName: "CardNum");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bills",
                newName: "PatientId");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_received",
                table: "Bills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Date_received",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Bills",
                newName: "paymentType");

            migrationBuilder.RenameColumn(
                name: "CardNum",
                table: "Bills",
                newName: "cardNum");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Bills",
                newName: "UserId");
        }
    }
}
