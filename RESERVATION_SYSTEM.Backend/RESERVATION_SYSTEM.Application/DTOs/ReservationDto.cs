﻿namespace RESERVATION_SYSTEM.Application.DTOs
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid? ServiceID { get; set; }
        public string? ServiceName { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string State { get; set; } = string.Empty;
        public int NumberPeople { get; set; }
    }
}
