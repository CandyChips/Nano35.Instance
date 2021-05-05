using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class GetWorkerStringByIdUseCase : UseCaseEndPointNodeBase<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetWorkerStringByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetWorkerStringByIdSuccessResultContract>> Ask(
            IGetWorkerStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = (await _context
                .Workers
                .FirstOrDefaultAsync(e => e.Id == input.WorkerId, cancellationToken))
                .ToString();
            
            return result == null ? 
                new UseCaseResponse<IGetWorkerStringByIdSuccessResultContract>("Сотрудник не найден.") : 
                new UseCaseResponse<IGetWorkerStringByIdSuccessResultContract>(new GetWorkerStringByIdSuccessResultContract() {Data = result});
        }
    }
}