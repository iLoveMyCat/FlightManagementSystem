namespace FlightManagement.AlertApi.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string? DeviceToken { get; set; }
    }
}
