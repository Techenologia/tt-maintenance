namespace TT.Backend.Modules.Maintenance.Entities
{
    public enum TechnicianStatus
    {
        Available,
        Busy,
        OnLeave,
        Inactive
    }

    public enum TechnicianSpecialty
    {
        Electrical,
        Mechanical,
        HVAC,
        IT,
        Civil,
        General
    }

    public class TechnicianEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public TechnicianSpecialty Specialty { get; set; }
        public TechnicianStatus Status { get; set; } = TechnicianStatus.Available;
        public DateTime HiredAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }
}