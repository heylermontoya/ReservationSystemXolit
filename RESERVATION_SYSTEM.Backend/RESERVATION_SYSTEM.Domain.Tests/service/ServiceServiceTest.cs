using NSubstitute;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.service;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;

namespace RESERVATION_SYSTEM.Domain.Tests.service
{
    [TestClass]
    public class ServiceServiceTest
    {
        private ServiceService Service { get; set; } = default!;
        private IGenericRepository<Service> Repository { get; set; } = default!;
        private ServiceBuilder ServiceBuilder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            Repository = Substitute.For<IGenericRepository<Service>>();

            Service = new(
                Repository
            );

            ServiceBuilder = new();
        }

        [TestMethod]
        public async Task CreateServiceAsync_Ok()
        {
            //Arrange
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;
            int minimumReservationTime = 1;
            int maximumReservationTime = 2;

            Service service = ServiceBuilder
                .WithName(name)
                .WithDescription(description)
                .WithPrice(price)
                .WithCapacity(capacity)
                .WithMinimumReservationTime(minimumReservationTime)
                .WithMaximumReservationTime(maximumReservationTime)
                .Build();
            IEnumerable<Service> listService = [];

            Repository.AddAsync(service)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.CreateServiceAsync(service, listService);

            //Assert                        
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<Service>()
                );
        }

        [TestMethod]
        public async Task CreateServiceAsync_Failed()
        {
            //Arrange
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;
            int minimumReservationTime = 1;
            int maximumReservationTime = 2;
            Service service = ServiceBuilder
                .WithName(name)
                .WithDescription(description)
                .WithPrice(price)
                .WithCapacity(capacity)
                .WithMinimumReservationTime(minimumReservationTime)
                .WithMaximumReservationTime(maximumReservationTime)
                .Build();
            IEnumerable<Service> listService = [service];

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateServiceAsync(service, listService);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.NameServiceNotValid, ex.Message);
        }

        [TestMethod]
        public async Task UpdateServiceAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;
            int minimumReservationTime = 1;
            int maximumReservationTime = 2;

            Service service = ServiceBuilder
                .WithId(id)
                .Build();
            IEnumerable<Service> listService = [];

            Repository.UpdateAsync(service)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.UpdateServiceAsync(name, description, price, capacity, minimumReservationTime, maximumReservationTime, service, listService);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Service>()
                );
        }

        [TestMethod]
        public async Task UpdateServiceAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;
            int minimumReservationTime = 1;
            int maximumReservationTime = 2;

            Service service = ServiceBuilder
                .WithId(id)
                .Build();
            IEnumerable<Service> listService = [service];

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.UpdateServiceAsync(name, description, price, capacity, minimumReservationTime, maximumReservationTime, service, listService);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.NameServiceNotValid, ex.Message);
        }

        [TestMethod]
        public async Task DeleteServiceAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Service service = ServiceBuilder.Build();

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(service);

            Repository.DeleteAsync(service)
                .Returns(Task.CompletedTask);

            //Act
            await Service.DeleteServiceAsync(id);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
               .DeleteAsync(Arg.Any<Service>());
        }
    }
}
