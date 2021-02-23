using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.UpdateWorkersComment
{
    public class UpdateWorkersCommentRequest :
        IPipelineNode<
            IUpdateWorkersCommentRequestContract,
            IUpdateWorkersCommentResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateWorkersCommentRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateWorkersCommentSuccessResultContract : 
            IUpdateWorkersCommentSuccessResultContract
        {
            
        }

        public async Task<IUpdateWorkersCommentResultContract> Ask(
            IUpdateWorkersCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}