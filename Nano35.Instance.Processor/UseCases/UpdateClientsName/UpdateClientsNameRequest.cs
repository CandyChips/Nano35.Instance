using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsName
{
    public class UpdateClientsNameRequest :
        EndPointNodeBase<
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

        public override async Task<IUpdateClientsNameResultContract> Ask(
            IUpdateClientsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Clients.FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken));
            result.WorkerId = input.UpdaterId;
            result.Name = input.Name;
            
            return new UpdateClientsNameSuccessResultContract();
        }
    }
}