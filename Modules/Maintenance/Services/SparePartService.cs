using Microsoft.EntityFrameworkCore;
using TT.Backend.Infrastructure.Data;
using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public class SparePartService : ISparePartService
    {
        private readonly AppDbContext _db;

        public SparePartService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<SparePartEntity>> GetAllParts() =>
            await _db.SpareParts.ToListAsync();

        public async Task<SparePartEntity?> GetPartById(Guid id) =>
            await _db.SpareParts.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<SparePartEntity>> GetLowStockParts() =>
            await _db.SpareParts
                .Where(p => p.Quantity <= p.MinimumQuantity)
                .ToListAsync();

        public async Task<SparePartEntity> CreatePart(CreateSparePartRequest request)
        {
            var part = new SparePartEntity
            {
                Name            = request.Name,
                Reference       = request.Reference,
                Description     = request.Description,
                Quantity        = request.Quantity,
                MinimumQuantity = request.MinimumQuantity,
                UnitPrice       = request.UnitPrice,
                Supplier        = request.Supplier
            };

            _db.SpareParts.Add(part);
            await _db.SaveChangesAsync();
            return part;
        }

        public async Task<bool> RestockPart(Guid id, int quantity)
        {
            var part = await _db.SpareParts.FirstOrDefaultAsync(p => p.Id == id);
            if (part == null) return false;

            part.Quantity  += quantity;
            part.UpdatedAt  = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<SparePartUsageEntity> UsePart(UseSparePartRequest request)
        {
            var part = await _db.SpareParts
                .FirstOrDefaultAsync(p => p.Id == request.SparePartId);

            if (part == null)
                throw new KeyNotFoundException("Pièce introuvable");

            if (part.Quantity < request.QuantityUsed)
                throw new InvalidOperationException(
                    $"Stock insuffisant — disponible : {part.Quantity}, demandé : {request.QuantityUsed}");

            part.Quantity  -= request.QuantityUsed;
            part.UpdatedAt  = DateTime.UtcNow;

            var usage = new SparePartUsageEntity
            {
                SparePartId      = request.SparePartId,
                CorrectiveTaskId = request.CorrectiveTaskId,
                PreventiveTaskId = request.PreventiveTaskId,
                QuantityUsed     = request.QuantityUsed,
                Notes            = request.Notes
            };

            _db.SparePartUsages.Add(usage);
            await _db.SaveChangesAsync();
            return usage;
        }

        public async Task<List<SparePartUsageEntity>> GetUsageHistory(Guid sparePartId) =>
            await _db.SparePartUsages
                .Where(u => u.SparePartId == sparePartId)
                .OrderByDescending(u => u.UsedAt)
                .ToListAsync();
    }
}