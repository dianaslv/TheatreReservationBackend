using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Theatre.Web.Presentation.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE DATABASE IF NOT EXISTS Theatre");
            migrationBuilder.CreateTable(
                name: "TheatrePlays",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheatrePlays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(type: "varchar(256)", nullable: true),
                    Password = table.Column<string>(type: "varchar(256)", nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastLogged = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", nullable: true),
                    Address = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    SpectatorId = table.Column<Guid>(nullable: false),
                    TheatrePlayId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_SpectatorId",
                        column: x => x.SpectatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_TheatrePlays_TheatrePlayId",
                        column: x => x.TheatrePlayId,
                        principalTable: "TheatrePlays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Row = table.Column<int>(nullable: false),
                    Section = table.Column<int>(nullable: false),
                    SeatNumber = table.Column<int>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    ReservationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SpectatorId",
                table: "Reservations",
                column: "SpectatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TheatrePlayId",
                table: "Reservations",
                column: "TheatrePlayId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ReservationId",
                table: "Tickets",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TheatrePlays");
        }
    }
}
