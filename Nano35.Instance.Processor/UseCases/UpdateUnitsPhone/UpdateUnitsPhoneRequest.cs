using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneRequest :
        EndPointNodeBase<
            IUpdateUnitsPhoneRequestContract,
            IUpdateUnitsPhoneResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsPhoneRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateUnitsPhoneSuccessResultContract : 
            IUpdateUnitsPhoneSuccessResultContract
        {
            
        }

        public override async Task<IUpdateUnitsPhoneResultContract> Ask(
            IUpdateUnitsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Units.FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken));
            result.Phone = input.Phone;
            result.CreatorId = input.UpdaterId;
            
            return new UpdateUnitsPhoneSuccessResultContract() ;
        }
    }
}