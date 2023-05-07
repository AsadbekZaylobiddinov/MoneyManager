using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace MoneyManager.Api.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class MonthController : ControllerBase
        {
            private readonly IMonthService monthService;
            public MonthController(IMonthService monthService)
            {
                this.monthService = monthService;
            }
        }
}
