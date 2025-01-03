using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESERVATION_SYSTEM.Infrastructure.Migrations
{
    public partial class SeAgreganDatosSemillaParaEntidadesCustomerYSpacesOServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Customer",
                columns: new[] { "Id", "DateRegistration", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("4c3aef5e-d05e-456c-8008-98db1dca32de"), new DateTime(2025, 1, 3, 4, 21, 30, 12, DateTimeKind.Utc).AddTicks(8496), "jamontoya@example.com", "Jorge Montoya", "987654321" },
                    { new Guid("53642f6c-85a5-4c73-b812-cd1a7c3715c1"), new DateTime(2025, 1, 3, 4, 21, 30, 12, DateTimeKind.Utc).AddTicks(8487), "heylers03@gmail.com", "Heyler Montoya", "123456789" },
                    { new Guid("e84a0d01-bf19-47cd-8a9e-7627d27c6617"), new DateTime(2025, 1, 3, 4, 21, 30, 12, DateTimeKind.Utc).AddTicks(8501), "kleyva@example.com", "Kevin Leyva", "987654321" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Service",
                columns: new[] { "Id", "Capacity", "Description", "MaximumReservationTime", "MinimumReservationTime", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("07601682-d4fa-4811-3d68-08dd29993bc7"), 10, "Sala equipada con pantalla y videoconferencia para reuniones de equipo.", 2, 1, "Sala de Reuniones A", 50f },
                    { new Guid("1ce79359-9dec-4606-3d6c-08dd29993bc7"), 15, "Espacio creativo con herramientas tecnológicas como impresoras 3D y estaciones de trabajo.", 2, 1, "Laboratorio de Innovación", 150f },
                    { new Guid("2f784265-cdcd-43a1-3d73-08dd29993bc7"), 10, "Espacio avanzado con equipos de realidad virtual y simulación.", 2, 1, "Centro de Innovación Tecnológica", 200f },
                    { new Guid("3bdeb5a9-993b-4ce7-3d70-08dd29993bc7"), 40, "Espacio configurable para actividades como yoga, talleres o eventos corporativos.", 2, 1, "Salón Multiusos", 70f },
                    { new Guid("4bc33543-2132-43f0-3d72-08dd29993bc7"), 25, "Sala oscura con pantalla gigante y sistema de sonido envolvente, ideal para presentaciones multimedia.", 2, 1, "Sala de Proyecciones", 90f },
                    { new Guid("7a1649d9-5fa2-4c5a-3d6b-08dd29993bc7"), 30, "Área compartida con escritorios y conexión a internet de alta velocidad.", 2, 1, "Espacio de Coworking", 20f },
                    { new Guid("8e37fd17-4f1c-41d3-3d71-08dd29993bc7"), 20, "Sala equipada con mesas, sillas y proyector, ideal para talleres y cursos.", 2, 1, "Área de Capacitación", 60f },
                    { new Guid("99d18fd9-5afd-490a-3d6d-08dd29993bc7"), 4, "Oficina cerrada y equipada con escritorios, sillas ergonómicas y conexión a internet.", 2, 1, "Oficina Privada 1", 40f },
                    { new Guid("a8623c60-3d75-4867-3d6e-08dd29993bc7"), 5, "Oficina privada con vista al exterior y acceso a equipo de oficina básico.", 2, 1, "Oficina Privada 2", 45f },
                    { new Guid("a9cbd100-c74d-40d7-3d69-08dd29993bc7"), 8, "Sala moderna con una mesa redonda y pizarra para sesiones de brainstorming.", 2, 1, "Sala de Reuniones B", 40f },
                    { new Guid("c1617b5a-bafb-48f6-3d6a-08dd29993bc7"), 200, "Espacio para eventos grandes con capacidad para presentaciones o capacitaciones.", 2, 1, "Auditorio", 200f },
                    { new Guid("d17daf7a-ebb8-4063-3d67-08dd29993bc7"), 50, "Espacio amplio equipado con proyector, sistema de audio y micrófonos, ideal para conferencias y reuniones grandes.", 2, 1, "Sala de Conferencias Principal", 100f },
                    { new Guid("d5c6082a-f9d3-453d-3d6f-08dd29993bc7"), 80, "Espacio al aire libre ideal para eventos sociales o reuniones informales.", 2, 1, "Terraza para Eventos", 120f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("4c3aef5e-d05e-456c-8008-98db1dca32de"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("53642f6c-85a5-4c73-b812-cd1a7c3715c1"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("e84a0d01-bf19-47cd-8a9e-7627d27c6617"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("07601682-d4fa-4811-3d68-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("1ce79359-9dec-4606-3d6c-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("2f784265-cdcd-43a1-3d73-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("3bdeb5a9-993b-4ce7-3d70-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("4bc33543-2132-43f0-3d72-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("7a1649d9-5fa2-4c5a-3d6b-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("8e37fd17-4f1c-41d3-3d71-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("99d18fd9-5afd-490a-3d6d-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("a8623c60-3d75-4867-3d6e-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("a9cbd100-c74d-40d7-3d69-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("c1617b5a-bafb-48f6-3d6a-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("d17daf7a-ebb8-4063-3d67-08dd29993bc7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Service",
                keyColumn: "Id",
                keyValue: new Guid("d5c6082a-f9d3-453d-3d6f-08dd29993bc7"));
        }
    }
}
