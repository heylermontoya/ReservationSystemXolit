namespace RESERVATION_SYSTEM.Application.DTOs
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Capacity { get; set; }
        public int MinimumReservationTime { get; set; }
        public int MaximumReservationTime { get; set; }
    }
}
