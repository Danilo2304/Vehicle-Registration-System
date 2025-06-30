using Microsoft.AspNetCore.Identity;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IAuthService
    {
        Task<RepositoryResult<UserDto>> RegisterAsync(RegisterRequestDto request);

        Task<RepositoryResult<string>> LoginAsync(LoginRequestDto request);

        Task<RepositoryResult<UserDto>> DeleteAsync(string id);

        Task<RepositoryResult<UserDto>> UpdateUserAsync(UpdateUserRequestDto request);

        Task<RepositoryResult<UserDto>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<RepositoryResult<UserDto>> ResetPasswordAsync(string userId, string newPassword);
    }
}
