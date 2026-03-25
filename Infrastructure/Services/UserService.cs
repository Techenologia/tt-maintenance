using TT.Backend.Core.Entities;
using TT.Backend.Core.Interfaces;
using TT.Backend.Infrastructure.Data; // <- Ajouté pour AppDbContext

namespace TT.Backend.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public UserEntity? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<UserEntity> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}