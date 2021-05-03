using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringsByIds
{
    public class GetUnitStringsByIdsUseCase :
        UseCaseEndPointNodeBase<IGetUnitStringsByIdsRequestContract, IGetUnitStringsByIdsSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetUnitStringsByIdsUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetUnitStringsByIdsSuccessResultContract>> Ask(
            IGetUnitStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Units.Where(c => input.UnitIds.Contains(c.Id))
                .Select(e => $"{e.Name} - {e.Adress}")
                .ToListAsync(cancellationToken));
            
            return 
                new UseCaseResponse<IGetUnitStringsByIdsSuccessResultContract>(
                    new GetUnitStringsByIdsSuccessResultContract() {Data = result});
        }
    }
}