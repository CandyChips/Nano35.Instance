using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringsByIds
{
    public class GetInstanceStringsByIdsRequest :
        EndPointNodeBase<IGetInstanceStringsByIdsRequestContract, IGetInstanceStringsByIdsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetInstanceStringsByIdsRequest(ApplicationContext context) { _context = context; }
        
        private class GetInstanceStringsByIdsSuccessResultContract : 
            IGetInstanceStringsByIdsSuccessResultContract
        {
            public List<string> Data { get; set; }
        }

        public override async Task<IGetInstanceStringsByIdsResultContract> Ask(
            IGetInstanceStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Instances.Where(c => input.InstanceIds.Contains(c.Id))
                .Select(e => $"{e.OrgRealName}")
                .ToListAsync(cancellationToken));
            return new GetInstanceStringsByIdsSuccessResultContract() {Data = result};
        }
    }
}