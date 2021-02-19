using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateClientsName
{
    public class UpdateClientsNameRequest :
        IPipelineNode<
            IUpdateClientsNameRequestContract, 
            IUpdateClientsNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsNameRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsNameSuccessResultContract : 
            IUpdateClientsNameSuccessResultContract
        {
            
        }

        public async Task<IUpdateClientsNameResultContract> Ask(
            IUpdateClientsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Clients
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.Name = input.Name;
            return new UpdateClientsNameSuccessResultContract();
        }
    }
}