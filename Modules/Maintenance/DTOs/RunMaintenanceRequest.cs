namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class RunMaintenanceRequest
    {
        public string TaskName { get; set; }
        public string Description { get; set; }

        // <-- CONSTRUCTEUR à ajouter ici
        public RunMaintenanceRequest()
        {
            TaskName = string.Empty;
            Description = string.Empty;
        }
    }
}