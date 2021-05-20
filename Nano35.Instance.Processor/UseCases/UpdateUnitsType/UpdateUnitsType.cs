using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsType
{
    public class UpdateUnitsType : EndPointNodeBase<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateUnitsType(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateUnitsTypeResultContract>> Ask(
            IUpdateUnitsTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfUnit = await _context
                .Units
                .FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            if (entityOfUnit == null) return new UseCaseResponse<IUpdateUnitsTypeResultContract>("Подразделение не найдено.");
            entityOfUnit.UnitTypeId = input.TypeId;
            return new UseCaseResponse<IUpdateUnitsTypeResultContract>(new UpdateUnitsTypeResultContract());
        }
    }
}