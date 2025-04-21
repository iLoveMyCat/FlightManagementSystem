using FlightManagement.AlertApi.Models;

namespace FlightManagement.AlertApi.Repositories
{
    public interface IAlertRepository
    {
        Task CreateAsync(Alert alert);
        Task<Alert?> GetByIdAsync(Guid id);
        Task<List<Alert>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Guid id, Alert updated);
        Task DeleteAsync(Guid id);
    }
}
