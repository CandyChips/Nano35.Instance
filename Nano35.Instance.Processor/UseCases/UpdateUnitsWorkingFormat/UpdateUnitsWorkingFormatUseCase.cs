using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatUseCase : UseCaseEndPointNodeBase<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateUnitsWorkingFormatUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateUnitsWorkingFormatResultContract>> Ask(
            IUpdateUnitsWorkingFormatRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfUnit = await _context
                .Units
                .FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            if (entityOfUnit == null) return new UseCaseResponse<IUpdateUnitsWorkingFormatResultContract>("Подразделение не найдено.");
            entityOfUnit.WorkingFormat = input.WorkingFormat;
            entityOfUnit.CreatorId = input.UpdaterId;
            return new UseCaseResponse<IUpdateUnitsWorkingFormatResultContract>(new UpdateUnitsWorkingFormatResultContract());
        }
    }
}