using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class UpdateClientsPhoneRequest :
        IPipelineNode<
            IUpdateClientsPhoneRequestContract, 
            IUpdateClientsPhoneResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsPhoneRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsPhoneSuccessResultContract : 
            IUpdateClientsPhoneSuccessResultContract
        {
            
        }

        public async Task<IUpdateClientsPhoneResultContract> Ask(
            IUpdateClientsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Clients.FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken));
            result.WorkerId = input.UpdaterId;
            result.Phone = input.Phone;
            
            return new UpdateClientsPhoneSuccessResultContract();
        }
    }
}