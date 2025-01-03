using Microsoft.EntityFrameworkCore;
using RESERVATION_SYSTEM.Domain.Entities.customer;

namespace RESERVATION_SYSTEM.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureCustomer
    {
        internal static void ConfigureModelCustomer(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Heyler Montoya",
                    Email = "heylers03@gmail.com",
                    Phone = "123456789",
                    DateRegistration = DateTime.UtcNow
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Jorge Montoya",
                    Email = "jamontoya@example.com",
                    Phone = "987654321",
                    DateRegistration = DateTime.UtcNow
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Kevin Leyva",
                    Email = "kleyva@example.com",
                    Phone = "987654321",
                    DateRegistration = DateTime.UtcNow
                }
            );
        }
    }
}
