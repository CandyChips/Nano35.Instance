using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class GetInstanceStringByIdUseCase : UseCaseEndPointNodeBase<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetInstanceStringByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetInstanceStringByIdSuccessResultContract>> Ask(
            IGetInstanceStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context
                .Instances
                .FirstOrDefaultAsync(e => e.Id == input.InstanceId, cancellationToken))
                .ToString();
            
            return result == null ?
                new UseCaseResponse<IGetInstanceStringByIdSuccessResultContract>("Организация не найден.") :
                new UseCaseResponse<IGetInstanceStringByIdSuccessResultContract>(new GetInstanceStringByIdSuccessResultContract() {Data = result});
        }
    }
}