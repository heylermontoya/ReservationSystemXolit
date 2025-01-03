using RESERVATION_SYSTEM.Domain.Entities.reservation;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class ReservationBuilder
    {
        private Guid _id;
        private Guid _customerId;
        private Guid? _serviceId;
        private DateTime _dateReservation;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _state;
        private int _numberPeople;

        public ReservationBuilder()
        {
            _id = Guid.NewGuid();
            _customerId = Guid.NewGuid();
            _serviceId = null;
            _dateReservation = DateTime.Now;
            _startDate = DateTime.Now;
            _endDate = DateTime.Now.AddDays(1);
            _state = "Pending";
            _numberPeople = 1;
        }

        public Reservation Build()
        {
            return new Reservation
            {
                Id = _id,
                CustomerID = _customerId,
                ServiceID = _serviceId,
                DateReservation = _dateReservation,
                StartDate = _startDate,
                EndDate = _endDate,
                State = _state,
                NumberPeople = _numberPeople
            };
        }

        public ReservationBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ReservationBuilder WithCustomerId(Guid customerId)
        {
            _customerId = customerId;
            return this;
        }

        public ReservationBuilder WithServiceId(Guid? serviceId)
        {
            _serviceId = serviceId;
            return this;
        }

        public ReservationBuilder WithDateReservation(DateTime dateReservation)
        {
            _dateReservation = dateReservation;
            return this;
        }

        public ReservationBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public ReservationBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public ReservationBuilder WithState(string state)
        {
            _state = state;
            return this;
        }

        public ReservationBuilder WithNumberPeople(int numberPeople)
        {
            _numberPeople = numberPeople;
            return this;
        }
    }
}
