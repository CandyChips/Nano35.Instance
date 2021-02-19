using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateClientsSalle
{
    public class UpdateClientsSalleRequest :
        IPipelineNode<
            IUpdateClientsSalleRequestContract,
            IUpdateClientsSalleResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsSalleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsSalleSuccessResultContract : 
            IUpdateClientsSalleSuccessResultContract
        {
            
        }

        public async Task<IUpdateClientsSalleResultContract> Ask(
            IUpdateClientsSalleRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await ( _context.Clients
                    .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken)
                );

            result.Salle = (double)input.Salle;
            return new UpdateClientsSalleSuccessResultContract() ;
        }
    }
}