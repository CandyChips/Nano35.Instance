using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsType
{
    public class UpdateClientsTypeUseCase :
        EndPointNodeBase<
            IUpdateClientsTypeRequestContract,
            IUpdateClientsTypeResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateClientsTypeUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateClientsTypeSuccessResultContract : 
            IUpdateClientsTypeSuccessResultContract
        {
            
        }

        public override async Task<IUpdateClientsTypeResultContract> Ask(
            IUpdateClientsTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Clients.FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken);
            result.WorkerId = input.UpdaterId;
            result.ClientTypeId = input.TypeId;
            
            return new UpdateClientsTypeSuccessResultContract() ;
        }
    }
}