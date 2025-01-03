using MediatR;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.historyReservation.Queries
{
    public class ObtainHistoryReservationQueryHandler(
        IQueryWrapper queryWrapper
    ) : IRequestHandler<ObtainHistoryReservationQuery, List<HistoryReservationDto>>
    {
        public async Task<List<HistoryReservationDto>> Handle(ObtainHistoryReservationQuery query, CancellationToken cancellationToken)
        {
            List<FieldFilter> listFilters = query.Filters != null ? query.Filters.ToList() : [];

            IEnumerable<HistoryReservationDto> historyReservations =
                await queryWrapper
                    .QueryAsync<HistoryReservationDto>(
                        ItemsMessageConstants.GetHistoryReservation
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return historyReservations.ToList();            
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
