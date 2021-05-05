using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class GetInstanceStringByIdUseCase : UseCaseEndPointNodeBase<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetInstanceStringByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetInstanceStringByIdResultContract>> Ask(
            IGetInstanceStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context
                .Instances
                .FirstOrDefaultAsync(e => e.Id == input.InstanceId, cancellationToken));
            return result == null ?
                new UseCaseResponse<IGetInstanceStringByIdResultContract>("Организация не найден.") :
                new UseCaseResponse<IGetInstanceStringByIdResultContract>(new GetInstanceStringByIdResultContract() { Data = result.ToString() });
        }
    }
}