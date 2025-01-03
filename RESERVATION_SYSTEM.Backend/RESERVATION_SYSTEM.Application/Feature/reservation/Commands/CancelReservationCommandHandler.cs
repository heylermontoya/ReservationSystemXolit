using MediatR;
using RESERVATION_SYSTEM.Domain.Services.reservation;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Commands
{
    public class CancelReservationCommandHandler(
        ReservationService service
    ) : AsyncRequestHandler<CancelReservationCommand>
    {
        protected override async Task Handle(
            CancelReservationCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.CancelReservationAsync(
               command.Id
            );
        }
    }
}
