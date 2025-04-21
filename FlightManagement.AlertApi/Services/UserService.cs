using FlightManagement.AlertApi.DTOs;
using FlightManagement.AlertApi.Models;
using FlightManagement.AlertApi.Repositories;

namespace FlightManagement.AlertApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepository;

        public UserService(IUserRepository repo)
        {
            _usersRepository = repo;
        }
        public async Task EnsureExistsAsync(Guid userId, string email)
        {
            var user = new User
            {
                UserId = userId,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            await _usersRepository.CreateIfNotExistsAsync(user);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            return user == null ? null : new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                DeviceToken = user.DeviceToken
            };
        }

        public async Task RegisterDeviceTokenAsync(Guid userId, string deviceToken)
        {
            await _usersRepository.UpdateDeviceTokenAsync(userId, deviceToken);
        }
    }
}
