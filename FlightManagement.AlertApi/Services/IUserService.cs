using FlightManagement.AlertApi.DTOs;

namespace FlightManagement.AlertApi.Services
{
    public interface IUserService
    {
        Task EnsureExistsAsync(Guid userId, string email);
        Task<UserDto> GetByIdAsync(Guid id);
        Task RegisterDeviceTokenAsync(Guid userId, string deviceToken);
    }
}
