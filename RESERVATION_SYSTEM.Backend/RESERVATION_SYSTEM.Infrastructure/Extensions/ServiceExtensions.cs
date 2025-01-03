﻿using Microsoft.Extensions.DependencyInjection;
using RESERVATION_SYSTEM.Domain.Services;

namespace RESERVATION_SYSTEM.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var _services = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly =>
                {
                    return assembly.FullName is not null
                        && assembly.FullName.Contains("RESERVATION_SYSTEM.Domain", StringComparison.InvariantCulture);
                })
                .SelectMany(s => s.GetTypes())
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

            foreach (var _service in _services)
            {
                services.AddTransient(_service);
            }

            return services;
        }
    }
}
