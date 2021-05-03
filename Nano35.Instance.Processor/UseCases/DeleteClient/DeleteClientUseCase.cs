using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.DeleteClient
{
    public class DeleteClientUseCase :
        UseCaseEndPointNodeBase<IDeleteClientRequestContract, IDeleteClientSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public DeleteClientUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IDeleteClientSuccessResultContract>> Ask(
            IDeleteClientRequestContract input, 
            CancellationToken cancellationToken)
        {
            var entity = await _context.Clients.FirstAsync(e => e.Id == input.ClientId, cancellationToken: cancellationToken);
            entity.Deleted = true;
            return new UseCaseResponse<IDeleteClientSuccessResultContract>(new DeleteClientSuccessResultContract());
        }
    }   
}