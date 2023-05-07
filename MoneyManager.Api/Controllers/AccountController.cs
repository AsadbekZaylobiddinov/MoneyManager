using Azure;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.User;
using Service.Interfaces;

namespace MoneyManager.Api.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        public AccountController(IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> PostUserAsync(UserForCreationDto dto)
           => Ok(new
           {
               Code = 200,
               Error = "Success",
               Data = await this.userService.CreateAsync(dto)
           });

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken(string username, string password = null)
        {
            var token = await this.authService.GenerateTokenAsync(username, password);
            return Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = token
            });
        }
    }
}
