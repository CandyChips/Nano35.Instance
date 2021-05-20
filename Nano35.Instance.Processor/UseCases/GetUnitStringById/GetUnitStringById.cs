using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringById
{
    public class GetUnitStringById : EndPointNodeBase<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetUnitStringById(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetUnitStringByIdResultContract>> Ask(
            IGetUnitStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Units
                .FirstOrDefaultAsync(e => e.Id == input.UnitId, cancellationToken);
            return result == null ? 
                new UseCaseResponse<IGetUnitStringByIdResultContract>("Подразделение не найдено.") :
                new UseCaseResponse<IGetUnitStringByIdResultContract>(new GetUnitStringByIdResultContract() {Data = $"{result.Name} - {result.Adress}"});
        }
    }
}