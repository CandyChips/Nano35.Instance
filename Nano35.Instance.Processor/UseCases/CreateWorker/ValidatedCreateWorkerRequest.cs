using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateWorker
{
    public class CreateWorkerValidatorErrorResult :
        ICreateWorkerErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateWorkerRequest:
        PipeNodeBase<
            ICreateWorkerRequestContract,
            ICreateWorkerResultContract>
    {

        public ValidatedCreateWorkerRequest(
            IPipeNode<ICreateWorkerRequestContract,
                ICreateWorkerResultContract> next) : base(next)
        {}

        public override async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateWorkerValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}