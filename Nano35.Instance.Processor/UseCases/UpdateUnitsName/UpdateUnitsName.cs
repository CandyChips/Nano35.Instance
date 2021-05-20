using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsName
{
    public class UpdateUnitsName : EndPointNodeBase<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateUnitsName(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateUnitsNameResultContract>> Ask(
            IUpdateUnitsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfUnit = await _context
                .Units
                .FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            if (entityOfUnit == null) return new UseCaseResponse<IUpdateUnitsNameResultContract>("Подразделение не найдено.");
            entityOfUnit.Name = input.Name;
            return new UseCaseResponse<IUpdateUnitsNameResultContract>(new UpdateUnitsNameResultContract());
        }
    }
}