using NSubstitute;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using RESERVATION_SYSTEM.Domain.Services.reservation;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;
using System.Linq.Expressions;

namespace RESERVATION_SYSTEM.Domain.Tests.reservation
{
    [TestClass]
    public class ReservationTest
    {
        private ReservationService Service { get; set; } = default!;
        private HistoryReservationService HistoryReservationService { get; set; } = default!;
        private IGenericRepository<Reservation> Repository { get; set; } = default!;
        private ReservationBuilder ReservationBuilder { get; set; } = default!;
        private HistoryReservationBuilder HistoryReservationBuilder { get; set; } = default!;
        private ServiceBuilder ServiceBuilder { get; set; } = default!;
        private IGenericRepository<HistoryReservation> HistoryReservationRepository { get; set; } = default!;
        private IGenericRepository<Service> ServiceRepository { get; set; } = default!;


        [TestInitialize]
        public void Initialize()
        {
            Repository = Substitute.For<IGenericRepository<Reservation>>();
            HistoryReservationRepository = Substitute.For<IGenericRepository<HistoryReservation>>();
            ServiceRepository = Substitute.For<IGenericRepository<Service>>();

            HistoryReservationService = new(
                HistoryReservationRepository
            );

            Service = new(
                Repository,
                HistoryReservationService
            );

            ReservationBuilder = new();
            ServiceBuilder = new();
            HistoryReservationBuilder = new();
        }

        [TestMethod]
        public async Task CreateReservationAsync_Ok()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            int numberPeople = 1;

            Service service = ServiceBuilder.Build();

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Reservation reservationByService = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(1))
                .WithEndDate(DateTime.Now.AddHours(2))
                .Build();

            Reservation reservationByUser = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(5))
                .WithEndDate(DateTime.Now.AddHours(6))
                .Build();

            HistoryReservation historyReservation = HistoryReservationBuilder
                .WithReservationId(reservation.Id)
                .Build();

            Repository.AddAsync(reservation)
                .ReturnsForAnyArgs(reservation);

            HistoryReservationRepository.AddAsync(historyReservation)
                .ReturnsForAnyArgs(historyReservation);

            //Act
            await Service.CreateReservationAsync(reservation, [reservationByService], [reservationByUser], service);

            //Assert                        
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<Reservation>()
                );
            await HistoryReservationRepository.ReceivedWithAnyArgs(1)
                .AddAsync(Arg.Any<HistoryReservation>());
        }

        [TestMethod]
        public async Task CreateReservationAsync_ValidateEndDateIsAfterStartDate_failed()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            DateTime startDate = DateTime.Now.AddHours(1);
            DateTime endDate = DateTime.Now;
            int numberPeople = 1;

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithStartDate(startDate)
                .WithEndDate(endDate)
                .WithNumberPeople(numberPeople)
                .Build();

            Service service = ServiceBuilder.Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateReservationAsync(reservation, [], [], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateEndDateIsAfterStartDateFailed, ex.Message);
        }

        [TestMethod]
        public async Task CreateReservationAsync_ValidateSpaceCapacity_failed()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddHours(1);
            int numberPeople = 2;

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithStartDate(startDate)
                .WithEndDate(endDate)
                .WithNumberPeople(numberPeople)
                .Build();

            Service service = ServiceBuilder.Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateReservationAsync(reservation, [], [], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateSpaceCapacityFailed, ex.Message);
        }

        [TestMethod]
        public async Task CreateReservationAsync_ValidateReservationTimeLimit_failed()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            int numberPeople = 1;

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Reservation reservationByService = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(1))
                .WithEndDate(DateTime.Now.AddHours(2))
                .Build();

            Reservation reservationByUser = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(5))
                .WithEndDate(DateTime.Now.AddHours(6))
                .Build();

            Service service = ServiceBuilder
                .WithMinimumReservationTime(2)
                .WithMaximumReservationTime(3)
                .Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateReservationAsync(reservation, [reservationByService], [reservationByUser], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateReservationTimeLimitFailed, ex.Message);
        }

        [TestMethod]
        public async Task CreateReservationAsync_ValidateSpaceAvailabilityInDateRangeByCreate_failed()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            int numberPeople = 1;

            Service service = ServiceBuilder
                .Build();

            Reservation reservation = ReservationBuilder
                            .WithCustomerId(customerID)
                            .WithServiceId(serviceID)
                            .WithNumberPeople(numberPeople)
                            .WithStartDate(DateTime.Now.AddHours(3))
                            .WithEndDate(DateTime.Now.AddHours(4))
                            .Build();

            Reservation reservationByService = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Reservation reservationByUser = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(5))
                .WithEndDate(DateTime.Now.AddHours(6))
                .Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateReservationAsync(reservation, [reservationByService], [reservationByUser], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateSpaceAvailabilityInDateRangeByCreateFailed, ex.Message);
        }

        [TestMethod]
        public async Task CreateReservationAsync_ValidateReservationUserSpaceAvailabilityInDateRangeByCreate_failed()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            int numberPeople = 1;

            Service service = ServiceBuilder
                .Build();

            Reservation reservation = ReservationBuilder
                            .WithCustomerId(customerID)
                            .WithServiceId(serviceID)
                            .WithNumberPeople(numberPeople)
                            .WithStartDate(DateTime.Now.AddHours(3))
                            .WithEndDate(DateTime.Now.AddHours(4))
                            .Build();

            Reservation reservationByService = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(5))
                .WithEndDate(DateTime.Now.AddHours(6))
                .Build();

            Reservation reservationByUser = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateReservationAsync(reservation, [reservationByService], [reservationByUser], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateReservationUserSpaceAvailabilityInDateRangeByCreate, ex.Message);
        }

        [TestMethod]
        public async Task UpdateReservationAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            Guid customerID = Guid.NewGuid();
            int numberPeople = 1;

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Reservation reservationByService = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(1))
                .WithEndDate(DateTime.Now.AddHours(2))
                .Build();

            Reservation reservationByUser = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(5))
                .WithEndDate(DateTime.Now.AddHours(6))
                .Build();

            Service service = ServiceBuilder
                .Build();

            HistoryReservation historyReservation = HistoryReservationBuilder
                .WithReservationId(reservation.Id)
                .Build();

            Repository.GetByIdAsync(id)
               .ReturnsForAnyArgs(reservation);

            Repository.UpdateAsync(reservation)
               .ReturnsForAnyArgs(reservation);

            HistoryReservationRepository.AddAsync(historyReservation)
                .ReturnsForAnyArgs(historyReservation);

            //Act
            await Service.UpdateReservationAsync(reservation, [reservationByService], [reservationByUser], service);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Reservation>()
                );
            await HistoryReservationRepository.ReceivedWithAnyArgs(1)
                .AddAsync(Arg.Any<HistoryReservation>());
        }

        [TestMethod]
        public async Task UpdateReservationAsync_ValidateSpaceAvailabilityInDateRange_Failed()
        {
            //Arrange            
            Guid serviceID = Guid.NewGuid();
            Guid customerID = Guid.NewGuid();
            int numberPeople = 1;

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Reservation reservationByService = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Reservation reservationByUser = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(5))
                .WithEndDate(DateTime.Now.AddHours(6))
                .Build();

            Service service = ServiceBuilder
                .Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.UpdateReservationAsync(reservation, [reservationByService], [reservationByUser], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateSpaceAvailabilityInDateRangeByCreateFailed, ex.Message);
        }

        [TestMethod]
        public async Task UpdateReservationAsync_ValidateReservationUserSpaceAvailabilityInDateRange_Failed()
        {
            //Arrange            
            Guid serviceID = Guid.NewGuid();
            Guid customerID = Guid.NewGuid();
            int numberPeople = 1;

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Reservation reservationByService = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(5))
                .WithEndDate(DateTime.Now.AddHours(6))
                .Build();

            Reservation reservationByUser = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithNumberPeople(numberPeople)
                .WithStartDate(DateTime.Now.AddHours(3))
                .WithEndDate(DateTime.Now.AddHours(4))
                .Build();

            Service service = ServiceBuilder
                .Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.UpdateReservationAsync(reservation, [reservationByService], [reservationByUser], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateReservationUserSpaceAvailabilityInDateRangeByCreate, ex.Message);
        }

        [TestMethod]
        public async Task UpdateReservationAsync_ValidateEndDateIsAfterStartDate_failed()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            DateTime startDate = DateTime.Now.AddHours(1);
            DateTime endDate = DateTime.Now;
            int numberPeople = 1;

            Reservation reservation = ReservationBuilder
                .WithCustomerId(customerID)
                .WithServiceId(serviceID)
                .WithStartDate(startDate)
                .WithEndDate(endDate)
                .WithNumberPeople(numberPeople)
                .Build();

            Service service = ServiceBuilder.Build();

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.UpdateReservationAsync(reservation, [], [], service);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.ValidateEndDateIsAfterStartDateFailed, ex.Message);
        }

        [TestMethod]
        public async Task CancelReservationAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Reservation reservation = ReservationBuilder.Build();

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(reservation);

            Repository.UpdateAsync(reservation)
               .ReturnsForAnyArgs(reservation);

            Guid idService = Guid.NewGuid();

            Service service = ServiceBuilder.Build();

            ServiceRepository.GetByIdAsync(idService)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.CancelReservationAsync(id);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Reservation>()
                );
        }

        [TestMethod]
        public async Task LiberateServicesAsync_Ok()
        {
            //Arrange
            Guid idCustomer = Guid.NewGuid();
            Guid idReservation = Guid.NewGuid();

            Reservation reservation = ReservationBuilder
                .WithId(idReservation)
                .Build();

            List<Reservation> listReservation = [
                reservation
            ];

            HistoryReservation historyReservation = HistoryReservationBuilder
                .WithReservationId(idReservation)
                .Build();

            Repository.GetAsync(
                reservation => reservation.CustomerID == idCustomer &&
                reservation.State != ReservationStatus.Canceled.ToString() &&
                reservation.ServiceID != null
            ).ReturnsForAnyArgs(listReservation);

            Repository.GetByIdAsync(idReservation)
                .ReturnsForAnyArgs(reservation);

            Repository.UpdateAsync(reservation)
                .ReturnsForAnyArgs(reservation);

            HistoryReservationRepository.AddAsync(historyReservation)
                .ReturnsForAnyArgs(historyReservation);

            //Act
            await Service.LiberateServicesAsync(idCustomer);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Reservation, bool>>>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(idReservation);
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(reservation);
            await HistoryReservationRepository.ReceivedWithAnyArgs(1)
                .AddAsync(historyReservation);
        }
    }
}
