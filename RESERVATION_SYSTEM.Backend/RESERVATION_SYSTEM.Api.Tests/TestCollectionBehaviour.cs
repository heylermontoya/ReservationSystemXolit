using Xunit;

namespace RESERVATION_SYSTEM.Api.Tests
{
    [CollectionDefinition("TestCollection", DisableParallelization = false)]
    public class TestCollectionBehaviour : ICollectionFixture<TestStartup<Program>>
    {        
    }
}
