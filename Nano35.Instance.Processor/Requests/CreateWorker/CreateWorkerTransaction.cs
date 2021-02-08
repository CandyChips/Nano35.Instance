using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateWorkerTransactionErrorResult : ICreateWorkerErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateWorkerTransaction :
        IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> _nextNode;

        public CreateWorkerTransaction(
            IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> nextNode, ApplicationContext context)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var response = await _nextNode.Ask(input);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return response;
            }
            catch
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                return new CreateWorkerTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}