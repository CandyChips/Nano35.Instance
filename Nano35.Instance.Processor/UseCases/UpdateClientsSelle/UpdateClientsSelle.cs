using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsSelle
{
    public class UpdateClientsSelle : EndPointNodeBase<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateClientsSelle(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateClientsSelleResultContract>> Ask(
            IUpdateClientsSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfClient = await _context
                .Clients
                .FirstOrDefaultAsync(a => a.Id == input.ClientId, cancellationToken);
            
            return new UseCaseResponse<IUpdateClientsSelleResultContract>(new UpdateClientsSelleResultContract());
        }
    }
}