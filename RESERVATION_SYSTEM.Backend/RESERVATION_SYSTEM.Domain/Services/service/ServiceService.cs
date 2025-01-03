using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Ports;

namespace RESERVATION_SYSTEM.Domain.Services.service
{
    [DomainService]
    public class ServiceService(
        IGenericRepository<Service> repository
    )
    {
        public async Task CreateServiceAsync(
            Service service,
            IEnumerable<Service> listService
        )
        {
            if (listService.Any())
            {
                throw new AppException(MessagesExceptions.NameServiceNotValid);
            }

            await repository.AddAsync(service);
        }

        public async Task UpdateServiceAsync(
            string name,
            string description,
            float price,
            int capacity,
            int minimumReservationTime,
            int maximumReservationTime,
            Service service,
            IEnumerable<Service> listService
        )
        {
            if (listService.Any() && name != service.Name)
            {
                throw new AppException(MessagesExceptions.NameServiceNotValid);
            }

            service.Name = name;
            service.Description = description;
            service.Price = price;
            service.Capacity = capacity;
            service.MinimumReservationTime = minimumReservationTime;
            service.MaximumReservationTime = maximumReservationTime;

            await repository.UpdateAsync(service);
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            Service service = await ObtainServiceById(id);

            await repository.DeleteAsync(service);
        }

        public async Task<Service> ObtainServiceById(Guid id)
        {
            Service service = await repository.GetByIdAsync(id);
            return service;
        }
    }
}
