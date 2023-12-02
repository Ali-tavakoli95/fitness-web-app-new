using api.Models;

namespace api.Interfaces;
public interface ITokenService
{
    public string CreateToken(AppFitUser user);
}