using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringById
{
    public class GetUnitStringByIdUseCase : UseCaseEndPointNodeBase<IGetUnitStringByIdRequestContract, IGetUnitStringByIdSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetUnitStringByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetUnitStringByIdSuccessResultContract>> Ask(
            IGetUnitStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Units.FirstOrDefaultAsync(e => e.Id == input.UnitId, cancellationToken));
            
            return result == null ? 
                new UseCaseResponse<IGetUnitStringByIdSuccessResultContract>("Подразделение не найдено.") :
                new UseCaseResponse<IGetUnitStringByIdSuccessResultContract>(new GetUnitStringByIdSuccessResultContract() {Data = $"{result.Name} - {result.Adress}"});
        }
    }
}