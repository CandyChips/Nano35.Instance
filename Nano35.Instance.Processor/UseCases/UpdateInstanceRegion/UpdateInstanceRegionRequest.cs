using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRegion
{
    public class UpdateInstanceRegionRequest :
        EndPointNodeBase<
            IUpdateInstanceRegionRequestContract,
            IUpdateInstanceRegionResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateInstanceRegionRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateInstanceRegionSuccessResultContract : 
            IUpdateInstanceRegionSuccessResultContract
        {
            
        }

        public override async Task<IUpdateInstanceRegionResultContract> Ask(
            IUpdateInstanceRegionRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Instances.FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken));
            result.RegionId = input.RegionId;

            return new UpdateInstanceRegionSuccessResultContract();
        }
    }
}