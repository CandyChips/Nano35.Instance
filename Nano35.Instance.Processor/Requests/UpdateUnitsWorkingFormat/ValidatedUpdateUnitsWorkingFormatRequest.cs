using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatValidatorErrorResult : 
        IUpdateUnitsWorkingFormatErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsWorkingFormatRequest:
        IPipelineNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly IPipelineNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract> _nextNode;

        public ValidatedUpdateUnitsWorkingFormatRequest(
            IPipelineNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateUnitsWorkingFormatResultContract> Ask(
            IUpdateUnitsWorkingFormatRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateUnitsWorkingFormatValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}