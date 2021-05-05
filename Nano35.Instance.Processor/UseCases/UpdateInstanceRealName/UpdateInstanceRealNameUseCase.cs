using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameUseCase : UseCaseEndPointNodeBase<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstanceRealNameUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateInstanceRealNameResultContract>> Ask(
            IUpdateInstanceRealNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfInstance = await _context
                .Instances
                .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            if (entityOfInstance == null) return new UseCaseResponse<IUpdateInstanceRealNameResultContract>("Организация не найдена.");
            entityOfInstance.OrgRealName = input.RealName;
            return new UseCaseResponse<IUpdateInstanceRealNameResultContract>(new UpdateInstanceRealNameResultContract());
        }
    }
}