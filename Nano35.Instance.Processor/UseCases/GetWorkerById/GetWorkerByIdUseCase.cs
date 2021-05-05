using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerById
{
    public class GetWorkerByIdUseCase : UseCaseEndPointNodeBase<IGetWorkerByIdRequestContract, IGetWorkerByIdSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetWorkerByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetWorkerByIdSuccessResultContract>> Ask(
            IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Workers
                .FirstAsync(f => f.Id == input.WorkerId, cancellationToken);
            
            if (result == null) return new UseCaseResponse<IGetWorkerByIdSuccessResultContract>("Сотрудник не найден.");

            return new UseCaseResponse<IGetWorkerByIdSuccessResultContract>(
                new GetWorkerByIdSuccessResultContract()
                {
                    Data = new WorkerViewModel()
                    {
                        Id = result.Id,
                        Comment = result.Comment
                    }
                });
        }
    }
}