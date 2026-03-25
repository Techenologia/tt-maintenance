namespace TT.Backend.Core.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // ✅ ajout
        public string Role { get; set; } = "User"; // ✅ ajout
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}