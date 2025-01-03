using RESERVATION_SYSTEM.Domain.Entities.Base;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using Throw;

namespace RESERVATION_SYSTEM.Domain.Entities.customer
{
    public class Customer : DomainEntity
    {
        private string _name = default!;
        private string _email = default!;
        private string _phone = default!;

        public string Name {
            get => _name;
            set => _name = value.ThrowIfNull();
        }
        public string Email {
            get => _email;
            set => _email = value.ThrowIfNull();
        }
        public string Phone {
            get => _phone;
            set => _phone = value.ThrowIfNull();
        }
        public DateTime DateRegistration { get; set; }

        public virtual IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
