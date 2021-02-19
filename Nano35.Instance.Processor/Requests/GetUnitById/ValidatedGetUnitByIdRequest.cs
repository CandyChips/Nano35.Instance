using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetUnitById
{
    public class GetUnitByIdValidatorErrorResult :
        IGetUnitByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUnitByIdRequest:
        IPipelineNode<
            IGetUnitByIdRequestContract,
            IGetUnitByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetUnitByIdRequestContract,
            IGetUnitByIdResultContract> _nextNode;

        public ValidatedGetUnitByIdRequest(
            IPipelineNode<
                IGetUnitByIdRequestContract, 
                IGetUnitByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetUnitByIdResultContract> Ask(
            IGetUnitByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetUnitByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}