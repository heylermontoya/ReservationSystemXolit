using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using System;
using System.Collections.Generic;

namespace RESERVATION_SYSTEM.Api.Tests.DataBuilder
{
    public class CustomerBuilder
    {
        private Guid _id;
        private string _name;
        private string _email;
        private string _phone;
        private DateTime _dateRegistration;
        private IEnumerable<Reservation> _reservations;

        public CustomerBuilder()
        {
            _id = Guid.NewGuid();
            _name = "Default Name";
            _email = "default@example.com";
            _phone = "000-000-0000";
            _dateRegistration = DateTime.Now;
            _reservations = [];
        }

        public Customer Build()
        {
            return new Customer
            {
                Id = _id,
                Name = _name,
                Email = _email,
                Phone = _phone,
                DateRegistration = _dateRegistration,
                Reservations = _reservations
            };
        }

        public CustomerBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CustomerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CustomerBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CustomerBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public CustomerBuilder WithDateRegistration(DateTime dateRegistration)
        {
            _dateRegistration = dateRegistration;
            return this;
        }

        public CustomerBuilder WithReservations(IEnumerable<Reservation> reservations)
        {
            _reservations = reservations;
            return this;
        }
        
        public static Customer BuildWithReservation()
        {
            return new Customer
            {
                Id = new("526f3a54-45e2-4196-bd3f-c936ab336e91"),
                Name = "John Doe",
                Email = "johndoe@example.com",
                Phone = "123-456-7890",
                DateRegistration = DateTime.Now
            };
        }
        
        public static Customer BuildWithReservationByDelete()
        {
            return new Customer
            {
                Id = new("c94339b4-f469-47ba-a01d-f943698b4ec9"),
                Name = "John Doe Del",
                Email = "johndoeDel@example.com",
                Phone = "123-456-7890",
                DateRegistration = DateTime.Now
            };
        }
    }
}
