using FlightManagement.AlertApi.DTOs;

namespace FlightManagement.AlertApi.Services
{
    public interface IAlertService
    {
        Task CreateAsync(CreateAlertDto dto);
        Task<AlertDto?> GetByIdAsync(Guid id);
        Task<List<AlertDto>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Guid id, UpdateAlertDto dto);
        Task DeleteAsync(Guid id);
    }
}
