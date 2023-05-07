using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Category
{
    public class CategoryForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
