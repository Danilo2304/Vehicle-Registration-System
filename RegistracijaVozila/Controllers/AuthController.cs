using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAuthService authService;

        public AuthController(UserManager<IdentityUser> userManager, 
            IAuthService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await authService.RegisterAsync(request);

            if (!result.Success)
            {
                ModelState.AddModelError("RegistrationError", result.Message);
                return ValidationProblem(ModelState);
            }

            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await authService.LoginAsync(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(new { Token = result.Data });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await authService.DeleteAsync(id);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(result.Data);
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequestDto request)
        {
            var result = await authService.UpdateUserAsync(request);

            if (!result.Success)
            {
                return BadRequest(result.Errors.Any() ? result.Errors : new[] { result.Message });
            }

            return Ok(result.Data);
        }

        //[Authorize]
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeRequestDto request)
        {
            var userId = userManager.GetUserId(User);

            var result = await authService.ChangePasswordAsync
                (userId, request.CurrentPassword, request.NewPassword);

            if (!result.Success)
            {
                return BadRequest(result.Errors.Any() ? result.Errors : new[] { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequestDto request)
        {
            var result = await authService.ResetPasswordAsync(request.Id, request.NewPassword);

            if (!result.Success)
            {
                return BadRequest(result.Errors.Any() ? result.Errors : new[] { result.Message });
            }

            return Ok(result.Data);
        }


        [HttpGet]
        [Route("vratiNesto")]
        public async Task<IActionResult> VratiNesto()
        {
            var response = new
            {
                message = "Đe si, što ima?",
                time = DateTime.Now
            };

            return Ok(response);
        }
            
    }
}
