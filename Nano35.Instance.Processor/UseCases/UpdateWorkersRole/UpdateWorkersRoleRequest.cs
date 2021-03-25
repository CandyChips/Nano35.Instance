using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersRole
{
    public class UpdateWorkersRoleRequest :
        EndPointNodeBase<
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

        public override async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}