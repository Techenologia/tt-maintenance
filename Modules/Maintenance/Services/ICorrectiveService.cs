using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public interface ICorrectiveService
    {
        Task<List<CorrectiveTaskEntity>> GetAllTasks();
        Task<List<CorrectiveTaskEntity>> GetTasksByEquipment(Guid equipmentId);
        Task<List<CorrectiveTaskEntity>> GetByStatus(CorrectiveStatus status);
        Task<CorrectiveTaskEntity> ReportIssue(CreateCorrectiveTaskRequest request);
        Task<bool> UpdateStatus(Guid id, CorrectiveStatus status);
        Task<bool> ResolveTask(Guid id, string solution);
    }
}