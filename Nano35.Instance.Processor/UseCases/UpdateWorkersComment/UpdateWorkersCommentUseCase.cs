using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersComment
{
    public class UpdateWorkersCommentUseCase :
        EndPointNodeBase<
            IUpdateWorkersCommentRequestContract,
            IUpdateWorkersCommentResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateWorkersCommentUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IUpdateWorkersCommentResultContract> Ask(
            IUpdateWorkersCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfWorker = await _context.Workers.FirstAsync(f => f.Id == input.WorkersId, cancellationToken);
            entityOfWorker.Comment = input.Comment;
            return new UpdateWorkersCommentSuccessResultContract();
        }
    }
}