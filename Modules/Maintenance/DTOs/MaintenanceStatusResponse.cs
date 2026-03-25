using System;

namespace Maintenance.DTOs
{
    public class MaintenanceStatusResponse
    {
        public string Status { get; set; } // Exemple : OK, WARNING, ERROR
        public DateTime LastChecked { get; set; } // Dernière vérification
    }
}