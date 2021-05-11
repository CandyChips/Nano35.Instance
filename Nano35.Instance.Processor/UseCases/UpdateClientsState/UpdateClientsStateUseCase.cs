using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsState
{
    public class UpdateClientsStateUseCase : UseCaseEndPointNodeBase<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateClientsStateUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateClientsStateResultContract>> Ask(
            IUpdateClientsStateRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfClient = await _context
                .Clients
                .FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken);
            if (entityOfClient == null) return new UseCaseResponse<IUpdateClientsStateResultContract>("Клиент не найден.");
            entityOfClient.ClientStateId = input.StateId;
            return new UseCaseResponse<IUpdateClientsStateResultContract>(new UpdateClientsStateResultContract());
        }
    }
}