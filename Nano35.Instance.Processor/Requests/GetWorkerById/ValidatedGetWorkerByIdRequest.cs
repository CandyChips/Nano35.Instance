using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetWorkerById
{
    public class GetWorkerByIdValidatorErrorResult : 
        IGetWorkerByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetWorkerByIdRequest:
        IPipelineNode<
            IGetWorkerByIdRequestContract,
            IGetWorkerByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetWorkerByIdRequestContract,
            IGetWorkerByIdResultContract> _nextNode;

        public ValidatedGetWorkerByIdRequest(
            IPipelineNode<
                IGetWorkerByIdRequestContract,
                IGetWorkerByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetWorkerByIdResultContract> Ask(
            IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetWorkerByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}