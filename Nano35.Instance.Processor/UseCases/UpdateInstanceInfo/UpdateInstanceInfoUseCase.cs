using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceInfo
{
    public class UpdateInstanceInfoUseCase : EndPointNodeBase<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstanceInfoUseCase(ApplicationContext context) => _context = context;
        public override async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Instances.FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            result.CompanyInfo = input.Info;
            return new UpdateInstanceInfoSuccessResultContract();
        }
    }
}