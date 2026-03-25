using TT.Backend.Core.Entities; // <- pour UserEntity

namespace TT.Backend.Core.Interfaces
{
    public interface IUserService
    {
        UserEntity? GetUserById(int id);
        List<UserEntity> GetAllUsers();
    }
}