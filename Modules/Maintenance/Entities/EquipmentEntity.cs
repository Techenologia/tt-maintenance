// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

namespace TT.Backend.Modules.Maintenance.Entities
{
    public enum EquipmentStatus
    {
        Operational,
        UnderMaintenance,
        OutOfService
    }

    public enum EquipmentCategory
    {
        Machine,
        Vehicle,
        Building,
        Electrical,
        HVAC,
        Other
    }

    public class EquipmentEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public EquipmentCategory Category { get; set; }
        public EquipmentStatus Status { get; set; } = EquipmentStatus.Operational;
        public DateTime PurchaseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }
}
