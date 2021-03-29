using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringsByIds
{
    public class GetUnitStringsByIdsUseCase :
        EndPointNodeBase<IGetUnitStringsByIdsRequestContract, IGetUnitStringsByIdsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUnitStringsByIdsUseCase(ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetUnitStringsByIdsResultContract> Ask(
            IGetUnitStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Units.Where(c => input.UnitIds.Contains(c.Id))
                .Select(e => $"{e.Name} - {e.Adress}")
                .ToListAsync(cancellationToken));
            return new GetUnitStringsByIdsSuccessResultContract() {Data = result};
        }
    }
}