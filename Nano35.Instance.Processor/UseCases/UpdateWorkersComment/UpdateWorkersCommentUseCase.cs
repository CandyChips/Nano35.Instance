using System;
using System.Threading;
using System.Threading.Tasks;
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

        public UpdateWorkersCommentUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateWorkersCommentSuccessResultContract : 
            IUpdateWorkersCommentSuccessResultContract
        {
            
        }

        public override async Task<IUpdateWorkersCommentResultContract> Ask(
            IUpdateWorkersCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}