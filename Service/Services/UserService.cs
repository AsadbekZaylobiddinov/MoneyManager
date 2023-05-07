using AutoMapper;
using Data.IRepositories;
using Domain.Entities;
using Service.DTOs.User;
using Service.DTOs.UserImage;
using Service.Exceptions;
using Service.Extensions;
using Service.Helpers;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserImage> userImageRepository;
        public UserService(
            IMapper mapper,
            IRepository<User> repository,
            IRepository<UserImage> userImageRepository)
        {
            this.mapper = mapper;
            this.userRepository = repository;
            this.userImageRepository = userImageRepository;
        }

        public async ValueTask<UserForResultDto> CreateAsync(UserForCreationDto dto)
        {
            User user = await this.userRepository.SelectAsync(u => u.UserName.ToLower() == dto.UserName.ToLower());
            if (user is not null)
                throw new CustomException(403, "User already exist with this username");

            User mappedUser = mapper.Map<User>(dto);
            mappedUser.Password = PasswordHelper.Hash(dto.Password);
            var result = await this.userRepository.InsertAsync(mappedUser);
            await this.userRepository.SaveChangesAsync();
            return this.mapper.Map<UserForResultDto>(result);
        }
        public async ValueTask<bool> DeleteAsync(long id)
        {
            var user = await this.userRepository.SelectAsync(u => u.Id.Equals(id));
            if (user is null)
                throw new CustomException(404, "User not found");

            await this.userRepository.DeleteAsync(user);
            await this.userRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<UserForResultDto>> GetAllAsync(
            Expression<Func<User, bool>> expression = null, string search = null)
        {
            var users = userRepository.SelectAll(expression, isTracking: false);
            var result = mapper.Map<IEnumerable<UserForResultDto>>(users);

            foreach (var item in result)
                item.Image = mapper.Map<UserImageForResultDto>(
                    await this.userImageRepository.SelectAsync(t => t.UserId.Equals(item.Id)));

            if (!string.IsNullOrEmpty(search))
                return result.Where(
                    u => u.FirstName.ToLower().Contains(search.ToLower()) ||
                    u.UserName.ToLower().Contains(search.ToLower())).ToList();

            return result;
        }
        public async ValueTask<UserForResultDto> GetByIdAsync(long id)
        {
            var user = await userRepository.SelectAsync(u => u.Id.Equals(id));
            if (user is null)
                throw new CustomException(404, "User not found");

            var result = mapper.Map<UserForResultDto>(user);
            result.Image = mapper.Map<UserImageForResultDto>(
                await this.userImageRepository.SelectAsync(t => t.UserId.Equals(result.Id)));

            return result;
        }

        public async ValueTask<UserForResultDto> UpdateAsync(UserForUpdateDto dto)
        {
            var updatingUser = await userRepository.SelectAsync(u => u.Id.Equals(dto.Id));
            if (updatingUser is null)
                throw new CustomException(404, "User not found");

            this.mapper.Map(dto, updatingUser);
            updatingUser.UpdatedAt = DateTime.UtcNow;
            await this.userRepository.SaveChangesAsync();

            var result = mapper.Map<UserForResultDto>(updatingUser);
            result.Image = mapper.Map<UserImageForResultDto>(
               await this.userImageRepository.SelectAsync(t => t.UserId.Equals(result.Id)));

            return result;
        }

        public async ValueTask<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto)
        {
            User existUser = await userRepository.SelectAsync(u => u.UserName == dto.UserName);
            if (existUser is null)
                throw new Exception("This username is not exist");
            else if (dto.NewPassword != dto.ComfirmPassword)
                throw new Exception("New password and confirm password are not equal");
            else if (existUser.Password != dto.OldPassword)
                throw new Exception("Password is incorrect");

            existUser.Password = dto.ComfirmPassword;
            await userRepository.SaveChangesAsync();
            return mapper.Map<UserForResultDto>(existUser);
        }

        public async ValueTask<UserImageForResultDto> ImageUploadAsync(UserImageForCreationDto dto)
        {
            var user = await this.userRepository.SelectAsync(t => t.Id.Equals(dto.UserId));
            if (user is null)
                throw new CustomException(404, "User is not found");

            byte[] image = dto.Image.ToByteArray();
            var fileExtension = Path.GetExtension(dto.Image.FileName);
            var fileName = Guid.NewGuid().ToString("N") + fileExtension;
            var webRootPath = EnvironmentHelper.WebHostPath;
            var folder = Path.Combine(webRootPath, "uploads", "images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, fileName);
            using var imageStream = new MemoryStream(image);

            using var imagePath = new FileStream(fullPath, FileMode.CreateNew);
            imageStream.WriteTo(imagePath);

            var userImage = new UserImage
            {
                Name = fileName,
                Path = fullPath,
                UserId = dto.UserId,
                User = user,
                CreatedAt = DateTime.UtcNow,
            };

            var createdImage = await this.userImageRepository.InsertAsync(userImage);
            await this.userImageRepository.SaveChangesAsync();
            return mapper.Map<UserImageForResultDto>(createdImage);
        }
        public async ValueTask<bool> DeleteUserImageAsync(long userId)
        {
            var userImage = await this.userImageRepository.SelectAsync(t => t.UserId.Equals(userId));
            if (userImage is null)
                throw new CustomException(404, "Image is not found");

            File.Delete(userImage.Path);
            await this.userImageRepository.DeleteAsync(userImage);
            await this.userImageRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<UserImageForResultDto> GetUserImageAsync(long userId)
        {
            var userImage = await this.userImageRepository.SelectAsync(t => t.UserId.Equals(userId));
            if (userImage is null)
                throw new CustomException(404, "Image is not found");
            return mapper.Map<UserImageForResultDto>(userImage);
        }

        public async ValueTask<UserForResultDto> CheckUserAsync(string username, string password)
        {
            var user = await userRepository.SelectAsync(u => u.UserName == username);
            if (user != null && PasswordHelper.Verify(password, user.Password))
            {
                return this.mapper.Map<UserForResultDto>(user);
            }
            else
            {
                throw new CustomException(404,"User Not Found");
            }
        }
    }
}
