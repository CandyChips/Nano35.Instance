using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerValidatorErrorResult : ICreateWorkerErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateWorkerValidator:
        IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> _nextNode;

        public CreateWorkerValidator(
            IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            if (false)
            {
                return new CreateWorkerValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}