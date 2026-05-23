using Microsoft.EntityFrameworkCore;
using TT.Backend.Infrastructure.Data;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public class CorrectiveService : ICorrectiveService
    {
        private readonly AppDbContext _db;

        public CorrectiveService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<CorrectiveTaskEntity>> GetAllTasks() =>
            await _db.CorrectiveTasks.ToListAsync();

        public async Task<List<CorrectiveTaskEntity>> GetTasksByEquipment(Guid equipmentId) =>
            await _db.CorrectiveTasks
                .Where(t => t.EquipmentId == equipmentId)
                .ToListAsync();

        public async Task<List<CorrectiveTaskEntity>> GetByStatus(CorrectiveStatus status) =>
            await _db.CorrectiveTasks
                .Where(t => t.Status == status)
                .ToListAsync();

        public async Task<CorrectiveTaskEntity> ReportIssue(CreateCorrectiveTaskRequest request)
        {
            var task = new CorrectiveTaskEntity
            {
                EquipmentId = request.EquipmentId,
                Title = request.Title,
                ProblemDescription = request.ProblemDescription,
                Severity = request.Severity,
                AssignedTo = request.AssignedTo
            };

            _db.CorrectiveTasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateStatus(Guid id, CorrectiveStatus status)
        {
            var task = await _db.CorrectiveTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return false;

            task.Status = status;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResolveTask(Guid id, string solution)
        {
            var task = await _db.CorrectiveTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return false;

            task.Solution = solution;
            task.Status = CorrectiveStatus.Resolved;
            task.ResolvedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}