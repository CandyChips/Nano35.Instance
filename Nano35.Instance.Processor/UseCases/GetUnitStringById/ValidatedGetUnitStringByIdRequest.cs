using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringById
{
    public class GetUnitStringByIdValidatorErrorResult :
        IGetUnitStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUnitStringByIdRequest:
        IPipelineNode<
            IGetUnitStringByIdRequestContract, 
            IGetUnitStringByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetUnitStringByIdRequestContract, 
            IGetUnitStringByIdResultContract> _nextNode;

        public ValidatedGetUnitStringByIdRequest(
            IPipelineNode<
                IGetUnitStringByIdRequestContract, 
                IGetUnitStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetUnitStringByIdResultContract> Ask(
            IGetUnitStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetUnitStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}