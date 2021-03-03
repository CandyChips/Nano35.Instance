using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsEmail
{
    public class UpdateClientsEmailRequest :
        IPipelineNode<
            IUpdateClientsEmailRequestContract,
            IUpdateClientsEmailResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsEmailRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsEmailSuccessResultContract : 
            IUpdateClientsEmailSuccessResultContract
        {
            
        }

        public async Task<IUpdateClientsEmailResultContract> Ask(
            IUpdateClientsEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Clients.FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken));
            result.WorkerId = input.UpdaterId;
            result.Email = input.Email;

            return new UpdateClientsEmailSuccessResultContract();
        }
    }
}