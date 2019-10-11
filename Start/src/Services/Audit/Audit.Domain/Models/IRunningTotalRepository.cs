using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Domain.Models
{
    public interface IRunningTotalRepository
    {
        RunningTotal CreateRunningTotal(RunningTotal runningTotal);

        Task<bool> SaveChangesAsync();

        Task<double> GetPrevioudTotal();
    }
}
