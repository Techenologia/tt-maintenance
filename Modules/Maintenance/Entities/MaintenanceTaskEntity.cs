// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

namespace TT.Backend.Modules.Maintenance.Entities
{
    public class MaintenanceTaskEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
