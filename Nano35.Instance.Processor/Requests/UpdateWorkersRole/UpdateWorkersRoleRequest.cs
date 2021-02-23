using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateWorkersRole
{
    public class UpdateWorkersRoleRequest :
        IPipelineNode<
            IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateWorkersRoleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateWorkersRoleSuccessResultContract : 
            IUpdateWorkersRoleSuccessResultContract
        {
            
        }

        public async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}