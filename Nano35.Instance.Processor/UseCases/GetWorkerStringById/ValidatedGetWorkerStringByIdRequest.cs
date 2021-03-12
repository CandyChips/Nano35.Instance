using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class GetWorkerStringByIdValidatorErrorResult :
        IGetWorkerStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetWorkerStringByIdRequest:
        IPipelineNode<
            IGetWorkerStringByIdRequestContract, 
            IGetWorkerStringByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetWorkerStringByIdRequestContract, 
            IGetWorkerStringByIdResultContract> _nextNode;

        public ValidatedGetWorkerStringByIdRequest(
            IPipelineNode<
                IGetWorkerStringByIdRequestContract, 
                IGetWorkerStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetWorkerStringByIdResultContract> Ask(
            IGetWorkerStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetWorkerStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}