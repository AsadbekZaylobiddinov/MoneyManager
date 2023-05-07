using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Day
{
    public class DayForCreationDto
    {
        public long UserId { get; set; }
        public long MonthId { get; set; }
        public int Number { get; set; }
    }
}
