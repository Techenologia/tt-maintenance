using Microsoft.EntityFrameworkCore;
using TT.Backend.Infrastructure.Data;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly AppDbContext _db;

        public EquipmentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<EquipmentEntity>> GetAllEquipments() =>
            await _db.Equipments.ToListAsync();

        public async Task<EquipmentEntity?> GetEquipmentById(Guid id) =>
            await _db.Equipments.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<EquipmentEntity> CreateEquipment(CreateEquipmentRequest request)
        {
            var equipment = new EquipmentEntity
            {
                Name         = request.Name,
                SerialNumber = request.SerialNumber,
                Brand        = request.Brand,
                Location     = request.Location,
                Category     = request.Category,
                PurchaseDate = request.PurchaseDate,
                Notes        = request.Notes
            };
            _db.Equipments.Add(equipment);
            await _db.SaveChangesAsync();
            return equipment;
        }

        public async Task UpdateStatus(Guid id, EquipmentStatus status)
        {
            var equipment = await _db.Equipments.FirstOrDefaultAsync(e => e.Id == id);
            if (equipment == null) return;
            equipment.Status = status;
            await _db.SaveChangesAsync();
        }
    }
}