using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfSelle
{
    public class TransactedCreatePaymentOfSelleRequest :
        IPipelineNode<
            ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract> _nextNode;

        public TransactedCreatePaymentOfSelleRequest(
            ApplicationContext context,
            IPipelineNode<
                ICreatePaymentOfSelleRequestContract,
                ICreatePaymentOfSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input,
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