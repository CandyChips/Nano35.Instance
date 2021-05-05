using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneUseCase : UseCaseEndPointNodeBase<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateUnitsPhoneUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateUnitsPhoneResultContract>> Ask(
            IUpdateUnitsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfUnit = await _context
                .Units
                .FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            if (entityOfUnit == null) return new UseCaseResponse<IUpdateUnitsPhoneResultContract>("Подразделение не найдено.");
            entityOfUnit.Phone = input.Phone;
            entityOfUnit.CreatorId = input.UpdaterId;
            return new UseCaseResponse<IUpdateUnitsPhoneResultContract>(new UpdateUnitsPhoneResultContract());
        }
    }
}