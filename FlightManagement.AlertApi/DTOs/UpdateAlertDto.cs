namespace FlightManagement.AlertApi.DTOs
{
    public class UpdateAlertDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public DateTime? FlightTime { get; set; }
    }

}
