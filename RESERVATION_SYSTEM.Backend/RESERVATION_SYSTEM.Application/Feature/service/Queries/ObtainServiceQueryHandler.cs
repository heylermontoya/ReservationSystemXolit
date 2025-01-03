using MediatR;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.service.Queries
{
    public class ObtainServiceQueryHandler(
        IQueryWrapper queryWrapper
    ) : IRequestHandler<ObtainServiceQuery, List<ServiceDto>>
    {     
        public async Task<List<ServiceDto>>
            Handle(ObtainServiceQuery query, CancellationToken cancellationToken)
        {
            List<FieldFilter> listFilters = query.Filters != null ? query.Filters.ToList() : [];

            IEnumerable<ServiceDto> servicess =
                await queryWrapper
                    .QueryAsync<ServiceDto>(
                        ItemsMessageConstants.GetServices
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return servicess.ToList();
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
