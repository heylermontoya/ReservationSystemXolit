using RESERVATION_SYSTEM.Domain.Entities.Base;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using Throw;

namespace RESERVATION_SYSTEM.Domain.Entities.historyReservation
{
    public class HistoryReservation : DomainEntity
    {
        private string _descriptionChange = default!;

        public Guid ReservationID { get; set; }
        public DateTime DateChange { get; set; }
        public string DescriptionChange
        {
            get => _descriptionChange;
            set => _descriptionChange = value.ThrowIfNull();
        }

        public virtual Reservation Reservation { get; set; } = default!;
    }
}
