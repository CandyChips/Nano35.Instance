using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsEmail
{
    public class UpdateClientsEmailUseCase : UseCaseEndPointNodeBase<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateClientsEmailUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateClientsEmailResultContract>> Ask(
            IUpdateClientsEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfClient = await _context
                .Clients
                .FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken);
            if (entityOfClient == null) return new UseCaseResponse<IUpdateClientsEmailResultContract>("Клиент не найден.");
            entityOfClient.WorkerId = input.UpdaterId;
            entityOfClient.Email = input.Email;
            return new UseCaseResponse<IUpdateClientsEmailResultContract>(new UpdateClientsEmailResultContract());
        }
    }
}