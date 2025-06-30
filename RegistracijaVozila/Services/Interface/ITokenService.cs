using Microsoft.AspNetCore.Identity;

namespace RegistracijaVozila.Services.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(IdentityUser user);
    }
}
