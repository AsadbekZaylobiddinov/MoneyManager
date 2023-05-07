using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace MoneyManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService incomeService;
        public IncomeController(IIncomeService incomeService)
        {
            this.incomeService = incomeService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Income>> PostAsync(Income income)
          => Ok(await incomeService.CreateAsync(income));


        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.incomeService.GetByIdAsync(id)
            });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllIncomes()
        {
            int userId = int.Parse(HttpContext.Request.Query["user_id"]);
            int dayId = int.Parse(HttpContext.Request.Query["day_id"]);
            return Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.incomeService.GetAll(userId, dayId)
            }); ;
        }
    }
}
