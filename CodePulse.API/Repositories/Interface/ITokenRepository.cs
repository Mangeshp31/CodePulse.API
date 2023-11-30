using Microsoft.AspNetCore.Identity;

namespace ViewPoint.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
