// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class UseSparePartRequest
    {
        public Guid SparePartId { get; set; }
        public Guid? CorrectiveTaskId { get; set; }
        public Guid? PreventiveTaskId { get; set; }
        public int QuantityUsed { get; set; }
        public string? Notes { get; set; }
    }
}
