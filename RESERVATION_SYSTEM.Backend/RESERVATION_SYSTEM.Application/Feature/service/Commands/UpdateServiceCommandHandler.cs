using MediatR;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public class UpdateServiceCommandHandler(
        ServiceService service,
        IGenericRepository<Service> repository
    ) : AsyncRequestHandler<UpdateServiceCommand>
    {
        protected override async Task Handle(
            UpdateServiceCommand command,
            CancellationToken cancellationToken
        )
        {
            Service serviceEntity = await repository.GetByIdAsync(command.Id);

            IEnumerable<Service> listService = await repository.GetAsync(
                service => service.Name == command.Name
            );

            await service.UpdateServiceAsync(
                command.Name,
                command.Description,
                command.Price,
                command.Capacity,
                command.MinimumReservationTime,
                command.MaximumReservationTime,
                serviceEntity,
                listService
            );
        }
    }
}
