using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds
{
    public class GetWorkerStringsByIdsValidatorErrorResult :
        IGetWorkerStringsByIdsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetWorkerStringsByIdsRequest:
        IPipelineNode<
            IGetWorkerStringsByIdsRequestContract, 
            IGetWorkerStringsByIdsResultContract>
    {
        private readonly IPipelineNode<
            IGetWorkerStringsByIdsRequestContract, 
            IGetWorkerStringsByIdsResultContract> _nextNode;

        public ValidatedGetWorkerStringsByIdsRequest(
            IPipelineNode<
                IGetWorkerStringsByIdsRequestContract, 
                IGetWorkerStringsByIdsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetWorkerStringsByIdsResultContract> Ask(
            IGetWorkerStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetWorkerStringsByIdsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}