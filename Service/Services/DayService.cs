using AutoMapper;
using Data.IRepositories;
using Domain.Entities;
using Service.DTOs.Day;
using Service.DTOs.User;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
    public class DayService : IDayService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Day> dayRepository;
        private readonly IRepository<Month> monthRepository;
        public DayService(
            IMapper mapper,
            IRepository<Day> repository,
            IRepository<Month> repository1)
        {
            this.mapper = mapper;
            this.dayRepository = repository;
            this.monthRepository = repository1;
        }
        public async ValueTask<Day> CreateAsync(DayForCreationDto dto)
        {
            var month = await monthRepository.SelectAsync(s => s.Id.Equals(dto.MonthId));
            if (month == null)
            {
                throw new Exception($"Month with ID {dto.MonthId} not found");
            }

            // Создаем новый объект Day
            var day = this.mapper.Map<Day>(dto);

            // Добавляем новый объект Day в базу данных
            await dayRepository.InsertAsync(day);
            await dayRepository.SaveChangesAsync();

            return day;
        }

        //public IEnumerable<Day> GetDaysWithExpensesForUserAndMonth(int user_id, int month_id)
        //{
        //    var days = dayRepository.SelectAll(
        //        d => d.UserId == user_id && d.MonthId == month_id,
        //        includes: new[] { "Expenses" }
        //    );
        //    return days;

        //}
    }
}
