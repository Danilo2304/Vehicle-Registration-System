using Microsoft.AspNetCore.Identity;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
