using RESERVATION_SYSTEM.Api.Tests.DataBuilder;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Infrastructure.Context;
using System;

namespace RESERVATION_SYSTEM.Api.Tests.ConfigureDataSeed
{
    public static class ReservationSeed
    {
        public static void ConfigureDataSeed(PersistenceContext context)
        {

            ReservationBuilder _builder = new();
            ReservationBuilder _builderByDelete = new();

            Reservation reservation = _builder
                    .WithId(new("ea133773-f0c3-4391-9a1f-bd547f5542aa"))
                    .WithCustomerId(new("526f3a54-45e2-4196-bd3f-c936ab336e91"))
                    .WithServiceId(new("f188d658-7d23-40cd-a7e4-0453209a0979"))
                    .WithDateReservation(DateTime.Now)
                    .WithStartDate(DateTime.Now)
                    .WithEndDate(DateTime.Now)
                    .WithState("Confirmed")
                    .WithNumberPeople(1)
                    .WithCustomer(CustomerBuilder.BuildWithReservation())
                    .WithService(ServiceBuilder.BuildWithReservation())
                    .Build();

            Reservation reservationByDelete = _builderByDelete
                    .WithId(new("92c2cfe1-610b-47e1-89ac-6f1d08da6831"))
                    .WithCustomerId(new("c94339b4-f469-47ba-a01d-f943698b4ec9"))
                    .WithServiceId(new("44d06498-ae6b-4d64-b864-6e01c7678086"))
                    .WithDateReservation(DateTime.Now)
                    .WithStartDate(DateTime.Now)
                    .WithEndDate(DateTime.Now)
                    .WithState("Confirmed")
                    .WithNumberPeople(1)
                    .WithCustomer(CustomerBuilder.BuildWithReservationByDelete())
                    .WithService(ServiceBuilder.BuildWithReservationByDelete())
                    .Build();

            context.Add(
                reservation
            );
            context.Add(
                reservationByDelete
            );
        }
    }
}
