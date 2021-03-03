using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceInfo
{
    public class TransactedUpdateInstanceInfoRequest :
        IPipelineNode<
            IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract> _nextNode;

        public TransactedUpdateInstanceInfoRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateInstanceInfoRequestContract,
                IUpdateInstanceInfoResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input,
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