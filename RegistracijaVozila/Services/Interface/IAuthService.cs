using Microsoft.AspNetCore.Identity;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;
using System.Security.Claims;

namespace RegistracijaVozila.Services.Interface
{
    public interface IAuthService
    {
        Task<RepositoryResult<IEnumerable<UserDto>>> GetAll();

        Task<RepositoryResult<UserDto>> RegisterAsync(RegisterRequestDto request, ClaimsPrincipal currentUser);

        Task<RepositoryResult<LoginResponseDto>> LoginAsync(LoginRequestDto request);

        Task<RepositoryResult<UserDto>> DeleteAsync(string id);

        Task<RepositoryResult<UserDto>> UpdateUserAsync
            (UpdateUserRequestDto request, ClaimsPrincipal currentUser);

        Task<RepositoryResult<UserDto>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<RepositoryResult<UserDto>> ResetPasswordAsync(string userId, string newPassword);
    }
}
