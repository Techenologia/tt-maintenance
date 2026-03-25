namespace TT.Backend.Core.Security;

public interface IUserContext
{
    string UserId { get; }
    string Email { get; }
}