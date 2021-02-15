using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateWorker
{
    public class CreateWorkerValidatorErrorResult :
        ICreateWorkerErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateWorkerRequest:
        IPipelineNode<
            ICreateWorkerRequestContract,
            ICreateWorkerResultContract>
    {
        private readonly IPipelineNode<
            ICreateWorkerRequestContract,
            ICreateWorkerResultContract> _nextNode;

        public ValidatedCreateWorkerRequest(
            IPipelineNode<
                ICreateWorkerRequestContract, 
                ICreateWorkerResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateWorkerValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}