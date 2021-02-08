using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateClientTransactionErrorResult : ICreateClientErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateClientTransaction :
        IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> _nextNode;

        public CreateClientTransaction(
            IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> nextNode, ApplicationContext context)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            await using var transaction = _context.Database.BeginTransaction();
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
                return new CreateClientTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}