using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESERVATION_SYSTEM.Infrastructure.Migrations
{
    public partial class CambiosEntityReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                schema: "dbo",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Total",
                schema: "dbo",
                table: "Reservation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                schema: "dbo",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Total",
                schema: "dbo",
                table: "Reservation",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
