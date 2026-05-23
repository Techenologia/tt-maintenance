using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class CreateEquipmentRequest
    {
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public EquipmentCategory Category { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? Notes { get; set; }
    }
}