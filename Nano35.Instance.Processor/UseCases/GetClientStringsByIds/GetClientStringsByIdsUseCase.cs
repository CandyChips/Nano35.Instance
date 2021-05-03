using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringsByIds
{
    public class GetClientStringsByIdsUseCase :
        UseCaseEndPointNodeBase<IGetClientStringsByIdsRequestContract, IGetClientStringsByIdsSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetClientStringsByIdsUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetClientStringsByIdsSuccessResultContract>> Ask(
            IGetClientStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Clients.Where(c => input.ClientIds.Contains(c.Id))
                .Select(e => $"{e.Name} - {e.ClientProfile.Phone}")
                .ToListAsync(cancellationToken);
            return
                new UseCaseResponse<IGetClientStringsByIdsSuccessResultContract>(
                    new GetClientStringsByIdsSuccessResultContract() {Data = result});
        }
    }
}