using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public interface ITechnicianService
    {
        Task<List<TechnicianEntity>> GetAllTechnicians();
        Task<List<TechnicianEntity>> GetAvailableTechnicians();
        Task<TechnicianEntity?> GetTechnicianById(Guid id);
        Task<TechnicianEntity> CreateTechnician(CreateTechnicianRequest request);
        Task<bool> UpdateStatus(Guid id, TechnicianStatus status);
        Task<bool> AssignToPreventiveTask(Guid technicianId, Guid taskId);
        Task<bool> AssignToCorrectiveTask(Guid technicianId, Guid taskId);
    }
}