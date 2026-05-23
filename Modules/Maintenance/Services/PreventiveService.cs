using Microsoft.EntityFrameworkCore;
using TT.Backend.Infrastructure.Data;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public class PreventiveService : IPreventiveService
    {
        private readonly AppDbContext _db;

        public PreventiveService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PreventiveTaskEntity>> GetAllTasks() =>
            await _db.PreventiveTasks.ToListAsync();

        public async Task<List<PreventiveTaskEntity>> GetTasksByEquipment(Guid equipmentId) =>
            await _db.PreventiveTasks
                .Where(t => t.EquipmentId == equipmentId)
                .ToListAsync();

        public async Task<List<PreventiveTaskEntity>> GetOverdueTasks() =>
            await _db.PreventiveTasks
                .Where(t => t.NextDueDate < DateTime.UtcNow
                    && t.Status != MaintenanceTaskStatus.Completed)
                .ToListAsync();

        public async Task<PreventiveTaskEntity> CreateTask(CreatePreventiveTaskRequest request)
        {
            var task = new PreventiveTaskEntity
            {
                EquipmentId = request.EquipmentId,
                Title = request.Title,
                Description = request.Description,
                Frequency = request.Frequency,
                NextDueDate = request.NextDueDate,
                AssignedTo = request.AssignedTo
            };

            _db.PreventiveTasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<bool> CompleteTask(Guid id)
        {
            var task = await _db.PreventiveTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return false;

            task.LastCompletedAt = DateTime.UtcNow;

            task.NextDueDate = task.Frequency switch
            {
                PreventiveFrequency.Weekly     => DateTime.UtcNow.AddDays(7),
                PreventiveFrequency.Monthly    => DateTime.UtcNow.AddMonths(1),
                PreventiveFrequency.Quarterly  => DateTime.UtcNow.AddMonths(3),
                PreventiveFrequency.SemiAnnual => DateTime.UtcNow.AddMonths(6),
                PreventiveFrequency.Annual     => DateTime.UtcNow.AddYears(1),
                _                              => DateTime.UtcNow.AddMonths(1)
            };

            task.Status = MaintenanceTaskStatus.Scheduled;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}