using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.Services
{
    public interface IMaintenanceService
    {
        Task<IEnumerable<MaintenanceTaskEntity>> GetAllTasks();
        Task<MaintenanceTaskEntity?> GetTaskById(Guid id);
        Task CreateTask(MaintenanceTaskEntity task);
    }
}