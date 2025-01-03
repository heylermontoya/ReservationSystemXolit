using MediatR;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.service.Queries
{
    public record ObtainServiceQuery(IEnumerable<FieldFilter>? Filters) :
        IRequest<List<ServiceDto>>;
}
