using MediatR;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.reservation;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Commands
{
    public class CreateReservationCommandHandler(
        ReservationService service,
        ServiceService serviceService,
        IGenericRepository<Reservation> repository
    ) : AsyncRequestHandler<CreateReservationCommand>
    {
        protected override async Task Handle(
            CreateReservationCommand command,
            CancellationToken cancellationToken
        )
        {
            Reservation reservation = new()
            {
                CustomerID = command.CustomerID,
                ServiceID = command.ServiceID,
                DateReservation = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow),
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                State = ReservationStatus.Confirmed.ToString(),
                NumberPeople = command.NumberPeople
            };

            IEnumerable<Reservation> reservationsByService = await repository.GetAsync(
                reservations => reservations.ServiceID == reservation.ServiceID!.Value &&
                reservations.State != "Canceled"
            );

            IEnumerable<Reservation> reservationsByUser = await repository.GetAsync(
                reservations => reservations.CustomerID == reservation.CustomerID &&
                reservations.State != "Canceled"
            );

            Service spaceSelected = await serviceService.ObtainServiceById(reservation.ServiceID!.Value);

            await service.CreateReservationAsync(
               reservation,
               reservationsByService,
               reservationsByUser,
               spaceSelected
            );
        }
    }
}
