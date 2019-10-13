using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Audit.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IRunningTotalRepository _runningTotalRepository;

        public AuditController(IRunningTotalRepository runningTotalRepository)
        {
            _runningTotalRepository = runningTotalRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] double number)
        {
            var previousTotal = await _runningTotalRepository.GetPrevioudTotal();
            var runningTotal = new RunningTotal(previousTotal, number);
            _runningTotalRepository.CreateRunningTotal(runningTotal);
            await _runningTotalRepository.SaveChangesAsync();

            return Ok();

        }
    }
}