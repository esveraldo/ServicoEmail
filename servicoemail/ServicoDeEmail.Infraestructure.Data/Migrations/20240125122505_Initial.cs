using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicoDeEmail.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<string>(type: "varchar(255)", nullable: false),
                    InvalidEmails = table.Column<string>(type: "varchar(255)", nullable: true),
                    Subject = table.Column<string>(type: "varchar(150)", nullable: false),
                    Message = table.Column<string>(type: "varchar(255)", nullable: false),
                    AccessKey = table.Column<string>(type: "varchar(150)", nullable: false),
                    Obs = table.Column<string>(type: "varchar(255)", nullable: true),
                    AttachmentsDocs = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Smtp = table.Column<string>(type: "varchar(255)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    AccessKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false),
                    NameUser = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNameSend = table.Column<string>(type: "varchar(255)", nullable: true),
                    Extension = table.Column<string>(type: "varchar(255)", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    EmailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Consumer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerUsers_ServiceUsers_ServiceUsersId",
                        column: x => x.ServiceUsersId,
                        principalTable: "ServiceUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EmailId",
                table: "Attachments",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerUsers_ServiceUsersId",
                table: "ConsumerUsers",
                column: "ServiceUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "ConsumerUsers");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "ServiceUsers");
        }
    }
}
