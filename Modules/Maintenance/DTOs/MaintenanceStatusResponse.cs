namespace TT.Backend.Modules.Maintenance.DTOs;

public class MaintenanceStatusResponse
{
    public required string Status      { get; set; }
    public DateTime        LastChecked { get; set; }
}
