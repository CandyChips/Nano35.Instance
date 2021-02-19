using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateClientsType
{
    public class UpdateClientsTypeRequest :
        IPipelineNode<
            IUpdateClientsTypeRequestContract,
            IUpdateClientsTypeResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsTypeRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsTypeSuccessResultContract : 
            IUpdateClientsTypeSuccessResultContract
        {
            
        }

        public async Task<IUpdateClientsTypeResultContract> Ask(
            IUpdateClientsTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Clients
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.ClientTypeId = input.Type;
            return new UpdateClientsTypeSuccessResultContract() ;
        }
    }
}