using RESERVATION_SYSTEM.Api.Tests.DataBuilder;
using RESERVATION_SYSTEM.Infrastructure.Context;

namespace RESERVATION_SYSTEM.Api.Tests.ConfigureDataSeed
{
    public static class CustomerSeed
    {
        public static void ConfigureDataSeed(PersistenceContext context)
        {
            CustomerBuilder _builderRegisterForDeleted = new();
            context.Add(
                _builderRegisterForDeleted
                    .WithId(new("a2ec74d3-4517-4592-a49e-101808e7481c"))
                    .WithName("Jane Doe")
                    .WithEmail("janedoe@example.com")
                    .WithPhone("098-765-4321")
                    .Build()
            );
        }
    }
}
