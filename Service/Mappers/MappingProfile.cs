using AutoMapper;
using Domain.Entities;
using Service.DTOs.Day;
using Service.DTOs.User;
using Service.DTOs.UserImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            // User
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<User, UserForResultDto>().ReverseMap();
            CreateMap<User, UserForUpdateDto>().ReverseMap();
            CreateMap<User, UserForChangePasswordDto>().ReverseMap();

            //UserImage

            CreateMap<UserImage, UserImageForCreationDto>().ReverseMap();
            CreateMap<UserImage, UserImageForResultDto>().ReverseMap();

            //Day
            CreateMap<Day, DayForCreationDto>().ReverseMap();
            CreateMap<Day, DayForResultDto>().ReverseMap();
        }
    }
}
