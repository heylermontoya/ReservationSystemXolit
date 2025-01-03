using MediatR;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public class DeleteServiceCommandHandler(
        ServiceService service
    ) : AsyncRequestHandler<DeleteServiceCommand>
    {
        protected override async Task Handle(
            DeleteServiceCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.DeleteServiceAsync(
                command.Id
            );
        }
    }
}
