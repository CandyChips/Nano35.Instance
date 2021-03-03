using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateCashInput
{
    public class TransactedCreateCashInputRequest :
        IPipelineNode<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract> _nextNode;

        public TransactedCreateCashInputRequest(
            ApplicationContext context,
            IPipelineNode<
                ICreateCashInputRequestContract,
                ICreateCashInputResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input,
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