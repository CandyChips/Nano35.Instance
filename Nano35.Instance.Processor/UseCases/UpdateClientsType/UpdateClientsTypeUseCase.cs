using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsType
{
    public class UpdateClientsTypeUseCase : UseCaseEndPointNodeBase<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateClientsTypeUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateClientsTypeResultContract>> Ask(
            IUpdateClientsTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfClient = await _context
                .Clients
                .FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken);
            if (entityOfClient == null) return new UseCaseResponse<IUpdateClientsTypeResultContract>("Клиент не найден.");
            entityOfClient.WorkerId = input.UpdaterId;
            entityOfClient.ClientTypeId = input.TypeId;
            return new UseCaseResponse<IUpdateClientsTypeResultContract>(new UpdateClientsTypeResultContract());
        }
    }
}