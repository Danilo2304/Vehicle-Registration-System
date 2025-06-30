using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Implementation;
using RegistracijaVozila.Results;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext authDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthService(AuthDbContext authDbContext, UserManager<IdentityUser> userManager,
            ITokenService tokenService, IMapper mapper)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        public async Task<RepositoryResult<UserDto>> RegisterAsync(RegisterRequestDto request)
        {
            var user = new IdentityUser
            {
                UserName = request.Username?.Trim(),
                Email = request.Email?.Trim()
            };

            var identityResult = await userManager.CreateAsync(user,request.Password);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description).ToList();
                return RepositoryResult<UserDto>.Fail(string.Join("|", errors));
            }

            await userManager.AddToRoleAsync(user, "Zaposleni");

            var response = mapper.Map<UserDto>(user);

            return RepositoryResult<UserDto>.Ok(response);
        }

        public async Task<RepositoryResult<string>> LoginAsync(LoginRequestDto request)
        {
            var identityUser = await userManager.FindByNameAsync(request.Username);

            if(identityUser==null)
            {
                return RepositoryResult<string>.Fail($"INVALID_USERNAME: Username {request.Username} not found");
            }

            if (identityUser.Email != request.Email)
            {
                return RepositoryResult<string>.Fail($"INVALID_EMAIL: Email {request.Email} not found");
            }

            var passwordValid = await userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!passwordValid)
            {
                return RepositoryResult<string>.Fail("INVALID_PASSWORD: Incorrect password");
            }

            var token = await tokenService.GenerateJwtTokenAsync(identityUser);

            return RepositoryResult<string>.Ok(token);
        }

        public async Task<RepositoryResult<UserDto>> DeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if(user == null)
            {
                return RepositoryResult<UserDto>.Fail($"USER_NOT_FOUND: User with id {id} not found");
            }

            authDbContext.Users.Remove(user);
            await authDbContext.SaveChangesAsync();

            var response = mapper.Map<UserDto>(user);

            return RepositoryResult<UserDto>.Ok(response);
        }

        public async Task<RepositoryResult<UserDto>> UpdateUserAsync(UpdateUserRequestDto request)
        {
            var user = await userManager.FindByIdAsync(request.Id);

            if(user == null)
            {
                return RepositoryResult<UserDto>.Fail($"USER_NOT_FOUND: User with id {request.Id} not found");
            }


            user.Email = request.Email;
            user.UserName = request.Username;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                return RepositoryResult<UserDto>.Fail($"UPDATE_FAILED: {errorMessage}");
            }

            var response = mapper.Map<UserDto>(user);

            return RepositoryResult<UserDto>.Ok(response);

        }

        public async Task<RepositoryResult<UserDto>> ChangePasswordAsync
            (string userId, string currentPassword, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RepositoryResult<UserDto>.Fail($"USER_NOT_FOUND: User with id {userId} not found");
            }

            var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join("; ", result.Errors.Select(e=>e.Description));
                return RepositoryResult<UserDto>.Fail($"CHANGE_PASSWORD_FAILED: {errorMessage}");
            }

            await userManager.UpdateSecurityStampAsync(user);

            var response = mapper.Map<UserDto>(user);

            return RepositoryResult<UserDto>.Ok( response );
                
        }

        public async Task<RepositoryResult<UserDto>> ResetPasswordAsync(string userId, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RepositoryResult<UserDto>.Fail($"USER_NOT_FOUND: User with id {userId} not found");
            }

            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                return RepositoryResult<UserDto>.Fail($"RESET_PASSWORD_FAILED: {errorMessage}");
            }

            await userManager.UpdateSecurityStampAsync(user);

            var response = mapper.Map<UserDto>(user);

            return RepositoryResult<UserDto>.Ok(response);
        }
    }
}






