using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases
{
    public class Error : IError
    {
        public string Message { get; set; }
    }
    
    public class TransactedPipeNode<TIn, TOut> : PipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly ApplicationContext _context;

        public TransactedPipeNode(
            ApplicationContext context,
            IPipeNode<TIn, TOut> next) : base(next)
        {
            _context = context;
        }

        public override async Task<TOut> Ask(TIn input, CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
                switch (response)
                {
                    case ISuccess:
                        break;
                    default:
                        await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                        break;
                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw;
            }
        }
    }
}