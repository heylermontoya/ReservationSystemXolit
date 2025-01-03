using MediatR;
using RESERVATION_SYSTEM.Domain.Services.customer;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public class DeleteCustomerCommandHandler(
        CustomerService service
    ) : AsyncRequestHandler<DeleteCustomerCommand>
    {
        protected override async Task Handle(
            DeleteCustomerCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.DeleteCustomerAsync(
                command.Id
            );
        }
    }
}
