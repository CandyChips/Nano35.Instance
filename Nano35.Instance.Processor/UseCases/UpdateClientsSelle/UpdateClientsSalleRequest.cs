using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsSelle
{
    public class UpdateClientsSelleRequest :
        IPipelineNode<
            IUpdateClientsSelleRequestContract,
            IUpdateClientsSelleResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsSelleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsSelleSuccessResultContract : 
            IUpdateClientsSelleSuccessResultContract
        {
            
        }

        public async Task<IUpdateClientsSelleResultContract> Ask(
            IUpdateClientsSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Clients.FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken));
            result.WorkerId = input.UpdaterId;
            result.Salle = (double)input.Selle;
            
            return new UpdateClientsSelleSuccessResultContract() ;
        }
    }
}