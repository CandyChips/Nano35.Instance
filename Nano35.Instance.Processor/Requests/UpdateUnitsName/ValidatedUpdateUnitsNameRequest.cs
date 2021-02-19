using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateUnitsName
{
    public class UpdateUnitsNameValidatorErrorResult : 
        IUpdateUnitsNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsNameRequest:
        IPipelineNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>
    {
        private readonly IPipelineNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract> _nextNode;

        public ValidatedUpdateUnitsNameRequest(
            IPipelineNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateUnitsNameResultContract> Ask(
            IUpdateUnitsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateUnitsNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}