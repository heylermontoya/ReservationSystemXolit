using RESERVATION_SYSTEM.Api.Tests.DataBuilder;
using RESERVATION_SYSTEM.Infrastructure.Context;

namespace RESERVATION_SYSTEM.Api.Tests.ConfigureDataSeed
{
    public static class ServiceSeed
    {
        public static void ConfigureDataSeed(PersistenceContext context)
        {
            ServiceBuilder _builderRegisterForDeleted = new();

            context.Add(
                _builderRegisterForDeleted
                    .WithId(new("57e66b0c-0152-4041-aa33-8fc79612889c"))
                    .Build()
            );
        }
    }
}
