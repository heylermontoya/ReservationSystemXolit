﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public record UpdateServiceCommand(
        [Required] Guid Id,
        [Required] string Name,
        [Required] string Description,
        [Required] float Price,
        [Required] int Capacity,
        [Required] int MinimumReservationTime,
        [Required] int MaximumReservationTime
    ) : IRequest;
}
