using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;
using System;
using System.Collections.Generic;

namespace RESERVATION_SYSTEM.Api.Tests.DataBuilder
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

        public static Service BuildWithReservation()
        {
            return new Service
            {
                Id = new("f188d658-7d23-40cd-a7e4-0453209a0979"),
                Name = "juan",
                Description = "Description",
                Price = 1,
                Capacity = 2,
                MinimumReservationTime = 1,
                MaximumReservationTime = 2
            };
        }

        public static Service BuildWithReservationByDelete()
        {
            return new Service
            {
                Id = new("44d06498-ae6b-4d64-b864-6e01c7678086"),
                Name = "juan del",
                Description = "Description del",
                Price = 1,
                Capacity = 2,
                MinimumReservationTime = 1,
                MaximumReservationTime = 2
            };
        }
    }
}
