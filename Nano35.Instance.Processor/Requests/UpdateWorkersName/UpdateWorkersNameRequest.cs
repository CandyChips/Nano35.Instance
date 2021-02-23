using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateWorkersName
{
    public class UpdateWorkersNameRequest :
        IPipelineNode<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateWorkersNameRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateWorkersNameSuccessResultContract : 
            IUpdateWorkersNameSuccessResultContract
        {
            
        }

        public async Task<IUpdateWorkersNameResultContract> Ask(
            IUpdateWorkersNameRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}