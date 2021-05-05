using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsName
{
    public class UpdateClientsNameUseCase : UseCaseEndPointNodeBase<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateClientsNameUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateClientsNameResultContract>> Ask(
            IUpdateClientsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfClient = await _context
                .Clients
                .FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken);
            if (entityOfClient == null) return new UseCaseResponse<IUpdateClientsNameResultContract>("Клиент не найден.");
            entityOfClient.WorkerId = input.UpdaterId;
            entityOfClient.Name = input.Name;
            return new UseCaseResponse<IUpdateClientsNameResultContract>(new UpdateClientsNameResultContract());
        }
    }
}