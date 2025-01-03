﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Infrastructure.Adapters;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RESERVATION_SYSTEM.Infrastructure.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string stringConnection)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IQueryWrapper), typeof(DapperWrapper));
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient<IDbConnection>(_ => new SqlConnection(stringConnection));

            return services;
        }
    }
}
