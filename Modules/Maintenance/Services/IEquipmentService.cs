using TT.Backend.Modules.Maintenance.Entities;
using TT.Backend.Modules.Maintenance.DTOs;

namespace TT.Backend.Modules.Maintenance.Services
{
    public interface IEquipmentService
    {
        Task<List<EquipmentEntity>> GetAllEquipments();
        Task<EquipmentEntity?> GetEquipmentById(Guid id);
        Task<EquipmentEntity> CreateEquipment(CreateEquipmentRequest request);
        Task UpdateStatus(Guid id, EquipmentStatus status);
    }
}