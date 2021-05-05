using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerById
{
    public class GetWorkerByIdUseCase : UseCaseEndPointNodeBase<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetWorkerByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetWorkerByIdResultContract>> Ask(
            IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Workers
                .FirstOrDefaultAsync(f => f.Id == input.WorkerId, cancellationToken);
            return result == null ? 
                new UseCaseResponse<IGetWorkerByIdResultContract>("Сотрудник не найден.") : 
                new UseCaseResponse<IGetWorkerByIdResultContract>(
                    new GetWorkerByIdResultContract()
                    {
                        Data =
                            new WorkerViewModel()
                                {Id = result.Id,
                                 Comment = result.Comment}
                    });
        }
    }
}