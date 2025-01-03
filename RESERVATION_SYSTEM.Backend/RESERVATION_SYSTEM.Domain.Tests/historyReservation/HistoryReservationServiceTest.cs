using NSubstitute;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;

namespace RESERVATION_SYSTEM.Domain.Tests.historyReservation
{
    [TestClass]
    public class HistoryReservationServiceTest
    {
        private HistoryReservationService Service { get; set; } = default!;
        private IGenericRepository<HistoryReservation> Repository { get; set; } = default!;
        private HistoryReservationBuilder HistoryReservationBuilder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            Repository = Substitute.For<IGenericRepository<HistoryReservation>>();

            Service = new(
                Repository
            );

            HistoryReservationBuilder = new();
        }

        [TestMethod]
        public async Task CreateHistoryReservationAsync_Ok()
        {
            //Arrange
            Guid reservationID = Guid.NewGuid();
            HistoryReservation historyReservation = HistoryReservationBuilder
                .WithId(Guid.NewGuid())
                .WithReservationId(reservationID)
                .Build();

            Repository.AddAsync(historyReservation)
                .ReturnsForAnyArgs(historyReservation);

            //Act
            HistoryReservation result = await Service.CreateHistoryReservationAsync(reservationID, ReservationStatus.Confirmed);

            //Assert
            Assert.AreEqual(historyReservation.Id, result.Id);
            Assert.AreEqual(historyReservation.DescriptionChange, result.DescriptionChange);
            Assert.AreEqual(historyReservation.DateChange, result.DateChange);
            Assert.AreEqual(historyReservation.ReservationID, result.ReservationID);
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<HistoryReservation>()
                );
        }
    }
}
