using Microsoft.EntityFrameworkCore;
using RESERVATION_SYSTEM.Domain.Entities.service;

namespace RESERVATION_SYSTEM.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureService
    {
        internal static void ConfigureModelService(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = Guid.Parse("D17DAF7A-EBB8-4063-3D67-08DD29993BC7"),
                    Name = "Sala de Conferencias Principal",
                    Description = "Espacio amplio equipado con proyector, sistema de audio y micrófonos, ideal para conferencias y reuniones grandes.",
                    Price = 100,
                    Capacity = 50,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("07601682-D4FA-4811-3D68-08DD29993BC7"),
                    Name = "Sala de Reuniones A",
                    Description = "Sala equipada con pantalla y videoconferencia para reuniones de equipo.",
                    Price = 50,
                    Capacity = 10,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("A9CBD100-C74D-40D7-3D69-08DD29993BC7"),
                    Name = "Sala de Reuniones B",
                    Description = "Sala moderna con una mesa redonda y pizarra para sesiones de brainstorming.",
                    Price = 40,
                    Capacity = 8,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("C1617B5A-BAFB-48F6-3D6A-08DD29993BC7"),
                    Name = "Auditorio",
                    Description = "Espacio para eventos grandes con capacidad para presentaciones o capacitaciones.",
                    Price = 200,
                    Capacity = 200,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("7A1649D9-5FA2-4C5A-3D6B-08DD29993BC7"),
                    Name = "Espacio de Coworking",
                    Description = "Área compartida con escritorios y conexión a internet de alta velocidad.",
                    Price = 20,
                    Capacity = 30,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("1CE79359-9DEC-4606-3D6C-08DD29993BC7"),
                    Name = "Laboratorio de Innovación",
                    Description = "Espacio creativo con herramientas tecnológicas como impresoras 3D y estaciones de trabajo.",
                    Price = 150,
                    Capacity = 15,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("99D18FD9-5AFD-490A-3D6D-08DD29993BC7"),
                    Name = "Oficina Privada 1",
                    Description = "Oficina cerrada y equipada con escritorios, sillas ergonómicas y conexión a internet.",
                    Price = 40,
                    Capacity = 4,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("A8623C60-3D75-4867-3D6E-08DD29993BC7"),
                    Name = "Oficina Privada 2",
                    Description = "Oficina privada con vista al exterior y acceso a equipo de oficina básico.",
                    Price = 45,
                    Capacity = 5,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("D5C6082A-F9D3-453D-3D6F-08DD29993BC7"),
                    Name = "Terraza para Eventos",
                    Description = "Espacio al aire libre ideal para eventos sociales o reuniones informales.",
                    Price = 120,
                    Capacity = 80,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("3BDEB5A9-993B-4CE7-3D70-08DD29993BC7"),
                    Name = "Salón Multiusos",
                    Description = "Espacio configurable para actividades como yoga, talleres o eventos corporativos.",
                    Price = 70,
                    Capacity = 40,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("8E37FD17-4F1C-41D3-3D71-08DD29993BC7"),
                    Name = "Área de Capacitación",
                    Description = "Sala equipada con mesas, sillas y proyector, ideal para talleres y cursos.",
                    Price = 60,
                    Capacity = 20,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("4BC33543-2132-43F0-3D72-08DD29993BC7"),
                    Name = "Sala de Proyecciones",
                    Description = "Sala oscura con pantalla gigante y sistema de sonido envolvente, ideal para presentaciones multimedia.",
                    Price = 90,
                    Capacity = 25,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                },
                new Service
                {
                    Id = Guid.Parse("2F784265-CDCD-43A1-3D73-08DD29993BC7"),
                    Name = "Centro de Innovación Tecnológica",
                    Description = "Espacio avanzado con equipos de realidad virtual y simulación.",
                    Price = 200,
                    Capacity = 10,
                    MaximumReservationTime = 2,
                    MinimumReservationTime = 1
                }
            );
        }
    }
}
