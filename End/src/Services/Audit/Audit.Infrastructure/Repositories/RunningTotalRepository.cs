using Audit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Infrastructure.Repositories
{
    public class RunningTotalRepository : IRunningTotalRepository
    {
        private readonly RunningTotalDbContext _dbContext;

        public RunningTotalRepository(RunningTotalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RunningTotal CreateRunningTotal(RunningTotal runningTotal)
        {
            var dbSet = _dbContext.RunningTotal.Add(runningTotal);
            return dbSet.Entity;
        }

        public async Task<double> GetPrevioudTotal()
        {
            if (await _dbContext.RunningTotal.AnyAsync())
            {
                var maxId = await _dbContext.RunningTotal.MaxAsync(r => r.Id);
                var previousTotal = await _dbContext.RunningTotal.SingleAsync(r => r.Id == maxId);
                return previousTotal != null ? previousTotal.GetRunningTotal() : 0;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            //return true if 1 or more entities were changed
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }
}
