using Microsoft.EntityFrameworkCore;
using TT.Backend.Infrastructure.Data;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly AppDbContext _db;

        public TechnicianService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TechnicianEntity>> GetAllTechnicians() =>
            await _db.Technicians.ToListAsync();

        public async Task<List<TechnicianEntity>> GetAvailableTechnicians() =>
            await _db.Technicians
                .Where(t => t.Status == TechnicianStatus.Available)
                .ToListAsync();

        public async Task<TechnicianEntity?> GetTechnicianById(Guid id) =>
            await _db.Technicians.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<TechnicianEntity> CreateTechnician(CreateTechnicianRequest request)
        {
            var technician = new TechnicianEntity
            {
                FirstName = request.FirstName,
                LastName  = request.LastName,
                Email     = request.Email,
                Phone     = request.Phone,
                Specialty = request.Specialty,
                Notes     = request.Notes
            };

            _db.Technicians.Add(technician);
            await _db.SaveChangesAsync();
            return technician;
        }

        public async Task<bool> UpdateStatus(Guid id, TechnicianStatus status)
        {
            var technician = await _db.Technicians.FirstOrDefaultAsync(t => t.Id == id);
            if (technician == null) return false;

            technician.Status = status;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignToPreventiveTask(Guid technicianId, Guid taskId)
        {
            var technician = await _db.Technicians
                .FirstOrDefaultAsync(t => t.Id == technicianId);
            var task = await _db.PreventiveTasks
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (technician == null || task == null) return false;

            task.AssignedTo = $"{technician.FirstName} {technician.LastName}";
            technician.Status = TechnicianStatus.Busy;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignToCorrectiveTask(Guid technicianId, Guid taskId)
        {
            var technician = await _db.Technicians
                .FirstOrDefaultAsync(t => t.Id == technicianId);
            var task = await _db.CorrectiveTasks
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (technician == null || task == null) return false;

            task.AssignedTo = $"{technician.FirstName} {technician.LastName}";
            technician.Status = TechnicianStatus.Busy;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}