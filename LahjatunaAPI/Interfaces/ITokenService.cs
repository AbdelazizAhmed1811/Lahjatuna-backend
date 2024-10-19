using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User appUser, IList<string> roles);
    }
}
