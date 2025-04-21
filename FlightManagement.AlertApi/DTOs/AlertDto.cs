namespace FlightManagement.AlertApi.DTOs
{
    public class AlertDto
    {
        public Guid AlertId { get; set; }
        public Guid UserId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public DateTime? FlightTime { get; set; }
    }

}
