using NSubstitute;
using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.customer;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using RESERVATION_SYSTEM.Domain.Services.reservation;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;
using System.Linq.Expressions;

namespace RESERVATION_SYSTEM.Domain.Tests.customer
{
    [TestClass]
    public class CustomersServiceTest
    {
        private CustomerService Service { get; set; } = default!;
        private ReservationService ReservationService { get; set; } = default!;
        private HistoryReservationService HistoryReservationService { get; set; } = default!;
        private IGenericRepository<Customer> Repository { get; set; } = default!;
        private IGenericRepository<Reservation> ReservationRepository { get; set; } = default!;
        private IGenericRepository<HistoryReservation> HistoryReservationRepository { get; set; } = default!;
        private CustomerBuilder CustomerBuilder { get; set; } = default!;
        private ReservationBuilder ReservationBuilder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            Repository = Substitute.For<IGenericRepository<Customer>>();
            ReservationRepository = Substitute.For<IGenericRepository<Reservation>>();
            HistoryReservationRepository = Substitute.For<IGenericRepository<HistoryReservation>>();

            HistoryReservationService = new(
                HistoryReservationRepository
            );

            ReservationService = new(
                ReservationRepository,
                HistoryReservationService
            );

            Service = new(
                Repository,
                ReservationService
            );

            CustomerBuilder = new();
            ReservationBuilder = new();
        }

        [TestMethod]
        public async Task CreateCustomerAsync_Ok()
        {
            //Arrange
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder
                .WithName(name)
                .WithEmail(email)
                .WithPhone(phone)
                .Build();
            List<Customer> listCustomer = [];

            Repository.AddAsync(customer)
                .ReturnsForAnyArgs(customer);

            //Act
            await Service.CreateCustomerAsync(customer, listCustomer);

            //Assert                        
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<Customer>()
                );
        }

        [TestMethod]
        public async Task CreateCustomerAsync_Failed()
        {
            //Arrange
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder
                .WithName(name)
                .WithEmail(email)
                .WithPhone(phone)
                .Build();
            List<Customer> listCustomer = [customer];

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateCustomerAsync(customer, listCustomer);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.NameCustomerNotValid, ex.Message);
        }

        [TestMethod]
        public async Task UpdateCustomerAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder
                .WithId(id)
                .Build();
            List<Customer> listCustomer = [];

            Repository.UpdateAsync(customer)
                .ReturnsForAnyArgs(customer);

            //Act
            await Service.UpdateCustomerAsync(name, email, phone, customer, listCustomer);

            //Assert                        
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Customer>()
                );
        }

        [TestMethod]
        public async Task UpdateCustomerAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder
                .WithId(id)
                .Build();
            List<Customer> listCustomer = [customer];


            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.UpdateCustomerAsync(name, email, phone, customer, listCustomer);
            });

            //Assert            
            Assert.AreEqual(MessagesExceptions.NameCustomerNotValid, ex.Message);
        }

        [TestMethod]
        public async Task DeleteCustomerAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Customer customer = CustomerBuilder.Build();

            Guid idReservation = Guid.NewGuid();

            Reservation reservation = ReservationBuilder
                .WithId(idReservation)
                .Build();

            List<Reservation> listReservation = [
                reservation
            ];

            Repository.GetByIdAsync(id)
               .ReturnsForAnyArgs(customer);

            Repository.DeleteAsync(customer)
                .Returns(Task.CompletedTask);

            ReservationRepository.GetAsync(
                reservation => reservation.CustomerID == customer.Id &&
                reservation.State != ReservationStatus.Canceled.ToString() &&
                reservation.ServiceID != null
            ).ReturnsForAnyArgs(listReservation);

            ReservationRepository.GetByIdAsync(idReservation)
                .ReturnsForAnyArgs(reservation);

            ReservationRepository.UpdateAsync(reservation)
                .ReturnsForAnyArgs(reservation);
            //Act
            await Service.DeleteCustomerAsync(id);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
               .DeleteAsync(Arg.Any<Customer>());
            await ReservationRepository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Reservation, bool>>>()
                );
            await ReservationRepository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(Arg.Any<Guid>());
            await ReservationRepository.ReceivedWithAnyArgs(1)
                .UpdateAsync(Arg.Any<Reservation>());
        }
    }
}
