using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using RESERVATION_SYSTEM.Api.Tests.ConfigureDataSeed;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Infrastructure.Adapters;
using RESERVATION_SYSTEM.Infrastructure.Context;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RESERVATION_SYSTEM.Api.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestStartup<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public TestStartup()
        {
            _scopeFactory = Services.GetService<IServiceScopeFactory>()!;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            InMemoryDatabaseRoot rootDb = new();

            builder.ConfigureServices(services =>
            {

                ServiceDescriptor descriptor = services.SingleOrDefault(
                    descriptors => descriptors.ServiceType == typeof(DbContextOptions<PersistenceContext>)
                )!;

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<PersistenceContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryReservationSystemContext", rootDb);
                    options.EnableServiceProviderCaching(false);
                });

                ServiceProvider sp = services.BuildServiceProvider();
                using IServiceScope scope = sp.CreateScope();
                using PersistenceContext appContext = scope.ServiceProvider.GetRequiredService<PersistenceContext>();
                services.AddTransient(typeof(IQueryWrapper), typeof(DapperWrapper));

                try
                {
                    appContext.Database.EnsureCreated();
                   
                    #region Reservation
                    ReservationSeed.ConfigureDataSeed(appContext);
                    appContext.SaveChanges();
                    #endregion

                    #region Customer
                    CustomerSeed.ConfigureDataSeed(appContext);
                    appContext.SaveChanges();
                    #endregion

                    #region Service
                    ServiceSeed.ConfigureDataSeed(appContext);
                    appContext.SaveChanges();
                    #endregion                                       
                }
                catch (Exception ex)
                {                    
                    throw new AppException(ex.Message);
                }
            });

            builder.UseEnvironment("Development");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
