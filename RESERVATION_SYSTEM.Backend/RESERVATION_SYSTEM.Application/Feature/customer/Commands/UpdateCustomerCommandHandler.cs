using MediatR;
using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.customer;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public class UpdateCustomerCommandHandler(
        CustomerService service,
        IGenericRepository<Customer> repository
    ) : AsyncRequestHandler<UpdateCustomerCommand>
    {
        protected override async Task Handle(
            UpdateCustomerCommand command,
            CancellationToken cancellationToken
        )
        {
            Customer customer = await repository.GetByIdAsync(command.Id);

            IEnumerable<Customer> listCustomer = await repository.GetAsync(
                customer => customer.Name == command.Name
            );

            await service.UpdateCustomerAsync(
                command.Name,
                command.Email,
                command.Phone,
                customer,
                listCustomer
            );
        }
    }
}
