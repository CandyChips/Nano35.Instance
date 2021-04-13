using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsName
{
    public class UpdateClientsNameUseCase :
        EndPointNodeBase<
            IUpdateClientsNameRequestContract, 
            IUpdateClientsNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsNameUseCase(
            ApplicationContext context)
        {
            _context = context;
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