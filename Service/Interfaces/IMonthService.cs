using Domain.Entities;
using Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMonthService
    {
        ValueTask<Month> CreateAsync(Month month);
        ValueTask<Month> DeleteAsync(long Id);
    }
}
