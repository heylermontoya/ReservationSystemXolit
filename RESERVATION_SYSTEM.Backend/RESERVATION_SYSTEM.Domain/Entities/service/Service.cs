using RESERVATION_SYSTEM.Domain.Entities.Base;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using Throw;

namespace RESERVATION_SYSTEM.Domain.Entities.service
{
    public class Service : DomainEntity
    {
        private string _name = default!;
        private string _description = default!;

        public string Name {
            get => _name;
            set => _name = value.ThrowIfNull();
        }
        public string Description
        {
            get => _description;
            set => _description = value.ThrowIfNull();
        }
        public float Price { get; set; }
        public int Capacity { get; set; }
        public int MinimumReservationTime { get; set; }
        public int MaximumReservationTime { get; set; }

        public virtual IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
