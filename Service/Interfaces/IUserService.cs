using Domain.Entities;
using Service.DTOs.User;
using Service.DTOs.UserImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserForResultDto> CreateAsync(UserForCreationDto dto);
        ValueTask<UserForResultDto> UpdateAsync(UserForUpdateDto dto);
        ValueTask<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto);
        ValueTask<UserForResultDto> GetByIdAsync(long id);
        ValueTask<IEnumerable<UserForResultDto>> GetAllAsync(
            Expression<Func<User, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<UserImageForResultDto> ImageUploadAsync(UserImageForCreationDto dto);
        ValueTask<bool> DeleteUserImageAsync(long userId);
        ValueTask<UserImageForResultDto> GetUserImageAsync(long userId);
        ValueTask<UserForResultDto> CheckUserAsync(string username, string password);
    }
}
