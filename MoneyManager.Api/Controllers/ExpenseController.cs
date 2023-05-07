using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.User;
using Service.Interfaces;
using Service.Services;

namespace MoneyManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Expense>> PostAsync(Expense expense)
          => Ok(await expenseService.CreateAsync(expense));


        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.expenseService.GetByIdAsync(id)
            });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllExpenses()
        {
            int userId = int.Parse(HttpContext.Request.Query["user_id"]);
            int dayId = int.Parse(HttpContext.Request.Query["day_id"]);
            return Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.expenseService.GetAll(userId, dayId)
            }); ;
    }
        }
           
}
