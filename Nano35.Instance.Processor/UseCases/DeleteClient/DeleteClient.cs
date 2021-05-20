using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.DeleteClient
{
    public class DeleteClient : EndPointNodeBase<IDeleteClientRequestContract, IDeleteClientResultContract>
    {
        private readonly ApplicationContext _context;
        public DeleteClient(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IDeleteClientResultContract>> Ask(
            IDeleteClientRequestContract input, 
            CancellationToken cancellationToken)
        {
            var entity = await _context
                .Clients
                .FirstAsync(e => e.Id == input.ClientId, cancellationToken);
            entity.Deleted = true;
            return Pass(new DeleteClientResultContract());
        }
    }   
}