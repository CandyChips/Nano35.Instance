using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringsByIds
{
    public class GetClientStringsByIds : EndPointNodeBase<IGetClientStringsByIdsRequestContract, IGetClientStringsByIdsResultContract>
    {
        private readonly ApplicationContext _context;
        public GetClientStringsByIds(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetClientStringsByIdsResultContract>> Ask(
            IGetClientStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Clients
                .Where(c => input.ClientIds.Contains(c.Id))
                .Select(e => $"{e.Name} - {e.ClientProfile.Phone}")
                .ToListAsync(cancellationToken);
            
            return new UseCaseResponse<IGetClientStringsByIdsResultContract>(new GetClientStringsByIdsResultContract() {Data = result});
        }
    }
}