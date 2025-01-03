using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class ServiceBuilder
    {
        private Guid _id;
        private string _name;
        private string _description;
        private float _price;
        private int _capacity;
        private int _minimumReservationTime;
        private int _maximumReservationTime;
        private IEnumerable<Reservation> _reservations;

        public ServiceBuilder()
        {
            _id = Guid.NewGuid();
            _name = "Default Service Name";
            _description = "Default Description";
            _price = 0.0f;
            _capacity = 1;
            _minimumReservationTime = 1;
            _maximumReservationTime = 2;
            _reservations = [];
        }

        public Service Build()
        {
            return new Service
            {
                Id = _id,
                Name = _name,
                Description = _description,
                Price = _price,
                Capacity = _capacity,
                MinimumReservationTime = _minimumReservationTime,
                MaximumReservationTime = _maximumReservationTime,
                Reservations = _reservations
            };
        }

        public ServiceBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ServiceBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ServiceBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ServiceBuilder WithPrice(float price)
        {
            _price = price;
            return this;
        }

        public ServiceBuilder WithCapacity(int capacity)
        {
            _capacity = capacity;
            return this;
        }

        public ServiceBuilder WithMinimumReservationTime(int minimumReservationTime)
        {
            _minimumReservationTime = minimumReservationTime;
            return this;
        }

        public ServiceBuilder WithMaximumReservationTime(int maximumReservationTime)
        {
            _maximumReservationTime = maximumReservationTime;
            return this;
        }

        public ServiceBuilder WithReservations(IEnumerable<Reservation> reservations)
        {
            _reservations = reservations;
            return this;
        }
    }
}
