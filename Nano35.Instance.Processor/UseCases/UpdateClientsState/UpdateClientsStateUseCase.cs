using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsState
{
    public class UpdateClientsStateUseCase :
        EndPointNodeBase<
            IUpdateClientsStateRequestContract,
            IUpdateClientsStateResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsStateUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsStateSuccessResultContract : 
            IUpdateClientsStateSuccessResultContract
        {
            
        }

        public override async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Clients.FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken));
            result.WorkerId = input.UpdaterId;
            result.ClientStateId = input.StateId;
            
            return new UpdateClientsStateSuccessResultContract() ;
        }
    }
}