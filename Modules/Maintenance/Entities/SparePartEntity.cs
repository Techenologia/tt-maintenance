namespace TT.Backend.Modules.Maintenance.Entities
{
    public class SparePartEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; } // seuil d'alerte
        public decimal UnitPrice { get; set; }
        public string? Supplier { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}