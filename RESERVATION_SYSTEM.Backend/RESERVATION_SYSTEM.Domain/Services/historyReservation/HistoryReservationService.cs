using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;

namespace RESERVATION_SYSTEM.Domain.Services.historyReservation
{
    [DomainService]
    public class HistoryReservationService(
        IGenericRepository<HistoryReservation> repository
    )
    {
        public async Task<HistoryReservation> CreateHistoryReservationAsync(
            Guid reservationID,
            ReservationStatus descriptionChange
        )
        {
            HistoryReservation historyReservation = new()
            {
                ReservationID = reservationID,
                DateChange = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow),
                DescriptionChange = descriptionChange.ToString()
            };

            return await repository.AddAsync(historyReservation);
        }
    }
}
