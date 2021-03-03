using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnits
{
    public class GetAllUnitsValidatorErrorResult : IGetAllUnitsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUnitsRequest:
        IPipelineNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>
    {
        private readonly IPipelineNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract> _nextNode;

        public ValidatedGetAllUnitsRequest(
            IPipelineNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllUnitsResultContract> Ask(IGetAllUnitsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllUnitsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}