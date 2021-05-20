using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringsByIds
{
    public class GetUnitStringsByIds : EndPointNodeBase<IGetUnitStringsByIdsRequestContract, IGetUnitStringsByIdsResultContract>
    {
        private readonly ApplicationContext _context;
        public GetUnitStringsByIds(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetUnitStringsByIdsResultContract>> Ask(
            IGetUnitStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context
                .Units
                .Where(c => input.UnitIds.Contains(c.Id))
                .Select(e => $"{e.Name} - {e.Adress}")
                .ToListAsync(cancellationToken));
            
            return new UseCaseResponse<IGetUnitStringsByIdsResultContract>(new GetUnitStringsByIdsResultContract() {Data = result});
        }
    }
}