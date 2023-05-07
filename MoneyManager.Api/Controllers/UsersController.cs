using Service.DTOs.User;
using Service.Interfaces;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.UserImage;

namespace Api.Controllers;

#pragma warning disable
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

    [HttpPost]
    public async ValueTask<ActionResult<UserForResultDto>> PostAsync(UserForCreationDto dto)
          => Ok(await userService.CreateAsync(dto));

    [HttpPut("update")]
        public async Task<IActionResult> PutUserAsync(UserForUpdateDto dto)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.UpdateAsync(dto)
            });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteUserAsync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.DeleteAsync(id)
            });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.GetByIdAsync(id)
            });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllUsers()
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.GetAllAsync()
            });

        [HttpPost("image-upload")]
        public async ValueTask<IActionResult> UploadImage([FromForm] UserImageForCreationDto dto)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.ImageUploadAsync(dto)
            });

        [HttpDelete("image-delete/{userId:long}")]
        public async Task<IActionResult> DeleteUserImage(long userId)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.DeleteUserImageAsync(userId)
            });

        [HttpGet("image-get/{userId:long}")]
        public async Task<IActionResult> GetUserImage(long userId)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.GetUserImageAsync(userId)
            });
}
