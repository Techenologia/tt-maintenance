// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class CreatePreventiveTaskRequest
    {
        public Guid EquipmentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PreventiveFrequency Frequency { get; set; }
        public DateTime NextDueDate { get; set; }
        public string? AssignedTo { get; set; }
    }
}
