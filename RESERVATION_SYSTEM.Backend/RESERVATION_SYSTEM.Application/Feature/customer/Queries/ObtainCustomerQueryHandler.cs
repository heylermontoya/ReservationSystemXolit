using MediatR;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Queries
{
    public class ObtainCustomerQueryHandler(
      IQueryWrapper queryWrapper
    ) : IRequestHandler<ObtainCustomerQuery, List<CustomerDto>>
    {
        public async Task<List<CustomerDto>> Handle(ObtainCustomerQuery query, CancellationToken cancellationToken)
        {
            List<FieldFilter> listFilters = query.Filters != null ? query.Filters.ToList() : [];

            IEnumerable<CustomerDto> Customers =
                await queryWrapper
                    .QueryAsync<CustomerDto>(
                        ItemsMessageConstants.GetCustomers
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return Customers.ToList();
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
