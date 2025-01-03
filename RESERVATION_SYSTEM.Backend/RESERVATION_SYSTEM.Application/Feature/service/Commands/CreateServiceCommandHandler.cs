using MediatR;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public class CreateServiceCommandHandler(
        ServiceService service,
        IGenericRepository<Service> repository
    ) : AsyncRequestHandler<CreateServiceCommand>
    {
        protected override async Task Handle(
            CreateServiceCommand command,
            CancellationToken cancellationToken
        )
        {
            Service serviceEntity = new()
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Capacity = command.Capacity,
                MinimumReservationTime = command.MinimumReservationTime,
                MaximumReservationTime = command.MaximumReservationTime
            };

            IEnumerable<Service> listService = await repository.GetAsync(
                service => service.Name == command.Name
            );

            await service.CreateServiceAsync(
                serviceEntity,
                listService
            );
        }
    }
}
