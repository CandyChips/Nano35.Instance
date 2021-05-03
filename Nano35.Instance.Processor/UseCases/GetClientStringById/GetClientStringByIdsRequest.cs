using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class GetClientStringByIdUseCase :
        UseCaseEndPointNodeBase<IGetClientStringByIdRequestContract, IGetClientStringByIdSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetClientStringByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetClientStringByIdSuccessResultContract>> Ask(
            IGetClientStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Clients
                .FirstOrDefaultAsync(e => e.Id == input.ClientId, cancellationToken: cancellationToken);
            
            if (result == null)
            {
                return new UseCaseResponse<IGetClientStringByIdSuccessResultContract>("Клиент не найден.");
            }
            
            return 
                new UseCaseResponse<IGetClientStringByIdSuccessResultContract>(
                    new GetClientStringByIdSuccessResultContract() {Data = $"{result.Name} - +7{result.ClientProfile.Phone}"});
        }
    }
}