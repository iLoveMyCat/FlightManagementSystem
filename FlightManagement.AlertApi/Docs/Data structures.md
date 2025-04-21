1. Alert Management API:
   public class Alert
   {
   public Guid AlertId { get; set; }
   public Guid UserId { get; set; }
   public string Origin { get; set; }
   public string Destination { get; set; }
   public DateTime? FlightTime { get; set; }
   public DateTime CreatedAt { get; set; }
   }

   public class CreateAlertDto
   {
   public string Origin { get; set; }
   public string Destination { get; set; }
   public DateTime? FlightTime { get; set; }
   }

   public class UpdateAlertDto
   {
   public string Origin { get; set; }
   public string Destination { get; set; }
   public DateTime? FlightTime { get; set; }
   }

   public class AlertDto
   {
   public Guid AlertId { get; set; }
   public Guid UserId { get; set; }
   public string Origin { get; set; }
   public string Destination { get; set; }
   public DateTime? FlightTime { get; set; }
   }

   public class User
   {
   public Guid UserId { get; set; }
   public string Email { get; set; }
   public string? DeviceToken { get; set; }
   public DateTime CreatedAt { get; set; }
   }

   public class CreateUserDto
   {
   public string Email { get; set; }
   }

   public class RegisterDeviceTokenDto
   {
   public string DeviceToken { get; set; }
   }

   public class UserDto
   {
   public Guid UserId { get; set; }
   public string Email { get; set; }
   public string? DeviceToken { get; set; }
   }

2. Data Collector Service:
   public class FlightPriceEventDto
   {
   public string Origin { get; set; }
   public string Destination { get; set; }
   public DateTime DepartureDate { get; set; }
   public decimal Price { get; set; }
   public string Currency { get; set; }
   public string Source { get; set; }
   public DateTime RetrievedAt { get; set; }
   }

3. Proccessing Service:
   public class PushMessageDto
   {
   public Guid AlertId { get; set; }
   public Guid UserId { get; set; }
   public string DeviceToken { get; set; }
   public string Title { get; set; }
   public string Body { get; set; }
   public DateTime MatchedAt { get; set; }
   }

4. Notification Service:
   // No additional data structures needed here, The service consumes PushMessageDto from the push queue and
   // uses it directly to send push notifications via Firebase or APNs.
