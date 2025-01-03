using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Commands
{
    public record UpdateReservationCommand(
        [Required] Guid Id,
        [Required] Guid CustomerID,
        [Required] Guid ServiceId,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required] int NumberPeople
    ) : IRequest;
}
