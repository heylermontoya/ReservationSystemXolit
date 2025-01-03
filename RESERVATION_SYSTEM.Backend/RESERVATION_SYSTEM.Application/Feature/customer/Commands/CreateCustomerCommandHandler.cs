using MediatR;
using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.customer;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public class CreateCustomerCommandHandler(
        CustomerService service,
        IGenericRepository<Customer> repository
    ) : AsyncRequestHandler<CreateCustomerCommand>
    {
        protected override async Task Handle(
            CreateCustomerCommand command,
            CancellationToken cancellationToken
        )
        {
            Customer customer = new()
            {
                Name = command.Name,
                Email = command.Email,
                Phone = command.Phone,
                DateRegistration = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow)
            };

            IEnumerable<Customer> listCustomer = await repository.GetAsync(
                customer => customer.Name == command.Name
            );

            await service.CreateCustomerAsync(
                customer,
                listCustomer
            );
        }
    }
}
