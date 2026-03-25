namespace TT.Backend.Modules.Maintenance.DTOs
{
    public class CreateSparePartRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Supplier { get; set; }
    }
}