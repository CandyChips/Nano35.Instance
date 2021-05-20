using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsAddress
{
    public class UpdateUnitsAddress : EndPointNodeBase<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateUnitsAddress(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateUnitsAddressResultContract>> Ask(
            IUpdateUnitsAddressRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfUnit = await _context
                .Units
                .FirstOrDefaultAsync(a => a.Id == input.UnitId, cancellationToken);
            if (entityOfUnit == null) return new UseCaseResponse<IUpdateUnitsAddressResultContract>("Подразделение не найдено.");
            entityOfUnit.Adress = input.Address;
            return new UseCaseResponse<IUpdateUnitsAddressResultContract>(new UpdateUnitsAddressResultContract());
        }
    }
}