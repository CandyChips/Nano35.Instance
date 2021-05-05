using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceEmail
{
    public class UpdateInstanceEmailUseCase : UseCaseEndPointNodeBase<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateInstanceEmailUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateInstanceEmailResultContract>> Ask(
            IUpdateInstanceEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfInstance = await _context
                .Instances
                .FirstOrDefaultAsync(a => a.Id == input.InstanceId, cancellationToken);
            if (entityOfInstance == null) return new UseCaseResponse<IUpdateInstanceEmailResultContract>("Организация не найдена.");
            entityOfInstance.OrgEmail = input.Email;
            return new UseCaseResponse<IUpdateInstanceEmailResultContract>(new UpdateInstanceEmailResultContract());
        }
    }
}