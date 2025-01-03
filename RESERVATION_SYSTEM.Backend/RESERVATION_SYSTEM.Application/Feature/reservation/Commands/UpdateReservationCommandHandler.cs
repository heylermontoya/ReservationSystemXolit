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
    public class UpdateReservationCommandHandler(
        ReservationService service,
        ServiceService serviceService,
        IGenericRepository<Reservation> repository
    ) : AsyncRequestHandler<UpdateReservationCommand>
    {
        protected override async Task Handle(
            UpdateReservationCommand command,
            CancellationToken cancellationToken
        )
        {
            Reservation reservationUpdate = new()
            {
                Id = command.Id,
                CustomerID = command.CustomerID,
                ServiceID = command.ServiceId,
                DateReservation = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow),
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                State = ReservationStatus.Modified.ToString(),
                NumberPeople = command.NumberPeople
            };

            IEnumerable<Reservation> reservationsByService = await repository.GetAsync(
                reservation => reservation.ServiceID == reservationUpdate.ServiceID &&
                reservation.State != "Canceled" &&
                reservation.Id != reservationUpdate.Id
            );

            IEnumerable<Reservation> reservationsByUser = await repository.GetAsync(
                reservation => reservation.CustomerID == reservationUpdate.CustomerID &&
                reservation.State != "Canceled" &&
                reservation.Id != reservationUpdate.Id
            );

            Service spaceSelected = await serviceService.ObtainServiceById(reservationUpdate.ServiceID!.Value);

            await service.UpdateReservationAsync(
               reservationUpdate,
               reservationsByService,
               reservationsByUser,
               spaceSelected
            );
        }
    }
}
