using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public interface ISparePartService
    {
        Task<List<SparePartEntity>> GetAllParts();
        Task<SparePartEntity?> GetPartById(Guid id);
        Task<List<SparePartEntity>> GetLowStockParts();
        Task<SparePartEntity> CreatePart(CreateSparePartRequest request);
        Task<bool> RestockPart(Guid id, int quantity);
        Task<SparePartUsageEntity> UsePart(UseSparePartRequest request);
        Task<List<SparePartUsageEntity>> GetUsageHistory(Guid sparePartId);
    }
}