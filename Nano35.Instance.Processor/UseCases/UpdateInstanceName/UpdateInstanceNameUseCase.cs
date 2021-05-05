using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceName
{
    public class UpdateInstanceNameUseCase : UseCaseEndPointNodeBase<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstanceNameUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateInstanceNameResultContract>> Ask(
            IUpdateInstanceNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfInstance = await _context
                .Instances
                .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            if (entityOfInstance == null) return new UseCaseResponse<IUpdateInstanceNameResultContract>("Организация не найдена.");
            entityOfInstance.OrgName = input.Name;
            return new UseCaseResponse<IUpdateInstanceNameResultContract>(new UpdateInstanceNameResultContract());
        }
    }
}