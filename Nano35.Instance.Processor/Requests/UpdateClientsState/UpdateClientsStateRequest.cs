using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateClientsState
{
    public class UpdateClientsStateRequest :
        IPipelineNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsStateRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsStateSuccessResultContract : 
            IUpdateClientsStateSuccessResultContract
        {
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Clients
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.ClientStateId = input.State;
            return new UpdateClientsStateSuccessResultContract() ;
        }
    }
}