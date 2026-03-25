using Microsoft.EntityFrameworkCore;
using TT.Backend.Infrastructure.Data;
using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly AppDbContext _db;

        public MaintenanceService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MaintenanceTaskEntity>> GetAllTasks() =>
            await _db.MaintenanceTasks.ToListAsync();

        public async Task<MaintenanceTaskEntity?> GetTaskById(Guid id) =>
            await _db.MaintenanceTasks.FirstOrDefaultAsync(t => t.Id == id);

        public async Task CreateTask(MaintenanceTaskEntity task)
        {
            _db.MaintenanceTasks.Add(task);
            await _db.SaveChangesAsync();
        }
    }
}