using TT.Backend.Core.Entities;

namespace TT.Backend.Core.Interfaces
{
    public interface IAuthService
    {
        UserEntity? Login(string email, string password);
        UserEntity? Register(UserEntity user, string password);
        bool VerifyPassword(UserEntity user, string password);
        string GenerateToken(UserEntity user); // ✅ ajout
    }
}