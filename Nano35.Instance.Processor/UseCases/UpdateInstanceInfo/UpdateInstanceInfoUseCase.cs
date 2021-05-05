using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceInfo
{
    public class UpdateInstanceInfoUseCase : UseCaseEndPointNodeBase<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstanceInfoUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateInstanceInfoResultContract>> Ask(
            IUpdateInstanceInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfInstance = await _context
                .Instances
                .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            if (entityOfInstance == null) return new UseCaseResponse<IUpdateInstanceInfoResultContract>("Организация не найдена.");
            entityOfInstance.CompanyInfo = input.Info;
            return new UseCaseResponse<IUpdateInstanceInfoResultContract>(new UpdateInstanceInfoResultContract());
        }
    }
}