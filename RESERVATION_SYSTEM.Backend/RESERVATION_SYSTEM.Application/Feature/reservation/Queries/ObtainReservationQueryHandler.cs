using MediatR;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Queries
{
    public class ObtainReservationQueryHandler(
        IQueryWrapper queryWrapper
    ) : IRequestHandler<ObtainReservationQuery, List<ReservationDto>>
    {
        public async Task<List<ReservationDto>>
            Handle(ObtainReservationQuery query, CancellationToken cancellationToken)
        {
            List<FieldFilter> listFilters = query.Filters != null ? query.Filters.ToList() : [];

            IEnumerable<ReservationDto> reservations =
                await queryWrapper
                    .QueryAsync<ReservationDto>(
                        ItemsMessageConstants.GetReservation
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return reservations.ToList();
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
