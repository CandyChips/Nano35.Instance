using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateInstancePhone
{
    public class UpdateInstancePhoneValidatorErrorResult : 
        IUpdateInstancePhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstancePhoneRequest:
        IPipelineNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>
    {
        private readonly IPipelineNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract> _nextNode;

        public ValidatedUpdateInstancePhoneRequest(
            IPipelineNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateInstancePhoneResultContract> Ask(
            IUpdateInstancePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateInstancePhoneValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}