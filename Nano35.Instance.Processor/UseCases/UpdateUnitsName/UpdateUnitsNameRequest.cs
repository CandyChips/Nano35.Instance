using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsName
{
    public class UpdateUnitsNameRequest :
        IPipelineNode<
            IUpdateUnitsNameRequestContract,
            IUpdateUnitsNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsNameRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateUnitsNameSuccessResultContract : 
            IUpdateUnitsNameSuccessResultContract
        {
            
        }

        public async Task<IUpdateUnitsNameResultContract> Ask(
            IUpdateUnitsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Units.FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken));
            result.Name = input.Name;
            result.CreatorId = input.UpdaterId;
            
            return new UpdateUnitsNameSuccessResultContract() ;
        }
    }
}