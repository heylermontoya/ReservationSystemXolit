using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESERVATION_SYSTEM.Infrastructure.Migrations
{
    public partial class CambiosEntityReservation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumReservationTime",
                schema: "dbo",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumReservationTime",
                schema: "dbo",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumReservationTime",
                schema: "dbo",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "MinimumReservationTime",
                schema: "dbo",
                table: "Service");
        }
    }
}
