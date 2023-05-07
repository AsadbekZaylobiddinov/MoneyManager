using AutoMapper;
using Data.IRepositories;
using Domain.Entities;
using Service.DTOs.Day;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Expense> expenseRepository;
        public ExpenseService(
            IMapper mapper,
            IRepository<Expense> repository)
        {
            this.mapper = mapper;
            this.expenseRepository = repository;
        }
        public async ValueTask<Expense> CreateAsync(Expense expense)
        {
            // Добавляем новый объект Day в базу данных
            await expenseRepository.InsertAsync(expense);
            await expenseRepository.SaveChangesAsync();

            return expense;
        }
        public async ValueTask<Expense> GetByIdAsync(long Id)
        {
            var expense = await expenseRepository.SelectAsync(e => e.Id.Equals(Id));

            return expense;
        }
        public async ValueTask<IQueryable<Expense>> GetAll(long user_id, long day_id)
        {
            var expenses = expenseRepository.SelectAll(e => e.UserId == user_id && e.DayId == day_id);

            return expenses;
        }
}
}
