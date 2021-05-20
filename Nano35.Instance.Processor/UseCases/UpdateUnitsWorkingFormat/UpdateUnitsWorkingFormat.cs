using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormat : EndPointNodeBase<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateUnitsWorkingFormat(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateUnitsWorkingFormatResultContract>> Ask(
            IUpdateUnitsWorkingFormatRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfUnit = await _context
                .Units
                .FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            if (entityOfUnit == null) return new UseCaseResponse<IUpdateUnitsWorkingFormatResultContract>("Подразделение не найдено.");
            entityOfUnit.WorkingFormat = input.WorkingFormat;
            return new UseCaseResponse<IUpdateUnitsWorkingFormatResultContract>(new UpdateUnitsWorkingFormatResultContract());
        }
    }
}