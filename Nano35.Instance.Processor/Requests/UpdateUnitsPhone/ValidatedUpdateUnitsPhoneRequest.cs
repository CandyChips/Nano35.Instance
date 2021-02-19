using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneValidatorErrorResult : 
        IUpdateUnitsPhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsPhoneRequest:
        IPipelineNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>
    {
        private readonly IPipelineNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract> _nextNode;

        public ValidatedUpdateUnitsPhoneRequest(
            IPipelineNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateUnitsPhoneResultContract> Ask(
            IUpdateUnitsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateUnitsPhoneValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}