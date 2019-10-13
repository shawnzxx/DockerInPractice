using Compute.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Compute.Infrastructure.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly OperationDbContext _dbContext;

        public OperationRepository(OperationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Operation>> GetAllOpsAsync()
        {
            return await _dbContext.Operations.ToListAsync();
        }

        public async Task<Operation> GetOpAsync(int operationId)
        {
            return await _dbContext.Operations.FirstOrDefaultAsync(o => o.Id == operationId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            //return true if 1 or more entities were changed
            return (await _dbContext.SaveChangesAsync() > 0);
        }

        public Operation CreateOp(Operation operation)
        {
            return _dbContext.Operations.Add(operation).Entity;
        }
    }
}
