using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.User
{
    public class UserForChangePasswordDto
    {

        [Required(ErrorMessage = "To'ldirish Majburiy!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Eski parolingizni kiritishingiz shart!")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yangi parolni kiritishingiz shart!")]
        public string NewPassword { get; set; }

        [Compare("NewPasword")]
        public string ComfirmPassword { get; set; }
    }
}
