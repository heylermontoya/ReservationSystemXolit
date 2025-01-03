using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;

namespace RESERVATION_SYSTEM.Domain.Services.reservation
{
    [DomainService]
    public class ReservationService(
        IGenericRepository<Reservation> repository,
        HistoryReservationService historyReservationService
    )
    {
        public async Task CreateReservationAsync(
            Reservation reservation,
            IEnumerable<Reservation> reservationsByService,
            IEnumerable<Reservation> reservationsByUser,
            Service spaceSelected
        )
        {
            ValidationsCreateOrUpdateReservationsAsync(
                spaceSelected,
                reservation.StartDate,
                reservation.EndDate,
                reservation.NumberPeople
            );

            //Validacion de que el lugar este disponible en el rango de fechas
            ValidateSpaceAvailabilityInDateRangeAsync(
                reservationsByService,
                reservation.StartDate,
                reservation.EndDate
            );

            //Validacion de que un usuario  NO puede tener dos espacios reservados al mismo tiempo
            ValidateReservationUserSpaceAvailabilityInDateRangeAsync(
                reservationsByUser,
                reservation.StartDate,
                reservation.EndDate
            );

            reservation = await repository.AddAsync(reservation);

            await historyReservationService.CreateHistoryReservationAsync(reservation.Id, ReservationStatus.Confirmed);
        }

        public async Task UpdateReservationAsync(
            Reservation reservationUpdate,
            IEnumerable<Reservation> reservationsByService,
            IEnumerable<Reservation> reservationsByUser,
            Service spaceSelected
        )
        {
            ValidationsCreateOrUpdateReservationsAsync(
                spaceSelected,
                reservationUpdate.StartDate,
                reservationUpdate.EndDate,
                reservationUpdate.NumberPeople
            );

            //Validacion de que el lugar este disponible en el rango de fechas
            ValidateSpaceAvailabilityInDateRangeAsync(
                reservationsByService,
                reservationUpdate.StartDate,
                reservationUpdate.EndDate
            );

            //Validacion de que un usuario  NO puede tener dos espacios reservados al mismo tiempo
            ValidateReservationUserSpaceAvailabilityInDateRangeAsync(
                reservationsByUser,
                reservationUpdate.StartDate,
                reservationUpdate.EndDate
            );

            Reservation reservation = await ObtainReservationById(reservationUpdate.Id);

            reservation.ServiceID = reservationUpdate.ServiceID;
            reservation.DateReservation = reservationUpdate.DateReservation;
            reservation.StartDate = reservationUpdate.StartDate;
            reservation.EndDate = reservationUpdate.EndDate;
            reservation.State = reservationUpdate.State;
            reservation.NumberPeople = reservationUpdate.NumberPeople;

            reservation = await repository.UpdateAsync(reservation);

            await historyReservationService.CreateHistoryReservationAsync(reservation.Id, ReservationStatus.Modified);
        }

        public async Task CancelReservationAsync(Guid id)
        {
            Reservation reservation = await ObtainReservationById(id);
            reservation.State = ReservationStatus.Canceled.ToString();

            reservation = await repository.UpdateAsync(reservation);

            await historyReservationService
                .CreateHistoryReservationAsync(
                    reservation.Id,
                    ReservationStatus.Canceled
                );
        }

        public async Task LiberateServicesAsync(Guid idCustomer)
        {
            IEnumerable<Reservation> listReservation = await repository.GetAsync(
                reservation => reservation.CustomerID == idCustomer &&
                reservation.State != ReservationStatus.Canceled.ToString() &&
                reservation.ServiceID != null
            );

            foreach (var reservation in listReservation)
            {
                await CancelReservationAsync(reservation.Id);
            }
        }

        private async Task<Reservation> ObtainReservationById(Guid id)
        {
            Reservation reservation = await repository.GetByIdAsync(id);
            return reservation;
        }

        private static void ValidationsCreateOrUpdateReservationsAsync(
            Service spaceSelected,
            DateTime startDate,
            DateTime endDate,
            int numberPeople
        )
        {
            //Validacion de que la fecha de fin debe ser mayor a la fecha de inicio
            ValidateEndDateIsAfterStartDate(startDate, endDate);

            //Validacion capacidad del espacio
            ValidateSpaceCapacity(spaceSelected, numberPeople);

            //Validacion del limite de tiempo de la reserva del lugar
            ValidateReservationTimeLimit(startDate, endDate, spaceSelected);
        }

        private static void ValidateEndDateIsAfterStartDate(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new AppException(MessagesExceptions.ValidateEndDateIsAfterStartDateFailed);
            }
        }

        private static void ValidateSpaceCapacity(
            Service spaceSelected,
            int numberPeople
        )
        {
            if (spaceSelected.Capacity < numberPeople)
            {
                throw new AppException(MessagesExceptions.ValidateSpaceCapacityFailed);
            }
        }

        private static void ValidateReservationTimeLimit(
            DateTime startDate,
            DateTime endDate,
            Service spaceSelected
        )
        {
            TimeSpan difference = endDate - startDate;
            double hoursDifference = difference.TotalHours;

            if (
                hoursDifference < spaceSelected.MinimumReservationTime ||
                hoursDifference > spaceSelected.MaximumReservationTime
            )
            {
                throw new AppException(MessagesExceptions.ValidateReservationTimeLimitFailed);
            }
        }

        private static void ValidateSpaceAvailabilityInDateRangeAsync(
            IEnumerable<Reservation> reservations,
            DateTime startDate,
            DateTime endDate
        )
        {
            if (IsOverlapping(reservations, startDate, endDate))
            {
                throw new AppException(MessagesExceptions.ValidateSpaceAvailabilityInDateRangeByCreateFailed);
            }
        }

        private static void ValidateReservationUserSpaceAvailabilityInDateRangeAsync(
            IEnumerable<Reservation> reservationsByUser,
            DateTime startDate,
            DateTime endDate
        )
        {
            if (IsOverlapping(reservationsByUser, startDate, endDate))
            {
                throw new AppException(MessagesExceptions.ValidateReservationUserSpaceAvailabilityInDateRangeByCreate);
            }
        }

        private static bool IsOverlapping(IEnumerable<Reservation> reservations, DateTime startDate, DateTime endDate)
        {
            return reservations.Any(r =>
                (startDate >= r.StartDate && startDate < r.EndDate) || // StartDate solapa
                (endDate > r.StartDate && endDate <= r.EndDate) || // EndDate solapa
                (startDate <= r.StartDate && endDate >= r.EndDate) // Contiene completamente
            );
        }
    }
}
