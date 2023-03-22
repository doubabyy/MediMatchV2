using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMatch.Server.Data.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageFromID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageToID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                  
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_MessageFromID",
                        column: x => x.MessageFromID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_MessageFromToID",
                        column: x => x.MessageToID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageFromID",
                table: "Messages",
                column: "MessageFromID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageToID",
                table: "Messages",
                column: "MessageToID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
