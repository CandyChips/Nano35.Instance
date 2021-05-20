using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class GetWorkerStringById : EndPointNodeBase<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetWorkerStringById(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetWorkerStringByIdResultContract>> Ask(
            IGetWorkerStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Workers
                .FirstOrDefaultAsync(e => e.Id == input.WorkerId, cancellationToken);
            return result == null ? 
                Pass("Сотрудник не найден.") : 
                Pass(new GetWorkerStringByIdResultContract() { Data = result.ToString() });
        }
    }
}