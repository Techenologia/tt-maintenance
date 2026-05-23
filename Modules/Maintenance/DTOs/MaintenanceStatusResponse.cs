// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

namespace TT.Backend.Modules.Maintenance.DTOs;

public class MaintenanceStatusResponse
{
    public required string Status      { get; set; }
    public DateTime        LastChecked { get; set; }
}

