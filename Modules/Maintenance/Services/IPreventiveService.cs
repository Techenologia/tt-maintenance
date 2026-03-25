using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public interface IPreventiveService
    {
        Task<List<PreventiveTaskEntity>> GetAllTasks();
        Task<List<PreventiveTaskEntity>> GetTasksByEquipment(Guid equipmentId);
        Task<List<PreventiveTaskEntity>> GetOverdueTasks();
        Task<PreventiveTaskEntity> CreateTask(CreatePreventiveTaskRequest request);
        Task<bool> CompleteTask(Guid id);
    }
}