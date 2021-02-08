using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateInstanceTransactionErrorResult : ICreateInstanceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateInstanceTransaction :
        IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> _nextNode;

        public CreateInstanceTransaction(
            IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> nextNode, ApplicationContext context)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input)
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
                return new CreateInstanceTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}