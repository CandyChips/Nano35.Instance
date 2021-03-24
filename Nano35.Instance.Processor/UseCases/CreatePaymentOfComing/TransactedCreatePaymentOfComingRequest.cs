using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfComing
{
    public class TransactedCreatePaymentOfComingRequest :
        PipeNodeBase<
            ICreatePaymentOfComingRequestContract,
            ICreatePaymentOfComingResultContract>
    {
        private readonly ApplicationContext _context;
      
        public TransactedCreatePaymentOfComingRequest(
            ApplicationContext context,
            IPipeNode<ICreatePaymentOfComingRequestContract,
                ICreatePaymentOfComingResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input,
            CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
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