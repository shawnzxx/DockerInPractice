using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Compute.Domain.Models
{
    public interface IOperationRepository
    {
        Task<bool> SaveChangesAsync();

        Operation CreateOp(Operation operation);

        Task<IEnumerable<Operation>> GetAllOpsAsync();

        Task<Operation> GetOpAsync(int operationId);

    }
}
