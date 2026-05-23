// Copyright (c) 2026 T&T Technologia
// Licensed under the MIT License. See LICENSE in the project root.

namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class RunMaintenanceRequest
    {
        public string TaskName { get; set; }
        public string Description { get; set; }

        // <-- CONSTRUCTEUR Ã  ajouter ici
        public RunMaintenanceRequest()
        {
            TaskName = string.Empty;
            Description = string.Empty;
        }
    }
}
