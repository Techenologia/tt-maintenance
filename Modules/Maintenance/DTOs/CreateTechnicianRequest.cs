using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class CreateTechnicianRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public TechnicianSpecialty Specialty { get; set; }
        public string? Notes { get; set; }
    }
}