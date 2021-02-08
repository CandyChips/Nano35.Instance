using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateUnitTransactionErrorResult : ICreateUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateUnitTransaction :
        IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> _nextNode;

        public CreateUnitTransaction(
            IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> nextNode, ApplicationContext context)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateUnitResultContract> Ask(
            ICreateUnitRequestContract input)
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
                return new CreateUnitTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}