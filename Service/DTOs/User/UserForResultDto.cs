using Domain.Enums;
using Service.DTOs.UserImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.User
{
    public class UserForResultDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public UserImageForResultDto Image { get; set; }
    }
}
