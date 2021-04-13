using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsType
{
    public class UpdateUnitsTypeUseCase :
        EndPointNodeBase<
            IUpdateUnitsTypeRequestContract,
            IUpdateUnitsTypeResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateUnitsTypeUseCase(
            ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Units.FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            result.UnitTypeId = input.TypeId;
            result.CreatorId = input.UpdaterId;
            return new UpdateUnitsTypeSuccessResultContract();
        }
    }
}