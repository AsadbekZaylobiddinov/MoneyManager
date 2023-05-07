using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Day;
using Service.DTOs.User;
using Service.Interfaces;
using Service.Services;

namespace MoneyManager.Api.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class DayController : ControllerBase
        {
            private readonly IDayService dayService;
            public DayController(IDayService dayService)
            {
                this.dayService = dayService;
            }
        }
}
