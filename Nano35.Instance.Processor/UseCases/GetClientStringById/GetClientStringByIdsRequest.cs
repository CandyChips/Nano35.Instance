using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class GetClientStringById : EndPointNodeBase<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetClientStringById(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetClientStringByIdResultContract>> Ask(
            IGetClientStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Clients
                .FirstOrDefaultAsync(e => e.Id == input.ClientId && e.InstanceId == input.InstanceId, cancellationToken);
            return result == null ? 
                Pass("Клиент не найден.") : 
                Pass(new GetClientStringByIdResultContract() { Data = $"{result.Name} - +7{result.ClientProfile.Phone}" });
        }
    }
}