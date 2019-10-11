using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compute.Application.Services
{
    public interface IAuditService
    {
        Task SubmitForAuditAsync(double number);
    }
}
