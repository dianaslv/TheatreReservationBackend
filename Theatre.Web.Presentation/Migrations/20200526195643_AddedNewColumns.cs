using Microsoft.EntityFrameworkCore.Migrations;

namespace Theatre.Web.Presentation.Migrations
{
    public partial class AddedNewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Tickets");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Tickets",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Reservations",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reservations",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Reservations",
                type: "varchar(256)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Tickets",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
