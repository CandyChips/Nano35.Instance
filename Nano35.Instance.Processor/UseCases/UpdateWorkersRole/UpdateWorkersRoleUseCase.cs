using System.Diagnostics.Eventing.Reader;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersRole
{
    public class UpdateWorkersRoleUseCase : UseCaseEndPointNodeBase<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateWorkersRoleUseCase(ApplicationContext context) { _context = context; }
        public override async Task<UseCaseResponse<IUpdateWorkersRoleResultContract>> Ask(
            IUpdateWorkersRoleRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfWorker = await _context
                .Workers
                .FirstOrDefaultAsync(f => f.Id == input.WorkersId, cancellationToken);
            if (entityOfWorker == null) return new UseCaseResponse<IUpdateWorkersRoleResultContract>("Сотрудник не найден");
            entityOfWorker.WorkersRoleId = input.RoleId;
            return new UseCaseResponse<IUpdateWorkersRoleResultContract>(new UpdateWorkersRoleResultContract());
        }
    }
}