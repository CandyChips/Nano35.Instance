using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRegion
{
    public class UpdateInstanceRegion : EndPointNodeBase<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstanceRegion(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateInstanceRegionResultContract>> Ask(
            IUpdateInstanceRegionRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfInstance = await _context
                .Instances
                .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            if (entityOfInstance == null) return new UseCaseResponse<IUpdateInstanceRegionResultContract>("Организация не найдена.");
            entityOfInstance.RegionId = input.RegionId;
            return new UseCaseResponse<IUpdateInstanceRegionResultContract>(new UpdateInstanceRegionResultContract());
        }
    }
}