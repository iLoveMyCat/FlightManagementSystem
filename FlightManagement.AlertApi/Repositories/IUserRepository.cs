using FlightManagement.AlertApi.Models;

namespace FlightManagement.AlertApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task CreateIfNotExistsAsync(User user);
        Task UpdateDeviceTokenAsync(Guid userId, string deviceToken);
    }
}
