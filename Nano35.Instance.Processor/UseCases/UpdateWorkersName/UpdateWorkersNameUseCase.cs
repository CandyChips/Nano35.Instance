using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class UpdateWorkersNameUseCase :
        EndPointNodeBase<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateWorkersNameUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IUpdateWorkersNameResultContract> Ask(
            IUpdateWorkersNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfWorker = await _context.Workers.FirstAsync(f => f.Id == input.WorkersId, cancellationToken: cancellationToken);
            entityOfWorker.Name = input.Name;
            return new UpdateWorkersNameSuccessResultContract();
        }
    }
}