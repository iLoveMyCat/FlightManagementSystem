namespace FlightManagement.AlertApi.DTOs
{
    public class CreateAlertDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public DateTime? FlightTime { get; set; }
    }

}
