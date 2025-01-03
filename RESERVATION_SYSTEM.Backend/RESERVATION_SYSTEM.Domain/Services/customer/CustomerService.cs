using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.reservation;

namespace RESERVATION_SYSTEM.Domain.Services.customer
{
    [DomainService]
    public class CustomerService(
        IGenericRepository<Customer> repository,
        ReservationService reservationService
    )
    {
        public async Task CreateCustomerAsync(Customer customer, IEnumerable<Customer> listCustomer)
        {
            if (listCustomer.Any())
            {
                throw new AppException(MessagesExceptions.NameCustomerNotValid);
            }

            await repository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(
            string name,
            string email,
            string phone,
            Customer customer,
            IEnumerable<Customer> listCustomer
        )
        {
            if (listCustomer.Any() && name != customer.Name)
            {
                throw new AppException(MessagesExceptions.NameCustomerNotValid);
            }

            customer.Name = name;
            customer.Email = email;
            customer.Phone = phone;

            await repository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            Customer customer = await repository.GetByIdAsync(id);

            await reservationService.LiberateServicesAsync(id);

            await repository.DeleteAsync(customer);
        }
    }
}
