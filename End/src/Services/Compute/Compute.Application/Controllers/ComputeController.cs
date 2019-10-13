using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compute.Application.Services;
using Compute.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Compute.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComputeController : Controller
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IAuditService _auditService;

        public ComputeController(IOperationRepository operationRepository, IAuditService auditService)
        {
            _operationRepository = operationRepository;
            _auditService = auditService;
        }
        // GET api/v1/compute/add/7/8
        [HttpPost("add/{x}/{y}/{typeNum}")]
        public async Task<ActionResult<double>> Cal(double x, double y, int typeNum)
        {
            var operation = new Operation(x, y, typeNum);
            var storedOperation = _operationRepository.CreateOp(operation);
            var result = storedOperation.Result;

            await _auditService.SubmitForAuditAsync(result);
            await _operationRepository.SaveChangesAsync();

            return Ok(result);
        }

        // GET api/v1/ops
        [HttpGet("ops")]
        public async Task<ActionResult<IEnumerable<Operation>>> GetAllOps()
        {
            var ops = await _operationRepository.GetAllOpsAsync();
            return Ok(ops);
        }

        // GET api/v1/ops/5
        [HttpGet("ops/{id}")]
        public async Task<ActionResult<Operation>> GetOpsById(int id)
        {
            var op = await _operationRepository.GetOpAsync(id);
            return Ok(op);
        }
    }
}