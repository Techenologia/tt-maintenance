using TT.Backend.Modules.Maintenance.Entities;

namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class CreateCorrectiveTaskRequest
    {
        public Guid EquipmentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ProblemDescription { get; set; } = string.Empty;
        public Severity Severity { get; set; }
        public string? AssignedTo { get; set; }
    }
}