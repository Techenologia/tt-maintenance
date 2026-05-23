namespace TT.Backend.Modules.Maintenance.Entities
{
    public enum Severity
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum CorrectiveStatus
    {
        Reported,
        Diagnosed,
        InProgress,
        Resolved,
        Closed
    }

    public class CorrectiveTaskEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EquipmentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ProblemDescription { get; set; } = string.Empty;
        public string? Solution { get; set; }
        public Severity Severity { get; set; }
        public CorrectiveStatus Status { get; set; } = CorrectiveStatus.Reported;
        public string? AssignedTo { get; set; }
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResolvedAt { get; set; }
    }
}