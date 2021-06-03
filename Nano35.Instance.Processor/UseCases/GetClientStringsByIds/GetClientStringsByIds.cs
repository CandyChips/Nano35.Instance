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
            var result = input
                .ClientIds
                .Select(a => 
                    _context
                        .Clients
                        .First(c => c.Id == a)
                        .ToString())
                .ToList();
            return new UseCaseResponse<IGetClientStringsByIdsResultContract>(new GetClientStringsByIdsResultContract() {Data = result});
        }
    }
}