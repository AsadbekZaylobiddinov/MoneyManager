using AutoMapper;
using Data.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.User;
using Service.DTOs.UserImage;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MonthService : IMonthService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Month> monthRepository;
        private readonly IRepository<Day> dayRepository;
        public MonthService(
            IMapper mapper,
            IRepository<Month> repository,
            IRepository<Day> dayRepository)
        {
            this.mapper = mapper;
            this.monthRepository = repository;
            this.dayRepository = dayRepository;
        }

        public async ValueTask<Month> CreateAsync(Month month)
        {
            var result = await this.monthRepository.InsertAsync(month);
            await this.monthRepository.SaveChangesAsync();
            return result;
        }
        public async ValueTask<Month> DeleteAsync(long id)
        {
            var month = await this.monthRepository.SelectAsync(u => u.Id.Equals(id));
            if (month is null)
                throw new CustomException(404, "Month not found");

            await this.monthRepository.DeleteAsync(month);
            await this.monthRepository.SaveChangesAsync();
            return month;
        }

        //public async ValueTask<MonthForResultDto> GetMonthWithDays(int Id, int userId)
        //{
        //    var month = await monthRepository.SelectAsync(u => u.Id.Equals(Id));
        //    var days = dayRepository.SelectAll(
        //    d => d.UserId == userId && d.MonthId == Id,
        //    includes: new[] { "Expenses" });
        //); 

        //}
    }
}
