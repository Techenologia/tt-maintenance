// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

namespace TT.Backend.Modules.Maintenance.Entities
{
    public enum PreventiveFrequency
    {
        Weekly,
        Monthly,
        Quarterly,
        SemiAnnual,
        Annual
    }

    public enum MaintenanceTaskStatus  // âœ… Ã©tait TaskStatus
    {
        Scheduled,
        InProgress,
        Completed,
        Overdue
    }

    public class PreventiveTaskEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EquipmentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PreventiveFrequency Frequency { get; set; }
        public MaintenanceTaskStatus Status { get; set; } = MaintenanceTaskStatus.Scheduled;
        public DateTime NextDueDate { get; set; }
        public DateTime? LastCompletedAt { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
