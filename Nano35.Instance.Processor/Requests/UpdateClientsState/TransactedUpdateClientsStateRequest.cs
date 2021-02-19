using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Requests.UpdateClientsState
{
    public class TransactedUpdateClientsStateRequest :
        IPipelineNode<
            IUpdateClientsStateRequestContract,
            IUpdateClientsStateResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateClientsStateRequestContract,
            IUpdateClientsStateResultContract> _nextNode;

        public TransactedUpdateClientsStateRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateClientsStateRequestContract,
                IUpdateClientsStateResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input,
            CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await _nextNode.Ask(input, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw new Exception("Транзакция отменена", ex);
            }
        }
    }
}