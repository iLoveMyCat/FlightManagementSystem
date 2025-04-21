namespace FlightManagement.AlertApi.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string? DeviceToken { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
