using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateInstance
{
    public class TransactedCreateInstanceRequest :
        PipeNodeBase<
            ICreateInstanceRequestContract,
            ICreateInstanceResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateInstanceRequest(
            ApplicationContext context,
            IPipeNode<ICreateInstanceRequestContract,
                ICreateInstanceResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input,
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