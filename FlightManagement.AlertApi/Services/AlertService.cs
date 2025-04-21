using FlightManagement.AlertApi.DTOs;
using FlightManagement.AlertApi.Models;
using FlightManagement.AlertApi.Repositories;

namespace FlightManagement.AlertApi.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUserService _userService;

        public AlertService(IAlertRepository repository, IUserService userService)
        {
            _alertRepository = repository;
            _userService = userService;
        }

        public async Task CreateAsync(CreateAlertDto dto)
        {
            // create user if dont exist
            await _userService.EnsureExistsAsync(dto.UserId, dto.Email);

            var alert = new Alert
            {
                AlertId = Guid.NewGuid(),
                UserId = dto.UserId,
                Origin = dto.Origin,
                Destination = dto.Destination,
                FlightTime = dto.FlightTime,
                CreatedAt = DateTime.UtcNow
            };

            await _alertRepository.CreateAsync(alert);
        }

        public async Task<AlertDto?> GetByIdAsync(Guid id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);

            if (alert == null)
                return null;

            return new AlertDto
            {
                AlertId = alert.AlertId,
                UserId = alert.UserId,
                Origin = alert.Origin,
                Destination = alert.Destination,
                FlightTime = alert.FlightTime
            };
        }

        public async Task<List<AlertDto>> GetByUserIdAsync(Guid userId)
        {
            var alerts = await _alertRepository.GetByUserIdAsync(userId);

            return alerts.Select(a => new AlertDto
            {
                AlertId = a.AlertId,
                UserId = a.UserId,
                Origin = a.Origin,
                Destination = a.Destination,
                FlightTime = a.FlightTime
            }).ToList();
        }

        public async Task UpdateAsync(Guid id, UpdateAlertDto dto)
        {
            var updated = new Alert
            {
                AlertId = id,
                Origin = dto.Origin,
                Destination = dto.Destination,
                FlightTime = dto.FlightTime,
            };

            await _alertRepository.UpdateAsync(id, updated);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _alertRepository.DeleteAsync(id);
        }

        //private static AlertDto Map(Alert alert) => new AlertDto
        //{
        //    AlertId = alert.AlertId,
        //    UserId = alert.UserId,
        //    Origin = alert.Origin,
        //    Destination = alert.Destination,
        //    FlightTime = alert.FlightTime
        //};
    }
}
